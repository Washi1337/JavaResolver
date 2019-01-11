namespace JavaResolver
{
    public interface IBigEndianWriter
    {
        /// <summary>
        /// Gets the offset the reader started reading at.
        /// </summary>
        long StartPosition
        {
            get;
        }
        
        /// <summary>
        /// Gets or sets the current offset of the reader.
        /// </summary>
        long Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the total amount of bytes the buffer contains.
        /// </summary>
        long Length
        {
            get;
        }

        /// <summary>
        /// Writes a single byte to the output and advances the position by 1.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(byte value);

        /// <summary>
        /// Writes a block of bytes to the output and advances the position by the amount of bytes written.
        /// </summary>
        /// <param name="bytes">The value to write.</param>
        void Write(byte[] bytes);
        
        /// <summary>
        /// Writes a single unsigned 16-bit integer and advances the position by 2.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(ushort value);
        
        /// <summary>
        /// Writes a single unsigned 32-bit integer and advances the position by 4.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(uint value);
        
        /// <summary>
        /// Writes a single unsigned 64-bit integer and advances the position by 8.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(ulong value);
        
        /// <summary>
        /// Writes a single signed byte and advances the position by 1.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(sbyte value);
        
        /// <summary>
        /// Writes a single signed 16-bit integer and advances the position by 2.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(short value);
        
        /// <summary>
        /// Writes a single signed 32-bit integer and advances the position by 4.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(int value);
        
        /// <summary>
        /// Writes a single signed 64-bit integer and advances the position by 8.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(long value);
        
        /// <summary>
        /// Writes a single single-precision floating point number and advances the position by 4.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(float value);
        
        /// <summary>
        /// Writes a single double-precision floating point number and advances the position by 8.
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(double value);
    }
}