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
    }
}