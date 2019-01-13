using System.Collections.Generic;
using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
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

        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        public MethodAccessFlags AccessFlags
        {
            get;
            set;
        }

        public MethodDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        public MethodBody Body
        {
            get => _body.Value;
            set => _body.Value = value;
        }

        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
    }
}