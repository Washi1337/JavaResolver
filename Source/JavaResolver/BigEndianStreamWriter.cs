using System;
using System.IO;

namespace JavaResolver
{
    public class BigEndianStreamWriter : IBigEndianWriter
    {
        private readonly Stream _stream;

        public BigEndianStreamWriter(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            
            if (!stream.CanWrite)
                throw new ArgumentException("Stream must be writable.", nameof(stream));
            if (!stream.CanSeek)
                throw new ArgumentException("Stream must be seekable.", nameof(stream));
            
            StartPosition = _stream.Position;
        }

        public long StartPosition
        {
            get;
        }

        public long Position
        {
            get => _stream.Position;
            set => _stream.Position = value;
        }

        public long Length => _stream.Length - StartPosition;

        public void Write(byte value)
        {
            _stream.WriteByte(value);
        }

        public void Write(byte[] bytes)
        {
            _stream.Write(bytes, 0, bytes.Length);
        }

        public void Write(ushort value)
        {
            _stream.WriteByte((byte) ((value >> 8) & 0xFF));
            _stream.WriteByte((byte) (value & 0xFF));
        }

        public void Write(uint value)
        {
            _stream.WriteByte((byte) ((value >> 24) & 0xFF));
            _stream.WriteByte((byte) ((value >> 16) & 0xFF));
            _stream.WriteByte((byte) ((value >> 8) & 0xFF));
            _stream.WriteByte((byte) (value & 0xFF));
        }

        public void Write(ulong value)
        {
            _stream.WriteByte((byte) ((value >> 56) & 0xFF));
            _stream.WriteByte((byte) ((value >> 48) & 0xFF));
            _stream.WriteByte((byte) ((value >> 40) & 0xFF));
            _stream.WriteByte((byte) ((value >> 32) & 0xFF));
            _stream.WriteByte((byte) ((value >> 24) & 0xFF));
            _stream.WriteByte((byte) ((value >> 16) & 0xFF));
            _stream.WriteByte((byte) ((value >> 8) & 0xFF));
            _stream.WriteByte((byte) (value & 0xFF));
        }

        public void Write(sbyte value)
        {
            Write(unchecked((byte) value));
        }

        public void Write(short value)
        {
            Write(unchecked((ushort) value));
        }

        public void Write(int value)
        {
            Write(unchecked((uint) value));
        }

        public void Write(long value)
        {
            Write(unchecked((ulong) value));
        }

        public void Write(float value)
        {
            throw new System.NotImplementedException();
        }

        public void Write(double value)
        {
            throw new System.NotImplementedException();
        }
    }
}