using System;
using System.IO;
using JavaResolver.Class.Constants;
using JavaResolver.Tests.Properties;
using Xunit;

namespace JavaResolver.Tests.Class.Constants
{
    public class ConstantPoolTest
    {
        private const int ConstantPoolOffset = 8;
        private readonly IBigEndianReader _reader = new MemoryBigEndianReader(Resources.ConstantPoolExample) {Position = ConstantPoolOffset};

        [Fact]
        public void ReadConstantPool_ConstantCountIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);

            // Class file has constant pool size of 98, index 0 is reserved, 2 indices are skipped due to large constants.
            Assert.Equal(95, constantPool.Constants.Count);
        }

        [Fact]
        public void ReadConstantPool_ConstantsHaveProperIndices()
        {
            var constantPool = ConstantPool.FromReader(_reader);

            // A constant before large pool entries where indices are sequential.
            var constant1 = constantPool.ResolveString(15);
            // A constant after large pool entries, with indices skipped.
            var constant2 = constantPool.ResolveString(30);

            Assert.Equal("IntValue", constant1);
            Assert.Equal("StringValue", constant2);
        }

        [Fact]
        public void ReadConstantPool_ClassInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (ClassInfo) constantPool.ResolveConstant(2);

            Assert.Equal(48, constant.NameIndex);
        }

        [Fact]
        public void ReadConstantPool_FieldRefInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (FieldRefInfo) constantPool.ResolveConstant(11);

            Assert.Equal(62, constant.ClassIndex);
            Assert.Equal(63, constant.NameAndTypeIndex);
        }

        [Fact]
        public void ReadConstantPool_MethodRefInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (MethodRefInfo) constantPool.ResolveConstant(1);

            Assert.Equal(14, constant.ClassIndex);
            Assert.Equal(47, constant.NameAndTypeIndex);
        }

        [Fact]
        public void ReadConstantPool_InterfaceMethodRefInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (InterfaceMethodRefInfo) constantPool.ResolveConstant(6);

            Assert.Equal(52, constant.ClassIndex);
            Assert.Equal(53, constant.NameAndTypeIndex);
        }

        [Fact]
        public void ReadConstantPool_IntegerInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (PrimitiveInfo) constantPool.ResolveConstant(18);

            Assert.IsType<int>(constant.Value);
            Assert.Equal(42, constant.Value);
        }

        [Fact]
        public void ReadConstantPool_FloatInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (PrimitiveInfo) constantPool.ResolveConstant(25);

            Assert.IsType<float>(constant.Value);
            Assert.Equal(42f, constant.Value);
        }

        [Fact]
        public void ReadConstantPool_LongInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (PrimitiveInfo) constantPool.ResolveConstant(21);

            Assert.IsType<long>(constant.Value);
            Assert.Equal(42L, constant.Value);
        }

        [Fact]
        public void ReadConstantPool_DoubleInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (PrimitiveInfo) constantPool.ResolveConstant(28);

            Assert.IsType<double>(constant.Value);
            Assert.Equal(42d, constant.Value);
        }

        [Fact]
        public void ReadConstantPool_NameAndTypeInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (NameAndTypeInfo) constantPool.ResolveConstant(47);
            
            Assert.Equal(32, constant.NameIndex);
            Assert.Equal(33, constant.DescriptorIndex);
        }

        [Fact]
        public void ReadConstantPool_Utf8InfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (Utf8Info) constantPool.ResolveConstant(15);
            
            Assert.Equal("IntValue", constant.Value);
        }

        [Fact]
        public void ReadConstantPool_MethodHandleInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (MethodHandleInfo) constantPool.ResolveConstant(56);
            
            Assert.Equal(MethodReferenceKind.InvokeStatic, constant.ReferenceKind);
            Assert.Equal(74, constant.ReferenceIndex);
        }

        [Fact]
        public void ReadConstantPool_MethodTypeInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (MethodTypeInfo) constantPool.ResolveConstant(57);
            
            Assert.Equal(33, constant.DescriptorIndex);
        }

        [Fact]
        public void ReadConstantPool_InvokeDynamicInfoIsCorrect()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            
            var constant = (InvokeDynamicInfo) constantPool.ResolveConstant(8);
            
            Assert.Equal(59, constant.NameAndTypeIndex);
            Assert.Equal(0, constant.BootstrapMethodIndex);
        }

        [Fact]
        public void WriteConstantPool_ByteAreEqual()
        {
            var constantPool = ConstantPool.FromReader(_reader);
            var stream = new MemoryStream();
            var writingContext = new WritingContext(new BigEndianStreamWriter(stream));

            constantPool.Write(writingContext);

            var constantPoolLength = (int) _reader.Position - ConstantPoolOffset;
            Assert.Equal(constantPoolLength, stream.Length);
            Assert.Equal(
                new ArraySegment<byte>(Resources.ConstantPoolExample, ConstantPoolOffset, constantPoolLength), 
                new ArraySegment<byte>(stream.ToArray()));
        }
    }
}