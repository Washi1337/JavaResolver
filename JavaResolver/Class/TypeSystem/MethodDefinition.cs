using System.Collections.Generic;
using System.Linq;
using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation o fa single method that is defined in a Java class.
    /// </summary>
    public class MethodDefinition : IExtraAttributeProvider
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<MethodDescriptor> _descriptor;
        private readonly LazyValue<ByteCodeMethodBody> _body = new LazyValue<ByteCodeMethodBody>();
        private readonly LazyValue<IList<ClassReference>> _exceptions = new LazyValue<IList<ClassReference>>(new List<ClassReference>());
        
        public MethodDefinition(string name, MethodDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<MethodDescriptor>(descriptor);
        }

        internal MethodDefinition(JavaClassImage classImage, MethodInfo methodInfo)
        {
            _name = new LazyValue<string>(() =>
                classImage.ClassFile.ConstantPool.ResolveString(methodInfo.NameIndex) ?? $"<<<INVALID({methodInfo.NameIndex})>>>");
            
            AccessFlags = methodInfo.AccessFlags;
            
            _descriptor = new LazyValue<MethodDescriptor>(() =>
                classImage.ResolveMethodDescriptor(methodInfo.DescriptorIndex));

            foreach (var attribute in methodInfo.Attributes)
            {
                string name = classImage.ClassFile.ConstantPool.ResolveString(attribute.NameIndex);
                switch (name)
                {
                    // Code
                    case CodeAttribute.AttributeName:
                        _body = new LazyValue<ByteCodeMethodBody>(() =>
                        {
                            var reader = new MemoryBigEndianReader(attribute.Contents);
                            return new ByteCodeMethodBody(classImage, CodeAttribute.FromReader(reader));
                        });
                        break;
                    
                    // Exceptions
                    case ExceptionsAttribute.AttributeName:
                        _exceptions = new LazyValue<IList<ClassReference>>(() =>
                        {
                            var reader = new MemoryBigEndianReader(attribute.Contents);
                            var attr = ExceptionsAttribute.FromReader(reader);
                            return attr.Exceptions
                                .Select(index => classImage.ResolveClass(index))
                                .ToList();
                        });
                        break;
                    
                    default:
                        ExtraAttributes.Add(name, attribute.Clone());
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        /// <summary>
        /// Gets or sets the accessibility flags associated to the method.
        /// </summary>
        public MethodAccessFlags AccessFlags
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the method descriptor, which includes the return type as well as the parameter types.
        /// </summary>
        public MethodDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        /// <summary>
        /// Gets or sets the body of the method, which includes the code as well as local variables and exception handlers. 
        /// </summary>
        public ByteCodeMethodBody Body
        {
            get => _body.Value;
            set => _body.Value = value;
        }

        /// <summary>
        /// Gets a collection of exceptions that might be thrown during the execution of the method's code.
        /// </summary>
        public IList<ClassReference> Exceptions => _exceptions.Value;

        /// <summary>
        /// Gets a collection of additional attributes that were not interpreted by JavaResolver itself.
        /// </summary>
        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
    }
}