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

        public override void Write(WritingContext context)
        {
            base.Write(context);
            var writer = context.Writer;
            switch (Tag)
            {
                case ConstantPoolTag.Integer:
                    writer.Write((int) Value);
                    break;
                case ConstantPoolTag.Long:
                    writer.Write((long) Value);
                    break;
                case ConstantPoolTag.Float:
                    writer.Write((float) Value);
                    break;
                case ConstantPoolTag.Double:
                    writer.Write((double) Value);
                    break;
                default:
                    throw new ArgumentException("Value must be either an integer, a long, a float or a double.");
            }
        }

        public override string ToString()
        {
            return $"{Tag} (Value: {Value})";
        }

        protected bool Equals(PrimitiveInfo other)
        {
            return Equals(_value, other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((PrimitiveInfo) obj);
        }

        public override int GetHashCode()
        {
            return ((int) Tag << 24) ^ (_value != null ? _value.GetHashCode() : 0);
        }
    }
}