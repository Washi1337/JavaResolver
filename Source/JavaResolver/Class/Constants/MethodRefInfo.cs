namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents a reference to a method in the constant pool of a Java class file.
    /// </summary>
    public class MethodRefInfo : MemberRefInfo
    {
        /// <summary>
        /// Reads a single method reference at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The method reference that was read.</returns>
        public new static MethodRefInfo FromReader(IBigEndianReader reader)
        {
            var info = new MethodRefInfo
            {
                StartOffset = reader.Position - 1
            };
            info.ReadRemainingFields(reader);
            return info;
        }

        public MethodRefInfo()
        {
        }

        public MethodRefInfo(ushort classIndex, ushort nameAndTypeIndex) 
            : base(classIndex, nameAndTypeIndex)
        {
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.MethodRef;
    }
}