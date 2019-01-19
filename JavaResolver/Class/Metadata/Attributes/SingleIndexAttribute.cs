using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    public class SingleIndexAttribute : IAttributeContents
    {
        public const string ConstantValueAttribute = "ConstantValue";
        public const string SourceFileAttribute = "SourceFile";

        public static SingleIndexAttribute FromReader(string name, IBigEndianReader reader)
        {
            return new SingleIndexAttribute(name, reader.ReadUInt16());
        }

        public SingleIndexAttribute(string name, ushort constantPoolIndex)
        {
            Name = name;
            ConstantPoolIndex = constantPoolIndex;
        }
        
        public string Name
        {
            get;
        }

        public ushort ConstantPoolIndex
        {
            get;
        }

        public byte[] Serialize(BuildingContext context)
        {
            ushort value = ConstantPoolIndex;
            return new byte[2]
            {
                (byte) ((value >> 8) & 0xFF),
                (byte) (value & 0xFF)
            };
        }
    }
}