namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents metadata about a dynamic invocation (such as an invocation of a virtual method) in a Java class file.   
    /// </summary>
    public class InvokeDynamicInfo : ConstantInfo
    {
        /// <summary>
        /// Reads a single dynamic invocation entry at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The entry that was read.</returns>
        public new static InvokeDynamicInfo FromReader(IBigEndianReader reader)
        {
            return new InvokeDynamicInfo
            {
                StartOffset = reader.Position - 1,
                BootstrapMethodIndex = reader.ReadUInt16(),
                NameAndTypeIndex = reader.ReadUInt16(),
            };
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.InvokeDynamic;

        /// <summary>
        /// Gets or sets the index into the constant pool that references the method to invoke.
        /// </summary>
        public ushort BootstrapMethodIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index into the constant pool that references the name and type constant associated to the dynamic invoke.
        /// </summary>
        public ushort NameAndTypeIndex
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            base.Write(context);
            var writer = context.Writer;
            writer.Write(BootstrapMethodIndex);
            writer.Write(NameAndTypeIndex);
        }

        public override string ToString()
        {
            return $"InvokeDynamicInfo (Bootstrap: {BootstrapMethodIndex}, NameAndType: {NameAndTypeIndex})";
        }

        protected bool Equals(InvokeDynamicInfo other)
        {
            return BootstrapMethodIndex == other.BootstrapMethodIndex && NameAndTypeIndex == other.NameAndTypeIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((InvokeDynamicInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Tag << 24) ^ (BootstrapMethodIndex.GetHashCode() * 397) ^ NameAndTypeIndex.GetHashCode();
            }
        }
    }
}