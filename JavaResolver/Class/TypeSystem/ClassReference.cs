using JavaResolver.Class.Constants;

namespace JavaResolver.Class.TypeSystem
{
    public class ClassReference
    {
        private readonly LazyValue<string> _name;

        public ClassReference(string name)
        {
            _name = new LazyValue<string>(name);
        }

        internal ClassReference(JavaClassFile classFile, ClassInfo classInfo)
        {
            _name = new LazyValue<string>(() =>
            {
                var constantInfo = classFile.ConstantPool.ResolveConstant(classInfo.NameIndex);
                return constantInfo is Utf8Info utf8Info ? utf8Info.Value : $"<<<INVALID({classInfo.NameIndex})>>>";
            });
        }
        
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }
    }
}