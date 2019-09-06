using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a reference to a (external) method.
    /// </summary>
    public class MethodReference : IMethod
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

        /// <inheritdoc />
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        /// <inheritdoc />
        public string FullName => this.GetMethodFullName();
        
        /// <inheritdoc />
        public ClassReference DeclaringClass
        {
            get => _declaringClass.Value;
            set => _declaringClass.Value = value;
        }

        /// <summary>
        /// Gets or sets the method descriptor, which includes the return type as well as its parameter types of the
        /// referenced method.
        /// </summary>
        public MethodDescriptor Descriptor
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