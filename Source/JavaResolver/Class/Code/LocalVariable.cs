using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a single local variable defined in a method body of a Java class file.
    /// </summary>
    public class LocalVariable
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;
        
        public LocalVariable(string name, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
        }

        internal LocalVariable(JavaClassImage classImage, ByteCodeMethodBody body, LocalVariableInfo variableInfo)
        {
            _name = new LazyValue<string>(() => 
                classImage.ClassFile.ConstantPool.ResolveString(variableInfo.NameIndex));

            _descriptor = new LazyValue<FieldDescriptor>(() =>
                classImage.ResolveFieldDescriptor(variableInfo.DescriptorIndex));

            Index = variableInfo.LocalIndex;
            Start = body.Instructions.GetByOffset(variableInfo.StartOffset);
            End = body.Instructions.GetByOffset(variableInfo.StartOffset + variableInfo.Length);
        }
        
        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        /// <summary>
        /// Gets or sets the index of the variable.
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the descriptor of the variable. This includes the variable type.
        /// </summary>
        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }
        
        /// <summary>
        /// Gets or sets the first instruction of the range inside the method body where the variable is accessible. 
        /// </summary>
        /// <remarks>
        /// This instruction must appear before the instruction stored in the <see cref="End"/> property.
        /// </remarks>
        public ByteCodeInstruction Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last instruction of the range inside the method body where the variable is accessible. 
        /// </summary>
        /// <remarks>
        /// This instruction must appear after the instruction stored in the <see cref="Start"/> property.
        /// When this value is <c>null</c>, the end of the method is specified. 
        /// </remarks>
        public ByteCodeInstruction End
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{Descriptor.FieldType} {Name}";
        }
    }
}