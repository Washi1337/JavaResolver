using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a reference to a (external) field.  
    /// </summary>
    public class FieldReference : IField
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;
        private readonly LazyValue<ClassReference> _declaringClass;
        
        public FieldReference(string name, ClassReference declaringClass, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _declaringClass = new LazyValue<ClassReference>(declaringClass);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
        }

        internal FieldReference(JavaClassImage classImage, FieldRefInfo fieldRefInfo)
        {
            _name = new LazyValue<string>(() =>
            {
                var constantInfo = classImage.ClassFile.ConstantPool.ResolveConstant(fieldRefInfo.NameAndTypeIndex);
                return constantInfo is NameAndTypeInfo nameAndTypeInfo
                    ? classImage.ClassFile.ConstantPool.ResolveString(nameAndTypeInfo.NameIndex) ??
                      $"<<<INVALID({nameAndTypeInfo.NameIndex})>>>"
                    : $"<<<INVALID({fieldRefInfo.NameAndTypeIndex})>>>";
            });
            
            _descriptor = new LazyValue<FieldDescriptor>(() =>
            {
                var constantInfo = classImage.ClassFile.ConstantPool.ResolveConstant(fieldRefInfo.NameAndTypeIndex);
                return constantInfo is NameAndTypeInfo nameAndTypeInfo
                    ? classImage.ResolveFieldDescriptor(nameAndTypeInfo.DescriptorIndex)
                    : null;
            });

            _declaringClass = new LazyValue<ClassReference>(() =>
                classImage.ResolveClass(fieldRefInfo.ClassIndex));
        }
        
        /// <inheritdoc />
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        /// <inheritdoc />
        public string FullName => this.GetFieldFullName();

        /// <inheritdoc />
        public ClassReference DeclaringClass
        {
            get => _declaringClass.Value;
            set => _declaringClass.Value = value;
        }

        /// <summary>
        /// Gets or sets the field descriptor (which contains the field type) of the referenced field.
        /// </summary>
        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        IMemberDescriptor IMemberReference.Descriptor => Descriptor;

        /// <inheritdoc />
        public override string ToString()
        {
            return FullName;
        }
    }
}