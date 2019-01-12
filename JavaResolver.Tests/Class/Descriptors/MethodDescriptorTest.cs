using JavaResolver.Class.Descriptors;
using Xunit;

namespace JavaResolver.Tests.Class.Descriptors
{
    public class MethodDescriptorTest
    {
        private static readonly DescriptorComparer Comparer = new DescriptorComparer();
        
        [Fact]
        public void NoParameters()
        {
            Assert.Equal(new MethodDescriptor(new BaseType(BaseTypeValue.Void)),
                MethodDescriptor.FromString("()V"),
                Comparer);
        }
        
        [Fact]
        public void SimpleParameter()
        {
            Assert.Equal(new MethodDescriptor(new BaseType(BaseTypeValue.Void), new BaseType(BaseTypeValue.Int)),
                MethodDescriptor.FromString("(I)V"),
                Comparer);
        }

        [Fact]
        public void MultipleSimpleParameters()
        {
            Assert.Equal(new MethodDescriptor(new BaseType(BaseTypeValue.Void), 
                    new BaseType(BaseTypeValue.Int),
                    new BaseType(BaseTypeValue.Int),
                    new BaseType(BaseTypeValue.Double),
                    new BaseType(BaseTypeValue.Byte)),
                MethodDescriptor.FromString("(IIDB)V"),
                Comparer);
        }
        
        [Fact]
        public void ComplexReturnType()
        {
            Assert.Equal(new MethodDescriptor(new ArrayType(new ObjectType("java/lang/Object"))),
                MethodDescriptor.FromString("()[Ljava/lang/Object;"),
                Comparer);
        }
        
        [Fact]
        public void ComplexParametersAndReturnType()
        {
            Assert.Equal(new MethodDescriptor(new ObjectType("java/lang/Object"),
                    new BaseType(BaseTypeValue.Int),
                    new BaseType(BaseTypeValue.Double),
                    new ObjectType("java/lang/Thread")),
                MethodDescriptor.FromString("(IDLjava/lang/Thread;)Ljava/lang/Object;"),
                Comparer);
        }
    }
}