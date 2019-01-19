using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents a local variables table attribute that is associated to a method.
    /// This attribute contains names, indices and scope ranges for local variables defined in the associated method. 
    /// </summary>
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
        
        /// <inheritdoc />
        public string Name => AttributeName;

        /// <summary>
        /// Gets a collection of metadata entries representing local variables.
        /// </summary>
        public IList<LocalVariableInfo> LocalVariables
        {
            get;
        } = new List<LocalVariableInfo>();
        
        /// <inheritdoc />
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