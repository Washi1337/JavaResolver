namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// Represents a reference to an array type.
    /// </summary>
    public class ArrayType : FieldType
    {
        public ArrayType(FieldType componentType)
        {
            ComponentType = componentType;
        }

        /// <inheritdoc />
        public override char Prefix => '[';

        /// <summary>
        /// Gets the type of all elements in the referenced array. 
        /// </summary>
        public FieldType ComponentType
        {
            get;
        }

        /// <inheritdoc />
        public override string Serialize()
        {
            return Prefix + ComponentType.Serialize();
        }

        public override string ToString()
        {
            return ComponentType + "[]";
        }
    }
}