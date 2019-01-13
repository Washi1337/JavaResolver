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

        internal ClassDefinition(JavaClassImage classImage)
            : base(classImage, (ClassInfo) classImage.ClassFile.ConstantPool.ResolveConstant(classImage.ClassFile.ThisClass))
        {
            _superClass = new LazyValue<ClassReference>(() =>
                classImage.ClassFile.SuperClass != 0
                    ? new ClassReference(classImage,
                        (ClassInfo) classImage.ClassFile.ConstantPool.ResolveConstant(classImage.ClassFile.SuperClass))
                    : null);

            AccessFlags = classImage.ClassFile.AccessFlags;

            foreach (var field in classImage.ClassFile.Fields)
                Fields.Add(new FieldDefinition(classImage, field));

            foreach (var method in classImage.ClassFile.Methods)
                Methods.Add(new MethodDefinition(classImage, method));
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