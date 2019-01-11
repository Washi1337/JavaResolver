namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents a reference to a field from a Java class file. 
    /// </summary>
    public class FieldRefInfo : MemberRefInfo
    {
        /// <summary>
        /// Reads a single field reference at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The field reference that was read.</returns>
        public new static FieldRefInfo FromReader(IBigEndianReader reader)
        {
            var info = new FieldRefInfo();
            info.ReadRemainingFields(reader);
            return info;
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.FieldRef;
    }
}