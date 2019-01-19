namespace JavaResolver.Class.Metadata.Attributes
{
    public class LocalVariableInfo
    {
        public static LocalVariableInfo FromReader(IBigEndianReader reader)
        {
            return new LocalVariableInfo
            {
                StartOffset = reader.ReadUInt16(),
                Length = reader.ReadUInt16(),
                NameIndex = reader.ReadUInt16(),
                DescriptorIndex = reader.ReadUInt16(),
                LocalIndex = reader.ReadUInt16(),
            };
        }

        public ushort StartOffset
        {
            get;
            set;
        }

        public ushort Length
        {
            get;
            set;
        }

        public ushort NameIndex
        {
            get;
            set;
        }

        public ushort DescriptorIndex
        {
            get;
            set;
        }

        public ushort LocalIndex
        {
            get;
            set;
        }

        public void Write(IBigEndianWriter writer)
        {
            writer.Write(StartOffset);
            writer.Write(Length);
            writer.Write(NameIndex);
            writer.Write(DescriptorIndex);
            writer.Write(LocalIndex);
        }
    }
}