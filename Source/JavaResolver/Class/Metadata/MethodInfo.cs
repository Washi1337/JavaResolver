namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Represents raw metadata describing a method defined in a Java class file.s
    /// </summary>
    public class MethodInfo : MemberInfo
    {
        /// <summary>
        /// Reads a single method at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The method that was read</returns>
        public static MethodInfo FromReader(IBigEndianReader reader)
        {
            var info = new MethodInfo
            {
                StartOffset = reader.Position,
                AccessFlags = (MethodAccessFlags) reader.ReadUInt16(),
            };
            info.ReadRemainingFields(reader);
            return info;
        }

        /// <summary>
        /// Gets or sets the accessibility of the method.
        /// </summary>
        public MethodAccessFlags AccessFlags
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
            return $"Method (Name: {NameIndex}, Descriptor: {DescriptorIndex}, Access: {AccessFlags})";
        }
    }
}