using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a field defined in a Java class.
    /// </summary>
    public class FieldDefinition : IField, IExtraAttributeProvider
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;
        private readonly LazyValue<object> _constant = new LazyValue<object>();

        public FieldDefinition(string name, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
        }

        internal FieldDefinition(JavaClassImage classImage, FieldInfo fieldInfo)
        {
            // Name
            _name = new LazyValue<string>(() =>
                classImage.ClassFile.ConstantPool.ResolveString(fieldInfo.NameIndex)
                ?? $"<<<INVALID({fieldInfo.NameIndex})>>>");

            // Access flags.
            AccessFlags = fieldInfo.AccessFlags;

            // Descriptor.
            _descriptor = new LazyValue<FieldDescriptor>(() =>
                classImage.ResolveFieldDescriptor(fieldInfo.DescriptorIndex));

            // Extra attributes.
            foreach (var attr in fieldInfo.Attributes)
            {
                string name = classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex);
                switch (name)
                {
                    case SingleIndexAttribute.ConstantValueAttribute:
                        // Constant
                        _constant = new LazyValue<object>(() =>
                        {
                            var contents =
                                SingleIndexAttribute.FromReader(name, new MemoryBigEndianReader(attr.Contents));
                            var constantInfo =
                                classImage.ClassFile.ConstantPool.ResolveConstant(contents.ConstantPoolIndex);
                            switch (constantInfo)
                            {
                                case PrimitiveInfo primitiveInfo:
                                    return primitiveInfo.Value;
                                case StringInfo stringInfo:
                                    return classImage.ClassFile.ConstantPool.ResolveString(stringInfo.StringIndex);
                                default:
                                    return null;
                            }
                        });
                        break;

                    default:
                        // Fall back method:
                        ExtraAttributes.Add(classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex),
                            attr.Clone());
                        break;
                }

            }
        }

        /// <inheritdoc cref="IMemberReference.Name" />
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }
        
        /// <inheritdoc />
        public string FullName => this.GetFieldFullName();

        /// <summary>
        /// Gets or sets the accessibility flags associated to the field.
        /// </summary>
        public FieldAccessFlags AccessFlags
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets the class that defines the field.
        /// </summary>
        public ClassDefinition DeclaringClass
        {
            get;
            internal set;
        }

        /// <inheritdoc />
        ClassReference IMemberReference.DeclaringClass => DeclaringClass;

        /// <summary>
        /// Gets or sets the field descriptor (which includes the field type) of the field.
        /// </summary>
        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        IMemberDescriptor IMemberReference.Descriptor => Descriptor;

        /// <summary>
        /// Gets or sets the constant value associated to the field (if available).
        /// </summary>
        public object Constant
        {
            get => _constant.Value;
            set => _constant.Value = value;
        }

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