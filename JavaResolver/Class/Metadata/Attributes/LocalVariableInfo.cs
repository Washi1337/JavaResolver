namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Provides a raw representation of a single local variable stored in a local variables table attribute
    /// associated to a method body.
    /// </summary>
    public class LocalVariableInfo
    {
        /// <summary>
        /// Reads a raw local variable structure at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The local variable that was read.</returns>
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

        /// <summary>
        /// Gets or sets the starting offset of the instruction range this variable is accessible in.
        /// </summary>
        public ushort StartOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the length (in bytes) of the instruction range this variable is accessible in. 
        /// </summary>
        public ushort Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the raw index into the constant pool referencing the name of the variable.
        /// </summary>
        public ushort NameIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the raw index into the constant pool referencing the descriptor of the variable.
        /// </summary>
        public ushort DescriptorIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the local index of the variable.
        /// </summary>
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