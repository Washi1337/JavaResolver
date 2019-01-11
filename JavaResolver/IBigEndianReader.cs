namespace JavaResolver
{
    /// <summary>
    /// Provides members for reading data in big-endian format.
    /// </summary>
    public interface IBigEndianReader
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
        /// Reads a single byte at the current position and increase the position by 1. 
        /// </summary>
        /// <returns>The byte that was read.</returns>
        byte ReadByte();

        /// <summary>
        /// Reads a block of bytes at the current position and increase the position by
        /// the amount of bytes read. 
        /// </summary>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns>The bytes that were read.</returns>
        byte[] ReadBytes(int length);
        
        /// <summary>
        /// Reads a single unsigned 16-bit integer at the current position and increase the position by 2. 
        /// </summary>
        /// <returns>The integer that was read.</returns>
        ushort ReadUInt16();

        /// <summary>
        /// Reads a single unsigned 32-bit integer at the current position and increase the position by 4. 
        /// </summary>
        /// <returns>The integer that was read.</returns>
        uint ReadUInt32();

        /// <summary>
        /// Reads a single unsigned 64-bit integer at the current position and increase the position by 8. 
        /// </summary>
        /// <returns>The integer that was read.</returns>
        ulong ReadUInt64();
        
        /// <summary>
        /// Reads a single signed byte at the current position and increase the position by 1. 
        /// </summary>
        /// <returns>The byte that was read.</returns>
        sbyte ReadSByte();
        
        /// <summary>
        /// Reads a single nsigned 16-bit integer at the current position and increase the position by 2. 
        /// </summary>
        /// <returns>The integer that was read.</returns>
        short ReadInt16();

        /// <summary>
        /// Reads a single nsigned 32-bit integer at the current position and increase the position by 4. 
        /// </summary>
        /// <returns>The integer that was read.</returns>
        int ReadInt32();

        /// <summary>
        /// Reads a single signed 64-bit integer at the current position and increase the position by 8. 
        /// </summary>
        /// <returns>The integer that was read.</returns>
        long ReadInt64();
        
        /// <summary>
        /// Reads a single single-precision floating point number at the current position and increase the position by 4. 
        /// </summary>
        /// <returns>The number that was read.</returns>
        float ReadSingle();
        
        /// <summary>
        /// Reads a single double-precision floating point number at the current position and increase the position by 8.
        /// </summary>
        /// <returns>The number that was read.</returns>
        object ReadDouble();
    }
}