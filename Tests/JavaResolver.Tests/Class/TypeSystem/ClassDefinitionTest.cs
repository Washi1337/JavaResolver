using JavaResolver.Class;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.TypeSystem
{
    public class ClassDefinitionTest
    {
        private static JavaClassImage CreateDummyImage()
        {
            return new JavaClassImage(new ClassDefinition("DummyClass")
            {
                SuperClass = new ClassReference("java/lang/Object")
            });
        }
        
        [Fact]
        public void NameTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var image = new JavaClassImage(classFile);
            Assert.Equal("SimpleModel", image.RootClass.Name);
        }

        [Fact]
        public void SuperClassTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var image = new JavaClassImage(classFile);
            Assert.Equal("java/lang/Object", image.RootClass.SuperClass.Name);
        }

        [Fact]
        public void AccessFlagsTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var image = new JavaClassImage(classFile);
            Assert.Equal(ClassAccessFlags.Public | ClassAccessFlags.Super, image.RootClass.AccessFlags);
        }

        [Fact]
        public void FieldsTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var image = new JavaClassImage(classFile);
            Assert.Equal(3, image.RootClass.Fields.Count);
        }

        [Fact]
        public void PersistentName()
        {
            var dummyImage = CreateDummyImage();
            var newImage = Utils.RebuildClassImage(dummyImage);
            Assert.Equal(dummyImage.RootClass.Name, newImage.RootClass.Name);
        }

        [Fact]
        public void PersistentSuperClass()
        {
            const string superClassName = "java/lang/Exception";
            
            var dummyImage = CreateDummyImage();
            dummyImage.RootClass.SuperClass = new ClassReference(superClassName);
            
            var newImage = Utils.RebuildClassImage(dummyImage);
            Assert.Equal(superClassName, newImage.RootClass.SuperClass.Name);
        }

        [Fact]
        public void PersistentAccessFlags()
        {
            const ClassAccessFlags flags = ClassAccessFlags.Public | ClassAccessFlags.Enum;
            
            var dummyImage = CreateDummyImage();
            dummyImage.RootClass.AccessFlags = flags;
            
            var newImage = Utils.RebuildClassImage(dummyImage);
            Assert.Equal(flags, newImage.RootClass.AccessFlags);
        }
    }
}