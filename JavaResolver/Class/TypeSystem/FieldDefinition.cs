using System.Collections.Generic;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a field defined in a Java class.
    /// </summary>
    public class FieldDefinition : IExtraAttributeProvider
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;

        public FieldDefinition(string name, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
        }

        internal FieldDefinition(JavaClassImage classImage, FieldInfo fieldInfo)
        {
            _name = new LazyValue<string>(() =>
                classImage.ClassFile.ConstantPool.ResolveString(fieldInfo.NameIndex)
                ?? $"<<<INVALID({fieldInfo.NameIndex})>>>");

            AccessFlags = fieldInfo.AccessFlags;

            _descriptor = new LazyValue<FieldDescriptor>(() =>
                classImage.ResolveFieldDescriptor(fieldInfo.DescriptorIndex));

            foreach (var attr in fieldInfo.Attributes)
                ExtraAttributes.Add(classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex), attr.Clone());
        }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }  

        /// <summary>
        /// Gets or sets the accessibility flags associated to the field.
        /// </summary>
        public FieldAccessFlags AccessFlags
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the field descriptor (which includes the field type) of the field.
        /// </summary>
        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
    }
}