using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    public class InnerClassesAttribute : IAttributeContents
    {
        public const string AttributeName = "InnerClasses";
        
        /// <summary>
        /// Reads a single inner classes attribute at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The immer c;asses attribute that was read.</returns>
        public static InnerClassesAttribute FromReader(IBigEndianReader reader)
        {
            var attr = new InnerClassesAttribute();

            ushort classCount = reader.ReadUInt16();
            for (int i = 0; i < classCount; i++)
                attr.InnerClasses.Add(InnerClassInfo.FromReader(reader));
            
            return attr;
        }

        /// <inheritdoc />
        public string Name => AttributeName;

        /// <summary>
        /// Gets a collection of inner class relations defined by this attribute.
        /// </summary>
        public IList<InnerClassInfo> InnerClasses
        {
            get;
        } = new List<InnerClassInfo>();

        /// <inheritdoc />
        public byte[] Serialize(BuildingContext context)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                
                writer.Write((ushort) InnerClasses.Count);
                foreach (var @class in InnerClasses)
                    @class.Write(writer);

                return stream.ToArray();
            }
        }
        
    }
}