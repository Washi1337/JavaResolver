namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Represents raw metadata describing a field defined in a Java class file.
    /// </summary>
    public class FieldInfo : MemberInfo
    {
        /// <summary>
        /// Reads a single field at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The field that was read.</returns>
        public static FieldInfo FromReader(IBigEndianReader reader)
        {
            var info = new FieldInfo
            {
                StartOffset = reader.Position,
                AccessFlags = (FieldAccessFlags) reader.ReadUInt16(),
            };
            info.ReadRemainingFields(reader);
            return info;
        }
        
        /// <summary>
        /// Gets or sets the accessibility of the field.
        /// </summary>
        public FieldAccessFlags AccessFlags
        {
            get;
            set;
        }

        /// <inheritdoc />
        public override void Write(WritingContext context)
        {
            context.Writer.Write((ushort) AccessFlags);
            base.Write(context);
        }

        public override string ToString()
        {
            return $"Field (Name: {NameIndex}, Descriptor: {DescriptorIndex}, Access: {AccessFlags})";
        }
    }
}