using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents an attribute associated to a method that specifies what exceptions might be thrown
    /// by the code stored in the body of the method.
    /// </summary>
    public class ExceptionsAttribute : IAttributeContents
    {
        public const string AttributeName = "Exceptions";

        public static ExceptionsAttribute FromReader(IBigEndianReader reader)
        {
            var result = new ExceptionsAttribute();
         
            ushort count = reader.ReadUInt16();
            for (int i = 0; i < count; i++)
                result.Exceptions.Add(reader.ReadUInt16());

            return result;
        }

        /// <inheritdoc />
        public string Name => AttributeName;

        /// <summary>
        /// Gets a collection of indices to the constant pool that refer to exception types.
        /// </summary>
        public IList<ushort> Exceptions
        {
            get;
        } = new List<ushort>();

        /// <inheritdoc />
        public byte[] Serialize(BuildingContext context)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                writer.Write((ushort) Exceptions.Count);

                foreach (var exception in Exceptions)
                    writer.Write(exception);

                return stream.ToArray();
            }
        }
    }
}