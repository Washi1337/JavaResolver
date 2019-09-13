using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    public class DynamicInvocation
    {
        private readonly LazyValue<BootstrapMethod> _bootstrapMethod;
        private readonly LazyValue<string> _methodName;
        private readonly LazyValue<MethodDescriptor> _methodDescriptor;

        public DynamicInvocation(BootstrapMethod bootstrapMethod, string name, MethodDescriptor descriptor)
        {
            _bootstrapMethod = new LazyValue<BootstrapMethod>(bootstrapMethod);
            _methodName = new LazyValue<string>(name);
            _methodDescriptor = new LazyValue<MethodDescriptor>(descriptor);
        }
        
        internal DynamicInvocation(JavaClassImage image, InvokeDynamicInfo dynamicInfo)
        {
            _bootstrapMethod = new LazyValue<BootstrapMethod>(
                () => image.ResolveBootstrapMethod(dynamicInfo.BootstrapMethodIndex));

            if (image.ClassFile.ConstantPool.ResolveConstant(dynamicInfo.NameAndTypeIndex) is NameAndTypeInfo nameAndType)
            {
                _methodName = new LazyValue<string>(
                    () => image.ClassFile.ConstantPool.ResolveString(nameAndType.NameIndex));
                _methodDescriptor = new LazyValue<MethodDescriptor>(
                    () => MethodDescriptor.FromString(image.ClassFile.ConstantPool.ResolveString(nameAndType.DescriptorIndex)));
            }
            else
            {
                _methodName = new LazyValue<string>();
                _methodDescriptor = new LazyValue<MethodDescriptor>();
            }
        }

        public BootstrapMethod BootstrapMethod
        {
            get => _bootstrapMethod.Value;
            set => _bootstrapMethod.Value = value;
        }

        public string MethodName
        {
            get => _methodName.Value;
            set => _methodName.Value = value;
        }

        public MethodDescriptor MethodDescriptor
        {
            get => _methodDescriptor.Value;
            set => _methodDescriptor.Value = value;
        }

        public override string ToString()
        {
            return
                $"{MethodDescriptor.ReturnType} {MethodName}({string.Join(", ", MethodDescriptor.ParameterTypes)}) (Bootstrap: {BootstrapMethod})";
        }
    }
}