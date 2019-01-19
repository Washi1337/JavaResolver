using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents the line numbers attribute that is associated to a <see cref="CodeAttribute"/>.
    /// This attribute contains a collection of offsets that are mapped to line numbers of the original file. 
    /// </summary>
    public class LineNumberTableAttribute : IAttributeContents
    {
        public const string AttributeName = "LineNumberTable";

        public static LineNumberTableAttribute FromReader(IBigEndianReader reader)
        {
            var result = new LineNumberTableAttribute();
            
            ushort count = reader.ReadUInt16();
            for (int i =0; i < count; i++)
            {
                ushort pc = reader.ReadUInt16();
                ushort line = reader.ReadUInt16();
                result.Entries[pc] = line;
            }

            return result;
        }

        /// <inheritdoc />
        public string Name => AttributeName;

        /// <summary>
        /// Gets a sorted mapping from offsets to line numbers.
        /// </summary>
        public IDictionary<ushort, ushort> Entries
        {
            get;
        } = new SortedDictionary<ushort, ushort>();
        
        /// <inheritdoc />
        public byte[] Serialize(BuildingContext context)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                writer.Write((ushort) Entries.Count);
                foreach (var entry in Entries)
                {
                    writer.Write(entry.Key);
                    writer.Write(entry.Value);
                }

                return stream.ToArray();
            }
        }
    }
}