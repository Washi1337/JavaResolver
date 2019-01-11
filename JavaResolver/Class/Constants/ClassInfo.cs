namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents information about a class stored in the constant pool of a Java class file.   
    /// </summary>
    public class ClassInfo : ConstantInfo
    {
        /// <summary>
        /// Reads a class info structure at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The class info that was read.</returns>
        /// <remarks>
        /// This method assumes the tag was already read from the reader.
        /// </remarks>
        public new static ClassInfo FromReader(IBigEndianReader reader)
        {
            return new ClassInfo
            {
                NameIndex = reader.ReadUInt16()
            };
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.Class;
        
        /// <summary>
        /// Gets or sets the index into the constant pool that references the name of the class.
        /// </summary>
        public ushort NameIndex
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "Class (Name: " + NameIndex + ")";
        }
    }
}