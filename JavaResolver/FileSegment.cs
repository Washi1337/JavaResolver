namespace JavaResolver
{
    /// <summary>
    /// Represents a single segment in a file.
    /// </summary>
    public abstract class FileSegment
    {
        /// <summary>
        /// Gets or sets the starting offset of the file segment.
        /// </summary>
        public long StartOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Writes the segment to the provided writing context.
        /// </summary>
        /// <param name="context">The writing context to use</param>
        public abstract void Write(WritingContext context);
    }
}