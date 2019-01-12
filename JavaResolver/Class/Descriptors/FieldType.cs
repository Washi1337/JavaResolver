namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// When derived from this class, represents a single reference to a type in a descriptor of a field or a method.
    /// </summary>
    public abstract class FieldType
    {
        /// <summary>
        /// Gets the prefix that is used to serialize the field type with.
        /// </summary>
        public abstract char Prefix
        {
            get;
        }

        /// <summary>
        /// Serializes the field type to a string so that it can be stored in the constant pool.
        /// </summary>
        /// <returns>The serialized version of the field type.</returns>
        public abstract string Serialize();
    }
}