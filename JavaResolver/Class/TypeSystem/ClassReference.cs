using JavaResolver.Class.Constants;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a reference to a (external) class.
    /// </summary>
    public class ClassReference
    {
        private readonly LazyValue<string> _name;

        public ClassReference(string name)
        {
            _name = new LazyValue<string>(name);
        }

        internal ClassReference(JavaClassImage classImage, ClassInfo classInfo)
        {
            _name = new LazyValue<string>(() =>
                classImage.ClassFile.ConstantPool.ResolveString(classInfo.NameIndex) ?? $"<<<INVALID({classInfo.NameIndex})>>>");
        }
        
        /// <summary>
        /// Gets or sets the name of the referenced class.
        /// </summary>
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }
    }
}