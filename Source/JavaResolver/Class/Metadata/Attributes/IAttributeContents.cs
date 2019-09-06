using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents the deserialized contents of an attribute associated to some metadata stored in a Java class file.
    /// </summary>
    public interface IAttributeContents
    {
        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        string Name
        {
            get;
        }
        
        /// <summary>
        /// Serializes the attribute to a byte array, and adds any additional metadata required to the buffers provided
        /// in the building context. 
        /// </summary>
        /// <param name="context">The building context to add the additional required metadata to.</param>
        /// <returns>The byte array representing the serialized data.</returns>
        byte[] Serialize(BuildingContext context);
    }
}