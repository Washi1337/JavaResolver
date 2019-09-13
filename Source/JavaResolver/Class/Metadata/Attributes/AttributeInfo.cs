namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Represents raw metadata describing an attribute used in a Java class file.
    /// </summary>
    public class AttributeInfo : FileSegment
    {
        /// <summary>
        /// Reads a single attribute at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader ot use.</param>
        /// <returns>The attribute that was read.</returns>
        public static AttributeInfo FromReader(IBigEndianReader reader)
        {
            return new AttributeInfo
            {
                StartOffset = reader.Position,
                NameIndex = reader.ReadUInt16(),
                Contents = reader.ReadBytes(reader.ReadInt32())
            };
        }

        public AttributeInfo()
        {
        }
        
        public AttributeInfo(ushort nameIndex, byte[] contents)
        {
            NameIndex = nameIndex;
            Contents = contents;
        }
        
        /// <summary>
        /// Gets or sets an index into the constant pool that references the name of the attribute.
        /// </summary>
        public ushort NameIndex
        {
            get;
            set;
        }

        // TODO: deserialize
        public byte[] Contents
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            var writer = context.Writer;
            writer.Write(NameIndex);
            writer.Write(Contents.Length);
            writer.Write(Contents);
        }

        public AttributeInfo Clone()
        {
            return new AttributeInfo {Contents = Contents, NameIndex = NameIndex};
        }
        
        public override string ToString()
        {
            return $"Attribute (Name: {NameIndex})";
        }
    }
}