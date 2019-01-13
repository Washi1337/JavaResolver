namespace JavaResolver.Class.Emit
{
    public class BuildingContext
    {
        public BuildingContext(JavaClassFileBuilder builder)
        {
            Builder = builder;
        }
        
        public JavaClassFileBuilder Builder
        {
            get;
        }   
    }
}