namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Represents raw metadata describing an attribute used in a Java class file.
    /// </summary>
    public class AttributeInfo 
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
                AttributeNameIndex = reader.ReadUInt16(),
                Info = reader.ReadBytes(reader.ReadInt32())
            };
        }

        /// <summary>
        /// Gets or sets an index into the constant pool that references the name of the attribute.
        /// </summary>
        public ushort AttributeNameIndex
        {
            get;
            set;
        }

        // TODO: deserialize
        public byte[] Info
        {
            get;
            set;
        }
    }
}