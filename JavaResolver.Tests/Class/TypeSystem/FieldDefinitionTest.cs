using System.Linq;
using JavaResolver.Class;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.TypeSystem
{
    public class FieldDefinitionTest
    {
        private static readonly DescriptorComparer Comparer = new DescriptorComparer();
        private FieldDefinition _myIntField;
        private FieldDefinition _myStringField;
        private FieldDefinition _myModelField;

        public FieldDefinitionTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var classDef = new ClassDefinition(classFile);
            _myIntField = classDef.Fields.FirstOrDefault(x => x.Name == "myIntField");
            _myStringField = classDef.Fields.FirstOrDefault(x => x.Name == "myStringField");
            _myModelField = classDef.Fields.FirstOrDefault(x => x.Name == "myModelField");
        }

        [Fact]
        public void NameTest()
        {
            Assert.NotNull(_myIntField);
            Assert.NotNull(_myModelField);
            Assert.NotNull(_myStringField);
        }

        [Fact]
        public void DescriptorTest()
        {
            Assert.Equal(new BaseType(BaseTypeValue.Int), _myIntField.Descriptor.FieldType, Comparer);
            Assert.Equal(new ObjectType("java/lang/String"), _myStringField.Descriptor.FieldType, Comparer);
            Assert.Equal(new ObjectType("SimpleModel"), _myModelField.Descriptor.FieldType, Comparer);
        }

        [Fact]
        public void AccessFlagsTest()
        {
            Assert.Equal(FieldAccessFlags.Private, _myIntField.AccessFlags);
            Assert.Equal(FieldAccessFlags.Private, _myStringField.AccessFlags);
            Assert.Equal(FieldAccessFlags.Private, _myModelField.AccessFlags);   
        }
    }
}