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
                BootstrapMethodAttrIndex = reader.ReadUInt16(),
                NameAndTypeIndex = reader.ReadInt16(),
            };
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.InvokeDynamic;

        /// <summary>
        /// Gets or sets the index into the constant pool that references the method to invoke.
        /// </summary>
        public ushort BootstrapMethodAttrIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index into the constant pool that references the name and type constant associated to the dynamic invoke.
        /// </summary>
        public short NameAndTypeIndex
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"InvokeDynamicInfo (Bootstrap: {BootstrapMethodAttrIndex}, NameAndType: {NameAndTypeIndex})";
        }
    }
}