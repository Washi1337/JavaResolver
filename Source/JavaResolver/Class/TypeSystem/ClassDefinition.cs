using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem.Collections;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a single class defined in a Java class file.
    /// </summary>
    public class ClassDefinition : ClassReference
    {
        private readonly LazyValue<ClassReference> _superClass;
        
        public ClassDefinition(string name)
            : this(name, null)
        {
        }

        public ClassDefinition(string name, ClassReference superClass)
            : base(name)
        {
            _superClass = new LazyValue<ClassReference>(superClass);
            Fields = new FieldCollection(this);
            Methods = new MethodCollection(this);
        }

        internal ClassDefinition(JavaClassImage classImage)
            : base(classImage, (ClassInfo) classImage.ClassFile.ConstantPool.ResolveConstant(classImage.ClassFile.ThisClass))
        {
            Image = classImage;
            
            // Super class
            _superClass = new LazyValue<ClassReference>(() =>
                classImage.ClassFile.SuperClass != 0
                    ? new ClassReference(classImage,
                        (ClassInfo) classImage.ClassFile.ConstantPool.ResolveConstant(classImage.ClassFile.SuperClass))
                    : null);

            // Flags
            AccessFlags = classImage.ClassFile.AccessFlags;

            // Fields
            Fields = new FieldCollection(this);
            foreach (var field in classImage.ClassFile.Fields)
                Fields.Add(new FieldDefinition(classImage, field));

            // Methods
            Methods = new MethodCollection(this);
            foreach (var method in classImage.ClassFile.Methods)
                Methods.Add(new MethodDefinition(classImage, method));
        }

        /// <summary>
        /// Gets the image of the file the class is defined in.
        /// </summary>
        public JavaClassImage Image
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the super class associated to this class.
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
        }

        /// <summary>
        /// Gets a collection of methods that this class defines.
        /// </summary>
        public IList<MethodDefinition> Methods
        {
            get;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}