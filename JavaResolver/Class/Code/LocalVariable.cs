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

            Start = body.Instructions.GetByOffset(variableInfo.StartOffset);
            End = body.Instructions.GetByOffset(variableInfo.StartOffset + variableInfo.Length);
        }
        
        public string Name
        {
            get;
            set;
        }

        public FieldDescriptor Descriptor
        {
            get;
            set;
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