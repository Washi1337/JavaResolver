using System.Collections.Generic;
using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation o fa single method that is defined in a Java class.
    /// </summary>
    public class MethodDefinition
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<MethodDescriptor> _descriptor;
        private readonly LazyValue<MethodBody> _body = new LazyValue<MethodBody>();

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
                    case "Code":
                        _body = new LazyValue<MethodBody>(() =>
                        {
                            var reader = new MemoryBigEndianReader(attribute.Contents);
                            return new MethodBody(classImage, CodeAttribute.FromReader(reader));
                        });
                        break;
                    
                    default:
                        ExtraAttributes.Add(name, attribute);
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
        public MethodBody Body
        {
            get => _body.Value;
            set => _body.Value = value;
        }
        
        /// <summary>
        /// Gets a collection of additional attributes that were not interpreted by JavaResolver itself.
        /// </summary>
        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
    }
}