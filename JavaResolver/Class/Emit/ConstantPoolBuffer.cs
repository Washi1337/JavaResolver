using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Emit
{
    public class ConstantPoolBuffer
    {
        private readonly IList<ConstantInfo> _constants = new List<ConstantInfo>();
        private readonly IDictionary<ClassInfo, int> _classInfos;
        private readonly IDictionary<Utf8Info, int> _utf8Infos;

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