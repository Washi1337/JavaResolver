using System.Collections.Generic;

namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Provides a base for all raw metadata structures describing a member stored in a Java class file.
    /// </summary>
    public abstract class MemberInfo : FileSegment, IAttributeProvider
    {
        /// <summary>
        /// Gets or sets an index into the constant pool that references the name of the member.
        /// </summary>
        public ushort NameIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an index into the constant pool that references the descriptor of the member.
        /// </summary>
        public ushort DescriptorIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of attributes that are assigned to the member.
        /// </summary>
        public IList<AttributeInfo> Attributes
        {
            get;
        } = new List<AttributeInfo>();

        /// <summary>
        /// Reads the remaining common fields that all member structures share. This includes the name, the descriptor
        /// and the attributes.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        protected void ReadRemainingFields(IBigEndianReader reader)
        {
            NameIndex = reader.ReadUInt16();
            DescriptorIndex = reader.ReadUInt16();
            
            ushort count = reader.ReadUInt16();
            for (int i = 0; i < count; i++)
                Attributes.Add(AttributeInfo.FromReader(reader));
        }

        /// <inheritdoc />
        public override void Write(WritingContext context)
        {
            var writer = context.Writer;
            writer.Write(NameIndex);
            writer.Write(DescriptorIndex);
            
            writer.Write((ushort) Attributes.Count);
            foreach (var attribute in Attributes)
                attribute.Write(context);
        }
    }

}