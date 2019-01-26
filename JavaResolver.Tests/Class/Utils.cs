using JavaResolver.Class.Emit;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Tests.Class
{
    internal class Utils
    {
        public static JavaClassImage RebuildClassImage(JavaClassImage image)
        {
            var builder = new JavaClassFileBuilder();
            var file = builder.CreateClassFile(image);
            return new JavaClassImage(file);
        }
    }
}