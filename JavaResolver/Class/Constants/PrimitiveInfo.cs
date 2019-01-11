using System;

namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents a primitive literal stored in the constant pool.
    /// This literal can be an integer, a long, a float or a double. 
    /// </summary>
    public class PrimitiveInfo : ConstantInfo
    {
        private object _value;

        public PrimitiveInfo(object value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag
        {
            get
            {
                switch (Type.GetTypeCode(Value.GetType()))
                {
                    case TypeCode.Int32:
                        return ConstantPoolTag.Integer;
                    case TypeCode.Int64:
                        return ConstantPoolTag.Long;
                    case TypeCode.Single:
                        return ConstantPoolTag.Float;
                    case TypeCode.Double:
                        return ConstantPoolTag.Double;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the raw value of the literal.
        /// </summary>
        public object Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            return $"{Tag} (Value: {Value})";
        }
    }
}