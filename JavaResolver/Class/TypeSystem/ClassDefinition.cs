using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem.Collections;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a single class defined in a Java class file.
    /// </summary>
    public class ClassDefinition : ClassReference, IExtraAttributeProvider
    {
        private readonly LazyValue<ClassReference> _superClass;
        private readonly LazyValue<string> _sourceFile = new LazyValue<string>();
        
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

            // Attributes
            foreach (var attr in classImage.ClassFile.Attributes)
            {
                string name = classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex);
                switch (name)
                {
                    // Source file
                    case SingleIndexAttribute.SourceFileAttribute:
                        _sourceFile = new LazyValue<string>(() =>
                        {
                            var sourceFile = SingleIndexAttribute.FromReader(name, new MemoryBigEndianReader(attr.Contents));
                            return classImage.ClassFile.ConstantPool.ResolveString(sourceFile.ConstantPoolIndex);
                        });
                        break;
                    
                    default:
                        ExtraAttributes.Add(name, attr.Clone());
                        break;
                }    
            }
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
        /// Gets or sets the path to the Java source file the class was originally declared in (if available). 
        /// </summary>
        public string SourceFile
        {
            get => _sourceFile.Value;
            set => _sourceFile.Value = value;
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
        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        }= new Dictionary<string, AttributeInfo>();

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}