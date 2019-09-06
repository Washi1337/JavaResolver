namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents a single entry in an inner classes attribute, describing two classes of which one is nested in the other. 
    /// </summary>
    public class InnerClassInfo
    {
        /// <summary>
        /// Reads a single inner class entry at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The entry that was read.</returns>
        public static InnerClassInfo FromReader(IBigEndianReader reader)
        {
            return new InnerClassInfo
            {
                InnerClassInfoIndex = reader.ReadUInt16(),
                OuterClassInfoIndex = reader.ReadUInt16(),
                NameIndex = reader.ReadUInt16(),
                InnerClassAccessFlags = (ClassAccessFlags) reader.ReadUInt16(),
            };
        }

        /// <summary>
        /// Gets or sets the raw index into the constant pool referencing the class info describing the class that is
        /// nested into the other.
        /// </summary>
        public ushort InnerClassInfoIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the raw index into the constant pool referencing the class info describing the class that is
        /// defining the nested class.
        /// </summary>
        public ushort OuterClassInfoIndex
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the raw index into the constant pool referencing the original simple name of the nested class.
        /// </summary>
        public ushort NameIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the access flags of the nested class.
        /// </summary>
        public ClassAccessFlags InnerClassAccessFlags
        {
            get;
            set;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Inner: {InnerClassInfoIndex}, Outer: {OuterClassInfoIndex}, Name: {NameIndex}, AccessFlags: {InnerClassAccessFlags}";
        }
        
        /// <summary>
        /// Writes the inner class info to an output stream. 
        /// </summary>
        /// <param name="writer">The writer representing the output stream.</param>
        public void Write(IBigEndianWriter writer)
        {
            writer.Write(InnerClassInfoIndex);
            writer.Write(OuterClassInfoIndex);
            writer.Write(NameIndex);
            writer.Write((ushort) InnerClassAccessFlags);
        }
        
    }
}