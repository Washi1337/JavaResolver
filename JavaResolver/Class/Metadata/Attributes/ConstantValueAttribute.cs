using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    public class ConstantValueAttribute : IAttributeContents
    {
        public const string AttributeName = "ConstantValue";
        
        public static ConstantValueAttribute FromReader(IBigEndianReader reader)
        {
            return new ConstantValueAttribute
            {
                ConstantValueIndex = reader.ReadUInt16(),
            };
        }

        public ConstantValueAttribute()
        {
        }

        public ConstantValueAttribute(ushort constantValueIndex)
        {
            ConstantValueIndex = constantValueIndex;
        }
        
        /// <inheritdoc />
        public string Name => AttributeName;
        
        public ushort ConstantValueIndex
        {
            get;
            set;
        }
        
        public byte[] Serialize(BuildingContext context)
        {
            ushort value = ConstantValueIndex;
            return new byte[2]
            {
                (byte) ((value >> 8) & 0xFF),
                (byte) (value & 0xFF)
            };
        }
    }
}