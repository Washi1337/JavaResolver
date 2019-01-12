using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    public class MethodDefinition
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<MethodDescriptor> _descriptor;

        public MethodDefinition(string name, MethodDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<MethodDescriptor>(descriptor);
        }

        internal MethodDefinition(JavaClassFile classFile, MethodInfo methodInfo)
        {
            _name = new LazyValue<string>(() =>
                classFile.ConstantPool.ResolveString(methodInfo.NameIndex) ?? $"<<<INVALID({methodInfo.NameIndex})>>>");
            
            AccessFlags = methodInfo.AccessFlags;
            
            _descriptor = new LazyValue<MethodDescriptor>(() =>
            {
                string rawDescriptor = classFile.ConstantPool.ResolveString(methodInfo.DescriptorIndex);
                return rawDescriptor != null ? MethodDescriptor.FromString(rawDescriptor) : null;
            });
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
    }
}