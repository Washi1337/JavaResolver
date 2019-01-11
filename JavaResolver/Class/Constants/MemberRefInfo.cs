namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Provides a base for all references to a particular member stored in the constant pool of a Java class file.
    /// </summary>
    public abstract class MemberRefInfo : ConstantInfo
    {
        /// <summary>
        /// Reads the common fields that all kinds of member reference constants share.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        protected void ReadRemainingFields(IBigEndianReader reader)
        {
            ClassIndex = reader.ReadUInt16();
            NameAndTypeIndex = reader.ReadUInt16();
        }
        
        /// <summary>
        /// Gets or sets an index into the constant pool that references the class constant that declares the member.
        /// </summary>
        public ushort ClassIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an index into the constant pool that references the name and type that is associated to the member.
        /// </summary>
        public ushort NameAndTypeIndex
        {
            get;
            set;
        }

        public override void Write(WritingContext context)
        {
            base.Write(context);
            var writer = context.Writer;
            writer.Write(ClassIndex);
            writer.Write(NameAndTypeIndex);
        }

        public override string ToString()
        {
            return $"{Tag} (Class: {ClassIndex}, NameAndType: {NameAndTypeIndex})";
        }
    }
}