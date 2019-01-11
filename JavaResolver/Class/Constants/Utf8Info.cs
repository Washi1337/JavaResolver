using System.Text;

namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents a single UTF8 string stored in the constant pool of a Java class file. This string can be used
    /// as a literal, but is also used to specify the name, or provide a descriptor to a member.
    /// </summary>
    public class Utf8Info : ConstantInfo
    {
        /// <summary>
        /// Reads a single UTF8 string at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The UTF8 string that was read.</returns>
        public new static Utf8Info FromReader(IBigEndianReader reader)
        {
            ushort length = reader.ReadUInt16();
            return new Utf8Info
            {
                Value = Encoding.UTF8.GetString(reader.ReadBytes(length))
            };
        }

        /// <inheritdoc />
        public override ConstantPoolTag Tag => ConstantPoolTag.Utf8;

        /// <summary>
        /// Gets or sets the raw string that is used.
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"Utf8 (Value: {Value})";
        }
    }
}