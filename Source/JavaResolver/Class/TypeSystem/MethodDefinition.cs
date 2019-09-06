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
    public class MethodDefinition : IMethod, IExtraAttributeProvider
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
            // Name
            _name = new LazyValue<string>(() =>
                classImage.ClassFile.ConstantPool.ResolveString(methodInfo.NameIndex) ?? $"<<<INVALID({methodInfo.NameIndex})>>>");
            
            // Flags
            AccessFlags = methodInfo.AccessFlags;
            
            //Descriptor
            _descriptor = new LazyValue<MethodDescriptor>(() =>
                classImage.ResolveMethodDescriptor(methodInfo.DescriptorIndex));

            // Attributes
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

        /// <inheritdoc />
        public string FullName => this.GetMethodFullName();

        /// <summary>
        /// Gets or sets the accessibility flags associated to the method.
        /// </summary>
        public MethodAccessFlags AccessFlags
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the class that defines the method.
        /// </summary>
        public ClassDefinition DeclaringClass
        {
            get;
            internal set;
        }

        ClassReference IMemberReference.DeclaringClass => DeclaringClass;

        /// <summary>
        /// Gets or sets the method descriptor, which includes the return type as well as the parameter types.
        /// </summary>
        public MethodDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        IMemberDescriptor IMemberReference.Descriptor => Descriptor;

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

        /// <inheritdoc />
        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
        
        /// <inheritdoc />
        public override string ToString()
        {
            return FullName;
        }
    }
}