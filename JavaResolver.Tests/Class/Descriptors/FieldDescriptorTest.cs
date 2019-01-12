using System;
using JavaResolver.Class.Descriptors;
using Xunit;

namespace JavaResolver.Tests.Class.Descriptors
{
    public class FieldDescriptorTest
    {
        private static readonly DescriptorComparer Comparer = new DescriptorComparer();
        
        [Theory]
        [InlineData(BaseTypeValue.Byte, "B")]
        [InlineData(BaseTypeValue.Char, "C")]
        [InlineData(BaseTypeValue.Double, "D")]
        [InlineData(BaseTypeValue.Float, "F")]
        [InlineData(BaseTypeValue.Int, "I")]
        [InlineData(BaseTypeValue.Long, "J")]
        [InlineData(BaseTypeValue.Short, "S")]
        [InlineData(BaseTypeValue.Boolean, "Z")]
        [InlineData(BaseTypeValue.Void, "V")]
        public void BaseTypeTest(BaseTypeValue primitive, string rawDescriptor)
        {
            Assert.Equal(new FieldDescriptor(new BaseType(primitive)),
                FieldDescriptor.FromString(rawDescriptor), 
                Comparer);
        }

        [Fact]
        public void ObjectTypeTest()
        {
            Assert.Equal(new FieldDescriptor(new ObjectType("java/lang/Object")), 
                FieldDescriptor.FromString("Ljava/lang/Object;"),
                Comparer);
        }

        [Fact]
        public void SimpleArrayTest()
        {
            Assert.Equal(new FieldDescriptor(new ArrayType(new BaseType(BaseTypeValue.Int))), 
                FieldDescriptor.FromString("[I"),
                Comparer);
        }
        
        [Fact]
        public void ObjectArrayTest()
        {
            Assert.Equal(new FieldDescriptor(new ArrayType(new ObjectType("java/lang/Object"))),
                FieldDescriptor.FromString("[Ljava/lang/Object;"),
                Comparer);
        }
        
        [Fact]
        public void MultiDimensionalBaseArrayTest()
        {
            Assert.Equal(new FieldDescriptor(
                    new ArrayType(new ArrayType(new ArrayType(new BaseType(BaseTypeValue.Int))))),
                FieldDescriptor.FromString("[[[I;"),
                Comparer);
        }

        [Fact]
        public void MultiDimensionalObjectArrayTest()
        {
            Assert.Equal(new FieldDescriptor(
                new ArrayType(new ArrayType(new ArrayType(new ObjectType("java/lang/Object"))))),
                FieldDescriptor.FromString("[[[Ljava/lang/Object;"),
                Comparer);
        }

        [Fact]
        public void MethodDescriptorCharacters()
        {
            Assert.Throws<FormatException>(() => FieldDescriptor.FromString("()V"));
        }
    }
}