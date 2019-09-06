using System;

namespace JavaResolver
{
    public class WritingContext
    {
        public WritingContext(IBigEndianWriter writer)
        {
            Writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public IBigEndianWriter Writer
        {
            get;
        }
    }
}