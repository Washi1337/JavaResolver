using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents an attribute that consists of only one single index into the constant pool of a Java class file.
    /// </summary>
    /// <remarks>
    /// Various attributes are represented using this class. These include:
    /// <list type="bullet">
    ///     <item><description>ConstantValue</description></item>
    ///     <item><description>SourceFile</description></item>
    /// </list>
    /// </remarks>
    public class SingleIndexAttribute : IAttributeContents
    {
        public const string ConstantValueAttribute = "ConstantValue";
        public const string SourceFileAttribute = "SourceFile";

        public static SingleIndexAttribute FromReader(string name, IBigEndianReader reader)
        {
            return new SingleIndexAttribute(name, reader.ReadUInt16());
        }

        public SingleIndexAttribute(string name, ushort constantPoolIndex)
        {
            Name = name;
            ConstantPoolIndex = constantPoolIndex;
        }
        
        /// <inheritdoc />
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets or sets the index into the constant pool that references the object used.
        /// </summary>
        public ushort ConstantPoolIndex
        {
            get;
            set;
        }

        /// <inheritdoc />
        public byte[] Serialize(BuildingContext context)
        {
            ushort value = ConstantPoolIndex;
            return new byte[2]
            {
                (byte) ((value >> 8) & 0xFF),
                (byte) (value & 0xFF)
            };
        }
    }
}