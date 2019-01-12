using System;

namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// Represents a descriptor for a field that specifies the field type.
    /// </summary>
    public class FieldDescriptor
    {
        /// <summary>
        /// Deserializes a field descriptor from its raw representation.
        /// </summary>
        /// <param name="rawDescriptor">The raw descriptor string to deserialize.</param>
        /// <returns>The deserialized field descriptor.</returns>
        public static FieldDescriptor FromString(string rawDescriptor)
        {
            var parser = new DescriptorParser(rawDescriptor);
            return parser.ReadFieldDescriptor();
        }
        
        public FieldDescriptor(FieldType fieldType)
        {
            FieldType = fieldType ?? throw new ArgumentNullException(nameof(fieldType));
        }

        /// <summary>
        /// Gets the type of values the field contains.
        /// </summary>
        public FieldType FieldType
        {
            get;
        }

        public override string ToString()
        {
            return $"{nameof(FieldType)}: {FieldType}";
        }

        protected bool Equals(FieldDescriptor other)
        {
            return Equals(FieldType, other.FieldType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((FieldDescriptor) obj);
        }

        public override int GetHashCode()
        {
            return (FieldType != null ? FieldType.GetHashCode() : 0);
        }
    }
}