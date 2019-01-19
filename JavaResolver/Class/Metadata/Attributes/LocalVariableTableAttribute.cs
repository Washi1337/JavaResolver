using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    public class LocalVariableTableAttribute : IAttributeContents
    {
        public const string AttributeName = "LocalVariableTable";

        public static LocalVariableTableAttribute FromReader(IBigEndianReader reader)
        {
            var result = new LocalVariableTableAttribute();
            
            ushort length = reader.ReadUInt16();
            for (int i = 0; i < length; i++)
                result.LocalVariables.Add(LocalVariableInfo.FromReader(reader));

            return result;
        }
        
        public string Name => AttributeName;

        public IList<LocalVariableInfo> LocalVariables
        {
            get;
        } = new List<LocalVariableInfo>();
        
        public byte[] Serialize(BuildingContext context)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                writer.Write((ushort) LocalVariables.Count);
                foreach (var variable in LocalVariables)
                    variable.Write(writer);
                return stream.ToArray();
            }
        }
    }
}