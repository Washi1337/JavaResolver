using System;
using System.IO;
using Xunit;

namespace JavaResolver.Tests
{
    public class ReaderWriterTest
    {
        private static readonly byte[] Bytes = new byte[]
        {
            8,
            0, 8,
            0, 0, 0, 8,
            0, 0, 0, 0, 0, 0, 0, 8,
        };

        [Fact]
        public void ReadTest()
        {
            var reader = new MemoryBigEndianReader(Bytes);
            Assert.Equal(8, reader.ReadByte());
            Assert.Equal(8, reader.ReadInt16());
            Assert.Equal(8, reader.ReadInt32());
            Assert.Equal(8, reader.ReadInt64());
            Assert.Throws<EndOfStreamException>(() => reader.ReadByte());
        }

        [Fact]
        public void WriteTest()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);

                writer.Write((byte) 8);
                writer.Write((ushort) 8);
                writer.Write((uint) 8);
                writer.Write((ulong) 8);

                Assert.Equal(Bytes, stream.ToArray());
            }
        }
    }
}