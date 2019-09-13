namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents metadata containing the type of a method stored in the constant pool of a Java class file.
    /// </summary>
    public class MethodTypeInfo : ConstantInfo
    {
        /// <summary>
        /// Reads a single method type at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The method type that was read.</returns>
        public new static MethodTypeInfo FromReader(IBigEndianReader reader)
        {
            return new MethodTypeInfo
            {
                StartOffset = reader.Position - 1,
                DescriptorIndex = reader.ReadUInt16(),
            };
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.MethodType;

        /// <summary>
        /// Gets or sets the index into the constant pool referencing the method descriptor to use.
        /// </summary>
        public ushort DescriptorIndex
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            base.Write(context);
            context.Writer.Write(DescriptorIndex);
        }

        public override string ToString()
        {
            return $"MethodTypeInfo (Descriptor: {DescriptorIndex})";
        }

        protected bool Equals(MethodTypeInfo other)
        {
            return DescriptorIndex == other.DescriptorIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType()) 
                return false;
            return Equals((MethodTypeInfo) obj);
        }

        public override int GetHashCode()
        {
            return ((int) Tag << 24) ^ DescriptorIndex.GetHashCode();
        }
    }
}