using System;
using JavaResolver.Class.Constants;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides a standard implementation of the operand resolver used by the <see cref="ByteCodeDisassembler"/>.
    /// </summary>
    public class DefaultOperandResolver : IOperandResolver
    {
        private readonly JavaClassImage _classImage;

        public DefaultOperandResolver(JavaClassImage classImage)
        {
            _classImage = classImage ?? throw new ArgumentNullException(nameof(classImage));
        }

        public FieldReference ResolveField(int fieldIndex)
        {
            return _classImage.ResolveField(fieldIndex);
        }

        public MethodReference ResolveMethod(int methodIndex)
        {
            return _classImage.ResolveMethod(methodIndex);
        }

        public object ResolveConstant(int constantIndex)
        {
            var constant = _classImage.ClassFile.ConstantPool.ResolveConstant(constantIndex);
            switch (constant)
            {
                case StringInfo stringInfo:
                    var raw = _classImage.ClassFile.ConstantPool.ResolveConstant(stringInfo.StringIndex) as Utf8Info;
                    return raw?.Value;
                case PrimitiveInfo primitiveInfo:
                    return primitiveInfo.Value;
            }

            return constant;
        }

        public ClassReference ResolveClass(int classIndex)
        {
            return _classImage.ResolveClass(classIndex);
        }

        public DynamicInvocation ResolveDynamic(int dynamicIndex)
        {
            return _classImage.ResolveDynamicInvoke(dynamicIndex);
        }
        
    }
}