using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    public class FieldReference : IMemberReference
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
        
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        public ClassReference DeclaringClass
        {
            get => _declaringClass.Value;
            set => _declaringClass.Value = value;
        }

        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        IMemberDescriptor IMemberReference.Descriptor => Descriptor;

        public string FullName => DeclaringClass.Name + "::" + Name;

        public override string ToString()
        {
            return Descriptor + " " +  FullName;
        }
    }
}