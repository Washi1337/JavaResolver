using System;
using System.IO;
using JavaResolver.Class;
using JavaResolver.Tests.Properties;
using Xunit;

namespace JavaResolver.Tests
{
    public class ClassReadWriteTest
    {
        [Fact]
        public void NoChange()
        {
            var reader = new MemoryBigEndianReader(Resources.HelloWorld);
            var classFile = JavaClassFile.FromReader(reader);

            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                classFile.Write(new WritingContext(writer));
                Assert.Equal(Resources.HelloWorld, stream.ToArray());
            }

        }
    }
}