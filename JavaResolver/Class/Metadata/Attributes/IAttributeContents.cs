using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    public interface IAttributeContents
    {
        string Name
        {
            get;
        }
        
        byte[] Serialize(BuildingContext context);
    }
}