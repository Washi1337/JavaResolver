using System;
using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata.Attributes;

namespace JavaResolver.Class.TypeSystem
{
    public class BootstrapMethod
    {
        private readonly LazyValue<MethodHandle> _handle;

        public BootstrapMethod(MethodHandle method, params object[] arguments)
        {
            _handle = new LazyValue<MethodHandle>(method);
            Arguments = new List<object>(arguments);
        }
        
        internal BootstrapMethod(JavaClassImage image, BootstrapMethodInfo info)
        {
            _handle = new LazyValue<MethodHandle>(() =>
            {
                var constantInfo = image.ClassFile.ConstantPool.ResolveConstant(info.MethodRefIndex);
                return constantInfo is MethodHandleInfo methodHandleInfo
                    ? new MethodHandle(image, methodHandleInfo)
                    : null;
            });
            
            Arguments = new List<object>(info.Arguments.Count);
            foreach (ushort argIndex in info.Arguments)
                Arguments.Add(ResolveArgument(image, argIndex));
        }

        private static object ResolveArgument(JavaClassImage image, ushort index)
        {
            var constantInfo = image.ClassFile.ConstantPool.ResolveConstant(index);
            switch (constantInfo.Tag)
            {
                case ConstantPoolTag.String:
                    constantInfo = image.ClassFile.ConstantPool.ResolveConstant(index);
                    return constantInfo is StringInfo stringInfo
                        ? image.ClassFile.ConstantPool.ResolveString(stringInfo.StringIndex)
                        : null;

                case ConstantPoolTag.Class:
                    return image.ResolveClass(index);
                
                case ConstantPoolTag.Integer:
                case ConstantPoolTag.Long:
                case ConstantPoolTag.Float:
                case ConstantPoolTag.Double:
                    return ((PrimitiveInfo) constantInfo).Value;
                
                case ConstantPoolTag.MethodType:
                    var methodType = (MethodTypeInfo) constantInfo;
                    string rawDescriptor = image.ClassFile.ConstantPool.ResolveString(methodType.DescriptorIndex);
                    return MethodDescriptor.FromString(rawDescriptor);
                
                case ConstantPoolTag.MethodHandle:
                    throw new NotImplementedException();
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public MethodHandle Handle
        {
            get => _handle.Value;
            set => _handle.Value = value;
        }

        public IList<object> Arguments
        {
            get;
        }

        public override string ToString()
        {
            return $"{nameof(Handle)}: {Handle}, {nameof(Arguments)}: {{{string.Join(", ", Arguments)}}}";
        }
    }
}