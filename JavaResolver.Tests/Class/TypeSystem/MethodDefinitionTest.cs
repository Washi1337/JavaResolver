using System.Linq;
using JavaResolver.Class;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.TypeSystem
{
    public class MethodDefinitionTest
    {
        private static readonly DescriptorComparer Comparer = new DescriptorComparer();
        private MethodDefinition _constructor;

        public MethodDefinitionTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var classDef = new ClassDefinition(classFile);
            _constructor = classDef.Methods.FirstOrDefault(x => x.Name == "<init>");
        }

        [Fact]
        public void NameTest()
        {
            Assert.NotNull(_constructor);
        }

        [Fact]
        public void AccessFlagsTest()
        {
            Assert.Equal(MethodAccessFlags.Public, _constructor.AccessFlags);
        }

        [Fact]
        public void DescriptorTest()
        {
            Assert.Equal(new MethodDescriptor(
                    new BaseType(BaseTypeValue.Void),
                    new ObjectType("java/lang/String"),
                    new BaseType(BaseTypeValue.Int), new ObjectType("SimpleModel")),
                _constructor.Descriptor, Comparer);
        }
    }
}