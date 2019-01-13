using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a single class defined in a Java class file.
    /// </summary>
    public class ClassDefinition : ClassReference, IExtraAttributeProvider
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

            foreach (var attr in classImage.ClassFile.Attributes)
                ExtraAttributes.Add(classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex), attr.Clone());
        }

        /// <summary>
        /// Gest or sets the super class associated to this class.
        /// </summary>
        public ClassReference SuperClass
        {
            get => _superClass.Value;
            set => _superClass.Value = value;
        }

        /// <summary>
        /// Gets or sets the accessibility flags associated to this class.
        /// </summary>
        public ClassAccessFlags AccessFlags
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of fields that this class defines.
        /// </summary>
        public IList<FieldDefinition> Fields
        {
            get;
        } = new List<FieldDefinition>();

        /// <summary>
        /// Gets a collection of methods that this class defines.
        /// </summary>
        public IList<MethodDefinition> Methods
        {
            get;
        } = new List<MethodDefinition>();

        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        }= new Dictionary<string, AttributeInfo>();
    }
}