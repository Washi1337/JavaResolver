namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents the raw structure of a reference to an interface method in a Java class file. 
    /// </summary>
    public class InterfaceMethodRefInfo : MemberRefInfo
    {
        /// <summary>
        /// Reads a single interface method reference at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The interface method reference that was read.</returns>
        public new static InterfaceMethodRefInfo FromReader(IBigEndianReader reader)
        {
            var info = new InterfaceMethodRefInfo
            {
                StartOffset = reader.Position - 1
            };
            info.ReadRemainingFields(reader);
            return info;
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.InterfaceMethodRef;
        
    }
}