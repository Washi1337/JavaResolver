namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a single raw exception handler structure providing information about a range of protected code
    /// in a method body.
    /// </summary>
    public class RawExceptionHandler : FileSegment
    {
        public static RawExceptionHandler FromReader(IBigEndianReader reader)
        {
            return new RawExceptionHandler
            {
                StartOffset = reader.ReadUInt16(),
                EndOffset = reader.ReadUInt16(),
                HandlerOffset = reader.ReadUInt16(),
                CatchType = reader.ReadUInt16(),
            };
        }

        /// <summary>
        /// Gets or sets the starting offset of the protected code range.
        /// </summary>
        public ushort StartOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ending offset of the protected code range.
        /// </summary>
        public ushort EndOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start of the handler block in the protected code range.
        /// </summary>
        public ushort HandlerOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of exception that is caught (when applicable)>
        /// </summary>
        public ushort CatchType
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            var writer = context.Writer;
            writer.Write(StartOffset);
            writer.Write(EndOffset);
            writer.Write(HandlerOffset);
            writer.Write(CatchType);
        }
    }
}