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
            var info = new FieldRefInfo
            {
                StartOffset = reader.Position - 1
            };
            info.ReadRemainingFields(reader);
            return info;
        }

        public FieldRefInfo()
        {
        }

        public FieldRefInfo(ushort classIndex, ushort nameAndTypeIndex) 
            : base(classIndex, nameAndTypeIndex)
        {
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.FieldRef;
        
        
    }
}