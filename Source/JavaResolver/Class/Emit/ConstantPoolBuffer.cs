using System;
using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Emit
{
    public class ConstantPoolBuffer
    {
        private readonly IList<ConstantInfo> _constants = new List<ConstantInfo>();
        private readonly IDictionary<ClassInfo, int> _classInfos;
        private readonly IDictionary<Utf8Info, int> _utf8Infos;
        private readonly IDictionary<MemberRefInfo, int> _memberRefInfos = new Dictionary<MemberRefInfo, int>();
        private readonly IDictionary<NameAndTypeInfo, int> _nameAndTypeInfos = new Dictionary<NameAndTypeInfo, int>();
        private readonly IDictionary<StringInfo, int> _stringInfos = new Dictionary<StringInfo, int>();
        private readonly IDictionary<PrimitiveInfo, int> _primitiveInfos = new Dictionary<PrimitiveInfo, int>();

        public ConstantPoolBuffer()
        {
            var comparer = new ConstantInfoComparer();
            _classInfos = new Dictionary<ClassInfo, int>(comparer);
            _utf8Infos = new Dictionary<Utf8Info, int>(comparer);
        }

        private int AddConstant(ConstantInfo info)
        {
            _constants.Add(info);
            return _constants.Count;
        }

        public int GetClassIndex(ClassReference reference)
        {
            var classInfo = new ClassInfo((ushort) GetUtf8Index(reference.Name));
            if (!_classInfos.TryGetValue(classInfo, out int index))
            {
                index = AddConstant(classInfo);
                _classInfos.Add(classInfo, index);
            }

            return index;
        }

        public int GetMemberIndex(IMemberReference reference)
        {
            int classIndex = GetClassIndex(reference.DeclaringClass);
            int nameAndTypeIndex = GetNameAndTypeIndex(reference.Name, reference.Descriptor);

            MemberRefInfo memberRefInfo;
            switch (reference)
            {
                case IField _:
                    memberRefInfo = new FieldRefInfo((ushort) classIndex, (ushort) nameAndTypeIndex);
                    break;
                case IMethod _:
                    memberRefInfo = new MethodRefInfo((ushort) classIndex, (ushort) nameAndTypeIndex);
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!_memberRefInfos.TryGetValue(memberRefInfo, out int index))
            {
                index = AddConstant(memberRefInfo);
                _memberRefInfos.Add(memberRefInfo, index);
            }

            return index;
        }

        public int GetNameAndTypeIndex(string name, IMemberDescriptor descriptor)
        {
            var nameAndTypeInfo = new NameAndTypeInfo(
                (ushort) GetUtf8Index(name),
                (ushort) GetDescriptorIndex(descriptor));

            if (!_nameAndTypeInfos.TryGetValue(nameAndTypeInfo, out int index))
            {
                index = AddConstant(nameAndTypeInfo);
                _nameAndTypeInfos.Add(nameAndTypeInfo, index);
            }

            return index;
        }

        public int GetDescriptorIndex(IMemberDescriptor descriptor)
        {
            return GetUtf8Index(descriptor.Serialize());
        }

        public int GetLiteralIndex(object constant)
        {
            int index;

            if (constant is string text)
            {
                var stringInfo = new StringInfo((ushort) GetUtf8Index(text));
                if (!_stringInfos.TryGetValue(stringInfo, out index))
                {
                    index = AddConstant(stringInfo);
                    _stringInfos.Add(stringInfo, index);
                }
            }
            else
            {
                var primitiveInfo = new PrimitiveInfo(constant);
                if (!_primitiveInfos.TryGetValue(primitiveInfo, out index))
                {
                    index = AddConstant(primitiveInfo);
                    _primitiveInfos.Add(primitiveInfo, index);
                }
            }

            return index;
        }

        public int GetUtf8Index(string text)
        {
            var info = new Utf8Info(text);
            if (!_utf8Infos.TryGetValue(info, out int index))
            {
                index = AddConstant(info);
                _utf8Infos.Add(info, index);
            }

            return index;
        }

        public ConstantPool CreateConstantPool()
        {
            var pool = new ConstantPool();
            foreach (var constantInfo in _constants)
                pool.Constants.Add(constantInfo);
            return pool;
        }
    }
}