using System.Collections.Generic;
using System.Linq;

namespace JavaResolver.Class.Descriptors
{
    public class MethodDescriptor : IMemberDescriptor
    {
        /// <summary>
        /// Deserializes a method descriptor from its raw representation.
        /// </summary>
        /// <param name="rawDescriptor">The raw descriptor string to deserialize.</param>
        /// <returns>The deserialized field descriptor.</returns>
        public static MethodDescriptor FromString(string rawDescriptor)
        {
            var parser = new DescriptorParser(rawDescriptor);
            return parser.ReadMethodDescriptor();
        }
        
        public MethodDescriptor(FieldType returnType)
            : this(returnType, Enumerable.Empty<FieldType>())
        {
        }

        public MethodDescriptor(FieldType returnType, params FieldType[] parameterTypes)
            : this (returnType, parameterTypes.AsEnumerable())
        {
        }

        public MethodDescriptor(FieldType returnType, IEnumerable<FieldType> parameterTypes)
        {
            ReturnType = returnType;
            ParameterTypes = new List<FieldType>(parameterTypes);
        }

        public IList<FieldType> ParameterTypes
        {
            get;
        }

        public FieldType ReturnType
        {
            get;
        }

        FieldType IMemberDescriptor.MemberType => ReturnType;
        
        public string Serialize()
        {
            return "(" + string.Join("", ParameterTypes.Select(x => x.Serialize())) + ")" + ReturnType.Serialize();
        }

        public override string ToString()
        {
            return $"{ReturnType} *({string.Join(", ", ParameterTypes)})";
        }
    }
}