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
                StartOffset = reader.Position - 1,
                NameIndex = reader.ReadUInt16()
            };
        }

        public ClassInfo()
        {
        }

        public ClassInfo(ushort nameIndex)
        {
            NameIndex = nameIndex;
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

        public override void Write(WritingContext context)
        {
            base.Write(context);
            context.Writer.Write(NameIndex);
        }

        protected bool Equals(ClassInfo other)
        {
            return NameIndex == other.NameIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType()) 
                return false;
            return Equals((ClassInfo) obj);
        }

        public override int GetHashCode()
        {
            return ((int) Tag << 24) ^ NameIndex.GetHashCode();
        }
        
    }
}