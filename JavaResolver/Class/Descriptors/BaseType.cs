namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// Represents a primitive type referenced in a member descriptor.
    /// </summary>
    public class BaseType : FieldType
    {
        public BaseType(BaseTypeValue value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override char Prefix => (char) Value;

        /// <summary>
        /// Gets the primitive type that is referenced.
        /// </summary>
        public BaseTypeValue Value
        {
            get;
        }

        /// <inheritdoc />
        public override string Serialize()
        {
            return Prefix.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value.ToString();
        }

        protected bool Equals(BaseType other)
        {
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((BaseType) obj);
        }

        public override int GetHashCode()
        {
            return (int) Value;
        }
    }
}