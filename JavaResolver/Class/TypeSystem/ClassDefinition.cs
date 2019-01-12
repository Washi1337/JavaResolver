using System.Collections.Generic;
using JavaResolver.Class.Constants;

namespace JavaResolver.Class.TypeSystem
{
    public class ClassDefinition : ClassReference
    {
        private readonly LazyValue<ClassReference> _superClass;
        
        public ClassDefinition(string name)
            : base(name)
        {
            _superClass = new LazyValue<ClassReference>();
        }

        public ClassDefinition(JavaClassFile classFile)
            : base(classFile, (ClassInfo) classFile.ConstantPool.ResolveConstant(classFile.ThisClass))
        {
            _superClass = new LazyValue<ClassReference>(() =>
                classFile.SuperClass != 0
                    ? new ClassReference(classFile,
                        (ClassInfo) classFile.ConstantPool.ResolveConstant(classFile.SuperClass))
                    : null);

            AccessFlags = classFile.AccessFlags;

            foreach (var field in classFile.Fields)
                Fields.Add(new FieldDefinition(classFile, field));

            foreach (var method in classFile.Methods)
                Methods.Add(new MethodDefinition(classFile, method));
        }

        public ClassReference SuperClass
        {
            get => _superClass.Value;
            set => _superClass.Value = value;
        }

        public ClassAccessFlags AccessFlags
        {
            get;
            set;
        }

        public IList<FieldDefinition> Fields
        {
            get;
        } = new List<FieldDefinition>();

        public IList<MethodDefinition> Methods
        {
            get;
        } = new List<MethodDefinition>();
    }
}