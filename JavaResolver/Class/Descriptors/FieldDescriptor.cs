using System;

namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// Represents a descriptor for a field that specifies the field type.
    /// </summary>
    public class FieldDescriptor : IMemberDescriptor
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
            set;
        }

        FieldType IMemberDescriptor.MemberType => FieldType;
        
        public string Serialize()
        {
            return FieldType.Serialize();
        }

        public override string ToString()
        {
            return FieldType.ToString();
        }
    }
}