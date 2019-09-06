using System;
using System.IO;

namespace JavaResolver
{
    public class MemoryBigEndianReader : IBigEndianReader
    {
        private readonly byte[] _data;
        private readonly int _length;

        public MemoryBigEndianReader(byte[] data)
            : this(data, 0, data.Length)
        {
        }

        public MemoryBigEndianReader(byte[] data, int start, int length)
        {
            StartPosition = start;
            _data = data;
            _length = length;
        }

        public long StartPosition
        {
            get;
        }

        public long Position
        {
            get;
            set;
        }

        public long Length => _length;

        private void AssertCanReadBytes(int length)
        {
            if (Position + length > StartPosition + Length)
                throw new EndOfStreamException();
        }

        public byte ReadByte()
        {
            AssertCanReadBytes(1);
            return _data[Position++];
        }

        public byte[] ReadBytes(int length)
        {
            AssertCanReadBytes(length);
            var buffer = new byte[length];
            Buffer.BlockCopy(_data, (int) Position, buffer, 0, length);
            Position += length;
            return buffer;
        }

        public ushort ReadUInt16()
        {
            AssertCanReadBytes(2);
            ushort value = (ushort) (_data[Position + 1] | (_data[Position] << 8));
            Position += 2;
            return value;
        }

        public uint ReadUInt32()
        {
            AssertCanReadBytes(4);
            uint value = unchecked((uint) (_data[Position + 3]
                                           | (_data[Position + 2] << 8)
                                           | (_data[Position + 1] << 16)
                                           | (_data[Position] << 24)));
            Position += 4;
            return value;
        }

        public ulong ReadUInt64()
        {
            AssertCanReadBytes(8);
            ulong value = unchecked((ulong) ((ulong) _data[Position + 7]
                                             | (ulong) (_data[Position + 6] << 8)
                                             | (ulong) (_data[Position + 5] << 16)
                                             | (ulong) (_data[Position + 4] << 24)
                                             | (ulong) (_data[Position + 3] << 32)
                                             | (ulong) (_data[Position + 2] << 40)
                                             | (ulong) (_data[Position + 1] << 48)
                                             | (ulong) (_data[Position] << 56)));
            Position += 8;
            return value;
        }

        public sbyte ReadSByte()
        {
            return unchecked((sbyte) ReadByte());
        }

        public short ReadInt16()
        {
            return unchecked((short) ReadUInt16());
        }

        public int ReadInt32()
        {
            return unchecked((int) ReadUInt32());
        }

        public long ReadInt64()
        {
            return unchecked((long) ReadUInt64());
        }

        public float ReadSingle()
        {
            return BitConverter.ToSingle(ReadBytes(4), 0);
        }

        public object ReadDouble()
        {
            return BitConverter.ToDouble(ReadBytes(8), 0);
        }
    }
}