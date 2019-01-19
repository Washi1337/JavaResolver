using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
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
        
        public string Name => AttributeName;

        public IDictionary<ushort, ushort> Entries
        {
            get;
        } = new SortedDictionary<ushort, ushort>();
        
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