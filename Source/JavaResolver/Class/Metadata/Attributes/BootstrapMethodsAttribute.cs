using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents the bootstrap methods attribute containing a table of boot strap methods used by the invokedynamic opcode.
    /// </summary>
    public class BootstrapMethodsAttribute : IAttributeContents
    {
        public const string AttributeName = "BootstrapMethods";

        /// <summary>
        /// Reads a single bootstrap methods attribute at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The bootstrap methods attribute that was read.</returns>
        public static BootstrapMethodsAttribute FromReader(IBigEndianReader reader)
        {
            var result = new BootstrapMethodsAttribute();

            ushort count = reader.ReadUInt16();
            for (int i = 0; i < count; i++)
                result.BootstrapMethods.Add(BootstrapMethodInfo.FromReader(reader));

            return result;
        }

        /// <inheritdoc />
        public string Name => AttributeName;

        /// <summary>
        /// Gets the ordered collection of bootstrap methods defined by this attribute.
        /// </summary>
        public IList<BootstrapMethodInfo> BootstrapMethods
        {
            get;
        } = new List<BootstrapMethodInfo>();

        /// <inheritdoc />
        public byte[] Serialize(BuildingContext context)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                writer.Write((ushort) BootstrapMethods.Count);
                foreach (var method in BootstrapMethods)
                    method.Write(writer);
                return stream.ToArray();
            }
        }
        
    }
}