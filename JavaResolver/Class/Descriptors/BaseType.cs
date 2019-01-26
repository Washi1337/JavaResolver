namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// Represents a primitive type referenced in a member descriptor.
    /// </summary>
    public class BaseType : FieldType
    {
        public static readonly BaseType Byte = new BaseType(BaseTypeValue.Byte);
        public static readonly BaseType Char = new BaseType(BaseTypeValue.Char);
        public static readonly BaseType Double = new BaseType(BaseTypeValue.Double);
        public static readonly BaseType Float = new BaseType(BaseTypeValue.Float);
        public static readonly BaseType Int = new BaseType(BaseTypeValue.Int);
        public static readonly BaseType Long = new BaseType(BaseTypeValue.Long);
        public static readonly BaseType Short = new BaseType(BaseTypeValue.Short);
        public static readonly BaseType Boolean = new BaseType(BaseTypeValue.Boolean);
        public static readonly BaseType Void = new BaseType(BaseTypeValue.Void);
        
        public BaseType(BaseTypeValue value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override char Prefix => (char) Value;

        /// <inheritdoc />
        public override string FullName => Value.ToString().ToLowerInvariant();

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