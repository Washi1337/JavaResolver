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
        private readonly FieldDefinition _myIntField;
        private readonly FieldDefinition _myStringField;
        private readonly FieldDefinition _myModelField;

        public FieldDefinitionTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var image = new JavaClassImage(classFile);
            var classDef = image.RootClass;
            _myIntField = classDef.Fields.FirstOrDefault(x => x.Name == "myIntField");
            _myStringField = classDef.Fields.FirstOrDefault(x => x.Name == "myStringField");
            _myModelField = classDef.Fields.FirstOrDefault(x => x.Name == "myModelField");
        }

        private static FieldDefinition CreateDummyField()
        {
            var field = new FieldDefinition("SomeField", new FieldDescriptor(BaseType.Int));
            var classDef = new ClassDefinition("SomeClass", new ClassReference("java/lang/Object"));
            classDef.Fields.Add(field);
            var image = new JavaClassImage(classDef);
            return field;
        }

        private static FieldDefinition RebuildImageWithField(FieldDefinition field)
        {
            var newImage = Utils.RebuildClassImage(field.DeclaringClass.Image);
            return newImage.RootClass.Fields.First(x => x.Name == field.Name);
        }

        [Fact]
        public void NameTest()
        {
            Assert.NotNull(_myIntField);
            Assert.NotNull(_myModelField);
            Assert.NotNull(_myStringField);
        }

        [Fact]
        public void PersistentName()
        {
            var field = CreateDummyField();
            var newField = RebuildImageWithField(field);
            Assert.Equal(field.Name, newField.Name);
        }

        [Fact]
        public void DescriptorTest()
        {
            Assert.Equal(new BaseType(BaseTypeValue.Int), _myIntField.Descriptor.FieldType, Comparer);
            Assert.Equal(new ObjectType("java/lang/String"), _myStringField.Descriptor.FieldType, Comparer);
            Assert.Equal(new ObjectType("SimpleModel"), _myModelField.Descriptor.FieldType, Comparer);
        }

        [Fact]
        public void PersistentDescriptor()
        {
            var type = ObjectType.String;
            
            var field = CreateDummyField();
            field.Descriptor = new FieldDescriptor(type);
            
            var newField = RebuildImageWithField(field);
            Assert.Equal(type.FullName, newField.Descriptor.FieldType.FullName);
        }

        [Fact]
        public void AccessFlagsTest()
        {
            Assert.Equal(FieldAccessFlags.Private, _myIntField.AccessFlags);
            Assert.Equal(FieldAccessFlags.Private, _myStringField.AccessFlags);
            Assert.Equal(FieldAccessFlags.Private, _myModelField.AccessFlags);   
        }

        [Fact]
        public void PersistentAccessFlags()
        {
            const FieldAccessFlags flags = FieldAccessFlags.Private | FieldAccessFlags.Static;
            
            var field = CreateDummyField();
            field.AccessFlags = flags;
            
            var newField = RebuildImageWithField(field);
            Assert.Equal(flags, newField.AccessFlags);
        }

        [Fact]
        public void PersistentConstant()
        {
            const int constant = 1234;

            var field = CreateDummyField();
            field.AccessFlags = FieldAccessFlags.Final;
            field.Constant = constant;

            var newField = RebuildImageWithField(field);
            Assert.Equal(constant, newField.Constant);
        }
    }
}