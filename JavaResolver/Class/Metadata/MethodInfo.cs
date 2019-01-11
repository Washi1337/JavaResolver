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
    }
}