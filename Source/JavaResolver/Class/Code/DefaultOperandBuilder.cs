using JavaResolver.Class.Emit;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    internal class DefaultOperandBuilder : IOperandBuilder
    {
        private readonly JavaClassFileBuilder _builder;

        public DefaultOperandBuilder(JavaClassFileBuilder builder)
        {
            _builder = builder;
        }
        
        public int GetFieldIndex(FieldReference reference)
        {
            return _builder.ConstantPoolBuffer.GetMemberIndex(reference);
        }

        public int GetMethodIndex(MethodReference reference)
        {
            return _builder.ConstantPoolBuffer.GetMemberIndex(reference);
        }

        public int GetClassIndex(ClassReference reference)
        {
            return _builder.ConstantPoolBuffer.GetClassIndex(reference);
        }

        public int GetDynamicIndex(DynamicInvocation invocation)
        {
            return _builder.ConstantPoolBuffer.GetDynamicInvocationIndex(invocation,
                _builder.GetBootstrapMethodIndex(invocation.BootstrapMethod));
        }

        public int GetLiteralIndex(object constant)
        {
            return _builder.ConstantPoolBuffer.GetLiteralIndex(constant);
        }

        public int GetStringIndex(string text)
        {
            return 0;
        }
    }
}