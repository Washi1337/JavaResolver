namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents a method handle constant in the constant pool of the Java class file.
    /// </summary>
    public class MethodHandleInfo : ConstantInfo
    {
        /// <summary>
        /// Reads a single method handle constant at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The method handle that was read.</returns>
        public new static MethodHandleInfo FromReader(IBigEndianReader reader)
        {
            return new MethodHandleInfo
            {
                StartOffset = reader.Position - 1,
                ReferenceKind = (MethodReferenceKind) reader.ReadByte(),
                ReferenceIndex = reader.ReadUInt16(),
            };
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.MethodHandle;

        /// <summary>
        /// Gets or sets a value indicating how this method handle is used.
        /// </summary>
        public MethodReferenceKind ReferenceKind
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets an index into the constant pool that references the method that is used.
        /// </summary>
        public ushort ReferenceIndex
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            base.Write(context);
            var writer = context.Writer;
            writer.Write((byte) ReferenceKind);
            writer.Write(ReferenceIndex);
        }

        public override string ToString()
        {
            return $"MethodHandle (Kind: {ReferenceKind}, Index: {ReferenceIndex})";
        }

        protected bool Equals(MethodHandleInfo other)
        {
            return ReferenceKind == other.ReferenceKind && ReferenceIndex == other.ReferenceIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            if (obj.GetType() != this.GetType()) 
                return false;
            return Equals((MethodHandleInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Tag << 24) ^ ((int) ReferenceKind * 397) ^ ReferenceIndex.GetHashCode();
            }
        }
        
    }
}