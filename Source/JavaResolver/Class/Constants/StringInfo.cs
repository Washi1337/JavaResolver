namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents a string stored in the constant pool of a Java class file.
    /// </summary>
    public class StringInfo : ConstantInfo
    {
        /// <summary>
        /// Reads a single string at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The string that was read.</returns>
        public new static StringInfo FromReader(IBigEndianReader reader)
        {
            return new StringInfo
            {
                StartOffset = reader.Position - 1,
                StringIndex = reader.ReadUInt16()
            };
        }

        public StringInfo()
        {
        }
        
        public StringInfo(ushort stringIndex)
        {
            StringIndex = stringIndex;
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.String;

        /// <summary>
        /// Gets or sets an index into the constant pool that references the UTF8 string used.
        /// </summary>
        public ushort StringIndex
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            base.Write(context);
            context.Writer.Write(StringIndex);
        }

        public override string ToString()
        {
            return $"String (String: {StringIndex})";
        }

        protected bool Equals(StringInfo other)
        {
            return StringIndex == other.StringIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            if (obj.GetType() != this.GetType()) 
                return false;
            return Equals((StringInfo) obj);
        }

        public override int GetHashCode()
        {
            return ((int) Tag << 24) ^ StringIndex.GetHashCode();
        }
    }
}