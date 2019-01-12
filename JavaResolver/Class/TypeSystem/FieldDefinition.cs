using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    public class FieldDefinition
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;

        public FieldDefinition(string name, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
        }

        internal FieldDefinition(JavaClassFile classFile, FieldInfo fieldInfo)
        {
            _name = new LazyValue<string>(() =>
                classFile.ConstantPool.ResolveString(fieldInfo.NameIndex) ?? $"<<<INVALID({fieldInfo.NameIndex})>>>");

            AccessFlags = fieldInfo.AccessFlags;
            
            _descriptor = new LazyValue<FieldDescriptor>(() =>
            {
                string rawDescriptor = classFile.ConstantPool.ResolveString(fieldInfo.DescriptorIndex);
                return rawDescriptor != null ? FieldDescriptor.FromString(rawDescriptor) : null;
            });
        }
        
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        public FieldAccessFlags AccessFlags
        {
            get;
            set;
        }
        
        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }
    }
}