using JavaResolver.Class.Constants;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Code
{
    internal class DefaultOperandBuilder : IOperandBuilder
    {
        private readonly ConstantPoolBuffer _buffer;

        public DefaultOperandBuilder(ConstantPoolBuffer buffer)
        {
            _buffer = buffer;
        }

        public int GetConstantIndex(ConstantInfo constantInfo)
        {
            return 0;
        }

        public int GetStringIndex(string text)
        {
            return 0;
        }
    }
}