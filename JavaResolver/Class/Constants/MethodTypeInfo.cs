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

        public override string ToString()
        {
            return $"MethodTypeInfo (Descriptor: {DescriptorIndex})";
        }
    }
}