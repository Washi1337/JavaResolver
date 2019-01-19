using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    public class LocalVariable
    {
        private readonly LazyValue<string> _name;
        private readonly LazyValue<FieldDescriptor> _descriptor;
        
        public LocalVariable(string name, FieldDescriptor descriptor)
        {
            _name = new LazyValue<string>(name);
            _descriptor = new LazyValue<FieldDescriptor>(descriptor);
        }

        internal LocalVariable(JavaClassImage classImage, MethodBody body, LocalVariableInfo variableInfo)
        {
            _name = new LazyValue<string>(() => 
                classImage.ClassFile.ConstantPool.ResolveString(variableInfo.NameIndex));

            _descriptor = new LazyValue<FieldDescriptor>(() =>
                classImage.ResolveFieldDescriptor(variableInfo.DescriptorIndex));

            Index = variableInfo.LocalIndex;
            Start = body.Instructions.GetByOffset(variableInfo.StartOffset);
            End = body.Instructions.GetByOffset(variableInfo.StartOffset + variableInfo.Length);
        }
        
        public string Name
        {
            get => _name.Value;
            set => _name.Value = value;
        }

        public int Index
        {
            get;
            set;
        }

        public FieldDescriptor Descriptor
        {
            get => _descriptor.Value;
            set => _descriptor.Value = value;
        }
        
        public ByteCodeInstruction Start
        {
            get;
            set;
        }

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