using System;
using JavaResolver.Class.Constants;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides a standard implementation of the operand resolver used by the <see cref="ByteCodeDisassembler"/>.
    /// </summary>
    public class DefaultOperandResolver : IOperandResolver
    {
        private readonly JavaClassFile _classFile;

        public DefaultOperandResolver(JavaClassFile classFile)
        {
            _classFile = classFile ?? throw new ArgumentNullException(nameof(classFile));
        }

        public object ResolveField(int fieldIndex)
        {
            return _classFile.ConstantPool.ResolveConstant(fieldIndex ) as FieldRefInfo;
        }

        public object ResolveMethod(int methodIndex)
        {
            return _classFile.ConstantPool.ResolveConstant(methodIndex) as MethodRefInfo;
        }

        public object ResolveConstant(int constantIndex)
        {
            var constant = _classFile.ConstantPool.ResolveConstant(constantIndex);
            switch (constant)
            {
                case StringInfo stringInfo:
                    var raw = _classFile.ConstantPool.ResolveConstant(stringInfo.StringIndex) as Utf8Info;
                    return raw?.Value;
                case PrimitiveInfo primitiveInfo:
                    return primitiveInfo.Value;
            }

            return constant;
        }

        public object ResolveClass(int classIndex)
        {
            return _classFile.ConstantPool.ResolveConstant(classIndex) as ClassInfo;
        }
    }
}