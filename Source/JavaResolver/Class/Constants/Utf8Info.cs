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
            return new Utf8Info
            {
                StartOffset = reader.Position - 1,
                Value = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadUInt16()))
            };
        }

        public Utf8Info()
        {
        }

        public Utf8Info(string value)
        {
            Value = value;
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

        public override void Write(WritingContext context)
        {
            base.Write(context);
            var writer = context.Writer;
            writer.Write((ushort) Value.Length);
            writer.Write(Encoding.UTF8.GetBytes(Value));
        }

        public override string ToString()
        {
            return $"Utf8 (Value: {Value})";
        }

        protected bool Equals(Utf8Info other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            if (obj.GetType() != this.GetType()) 
                return false;
            return Equals((Utf8Info) obj);
        }

        public override int GetHashCode()
        {
            return ((int) Tag << 24) ^ (Value != null ? Value.GetHashCode() : 0);
        }
    }
}