using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    public class MethodReference : IMemberReference
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<MethodDescriptor> _descriptor;
        private readonly LazyValue<ClassReference> _declaringClass;

        public MethodReference(string name, ClassReference declaringClass, MethodDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _declaringClass = new LazyValue<ClassReference>(declaringClass);
            _descriptor = new LazyValue<MethodDescriptor>(descriptor);
        }

        internal MethodReference(JavaClassImage classImage, MethodRefInfo methodRefInfo)
        {
            _name = new LazyValue<string>(() =>
            {
                var constantInfo = classImage.ClassFile.ConstantPool.ResolveConstant(methodRefInfo.NameAndTypeIndex);
                return constantInfo is NameAndTypeInfo nameAndTypeInfo
                    ? classImage.ClassFile.ConstantPool.ResolveString(nameAndTypeInfo.NameIndex) ??
                      $"<<<INVALID({nameAndTypeInfo.NameIndex})>>>"
                    : $"<<<INVALID({methodRefInfo.NameAndTypeIndex})>>>";
            });

            _descriptor = new LazyValue<MethodDescriptor>(() =>
            {
                var constantInfo = classImage.ClassFile.ConstantPool.ResolveConstant(methodRefInfo.NameAndTypeIndex);
                return constantInfo is NameAndTypeInfo nameAndTypeInfo
                    ? classImage.ResolveMethodDescriptor(nameAndTypeInfo.DescriptorIndex)
                    : null;
            });

            _declaringClass = new LazyValue<ClassReference>(() =>
                classImage.ResolveClass(methodRefInfo.ClassIndex));
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

        public MethodDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }

        public string FullName =>
            $"{Descriptor.ReturnType} {DeclaringClass}::{Name}({string.Join(", ", Descriptor.ParameterTypes)})";

        IMemberDescriptor IMemberReference.Descriptor => Descriptor;

        public override string ToString()
        {
            return FullName;
        }
    }
}