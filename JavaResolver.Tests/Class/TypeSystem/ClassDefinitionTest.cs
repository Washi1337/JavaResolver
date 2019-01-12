using JavaResolver.Class;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.TypeSystem
{
    public class ClassDefinitionTest
    {
        private readonly JavaClassFile _classFile;
        private readonly ClassDefinition _classDef;

        public ClassDefinitionTest()
        {
            _classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            _classDef = new ClassDefinition(_classFile);
        }
        
        [Fact]
        public void NameTest()
        {
            Assert.Equal("SimpleModel", _classDef.Name);
        }

        [Fact]
        public void SuperClassTest()
        {
            Assert.Equal("java/lang/Object", _classDef.SuperClass.Name);
        }

        [Fact]
        public void AccessFlagsTest()
        {
            Assert.Equal(ClassAccessFlags.Public | ClassAccessFlags.Super, _classDef.AccessFlags);
        }

        [Fact]
        public void FieldsTest()
        {
            Assert.Equal(3, _classDef.Fields.Count);
        }
    }
}