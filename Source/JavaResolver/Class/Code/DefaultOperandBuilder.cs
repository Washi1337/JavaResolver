using JavaResolver.Class.Emit;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    internal class DefaultOperandBuilder : IOperandBuilder
    {
        private readonly ConstantPoolBuffer _buffer;

        public DefaultOperandBuilder(ConstantPoolBuffer buffer)
        {
            _buffer = buffer;
        }
        
        public int GetFieldIndex(FieldReference reference)
        {
            return _buffer.GetMemberIndex(reference);
        }

        public int GetMethodIndex(MethodReference reference)
        {
            return _buffer.GetMemberIndex(reference);
        }

        public int GetClassIndex(ClassReference reference)
        {
            return _buffer.GetClassIndex(reference);
        }

        public int GetLiteralIndex(object constant)
        {
            return _buffer.GetLiteralIndex(constant);
        }

        public int GetStringIndex(string text)
        {
            return 0;
        }
    }
}