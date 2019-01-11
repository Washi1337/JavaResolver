namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents metadata stored in the constant pool of a Java class file, used to specify a name and a descriptor
    /// of a member defined or referenced.
    /// </summary>
    public class NameAndTypeInfo : ConstantInfo
    {
        /// <summary>
        /// Reads a single name and type pair at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The pair that was read.</returns>
        public new static NameAndTypeInfo FromReader(IBigEndianReader reader)
        {
            return new NameAndTypeInfo
            {
                StartOffset = reader.Position - 1,
                NameIndex = reader.ReadUInt16(),
                DescriptorIndex = reader.ReadUInt16()
            };
        }
        
        public NameAndTypeInfo()
        {
        }
        
        public NameAndTypeInfo(ushort nameIndex, ushort descriptorIndex)
        {
            NameIndex = nameIndex;
            DescriptorIndex = descriptorIndex;
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.NameAndType;

        /// <summary>
        /// Gets or sets an index into the constant pool that references the name of the member.
        /// </summary>
        public ushort NameIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an index into the constant pool that references the descriptor of the member.
        /// </summary>
        public ushort DescriptorIndex
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            base.Write(context);
            var writer = context.Writer;
            writer.Write(NameIndex);
            writer.Write(DescriptorIndex);
        }

        public override string ToString()
        {
            return $"NameAndTypeInfo (Name: {NameIndex}, Descriptor: {DescriptorIndex})";
        }
    }
}