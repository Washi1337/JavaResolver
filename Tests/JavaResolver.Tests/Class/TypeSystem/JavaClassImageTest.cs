using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.TypeSystem
{
    public class JavaClassImageTest
    {
        private static JavaClassImage CreateDummyImage()
        {
            return new JavaClassImage(new ClassDefinition("DummyClass")
            {
                SuperClass = new ClassReference("java/lang/Object")
            });
        }

        [Fact]
        public void PersistentSourceFile()
        {
            const string filePath = @"C:\\Path\\To\\File.java";
            
            var dummyImage = CreateDummyImage();
            dummyImage.SourceFile = filePath;
            
            var newImage = Utils.RebuildClassImage(dummyImage);
            Assert.Equal(filePath, newImage.SourceFile);
        }
    }
}