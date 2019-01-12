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

        protected bool Equals(ArrayType other)
        {
            return Equals(ComponentType, other.ComponentType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((ArrayType) obj);
        }

        public override int GetHashCode()
        {
            return (ComponentType != null ? ComponentType.GetHashCode() : 0);
        }
    }
}