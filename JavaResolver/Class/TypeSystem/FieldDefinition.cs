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
    public class FieldDefinition : IExtraAttributeProvider
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;
        private readonly LazyValue<object> _constant;

        public FieldDefinition(string name, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
            _constant = new LazyValue<object>(null);
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
                    case ConstantValueAttribute.AttributeName:
                        // Constant
                        _constant = new LazyValue<object>(() =>
                        {
                            var contents = ConstantValueAttribute.FromReader(new MemoryBigEndianReader(attr.Contents));
                            var constantInfo = classImage.ClassFile.ConstantPool.ResolveConstant(contents.ConstantValueIndex);
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
                        ExtraAttributes.Add(classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex), attr.Clone());
                        break;
                }
                
            }
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
        
        /// <summary>
        /// Gets or sets the constant value associated to the field (if available).
        /// </summary>
        public object Constant
        {
            get => _constant.Value;
            set => _constant.Value = value;
        }

        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
    }
}