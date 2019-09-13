using System.Collections.Generic;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents a single entry in the bootstrap methods attribute.
    /// </summary>
    public class BootstrapMethodInfo
    {
        /// <summary>
        /// Reads a single bootstrap methods attribute at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The bootstrap methods attribute that was read.</returns>
        public static BootstrapMethodInfo FromReader(IBigEndianReader reader)
        {
            var result = new BootstrapMethodInfo
            {
                MethodRefIndex = reader.ReadUInt16(),
            };

            ushort count = reader.ReadUInt16();
            for (int i = 0; i < count; i++)
                result.Arguments.Add(reader.ReadUInt16());

            return result;
        }

        /// <summary>
        /// Gets or sets the raw index into the constant pool referencing the method to be called.
        /// </summary>
        public ushort MethodRefIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a mutable collection of indices to static arguments to be passed onto the bootstrap method.
        /// </summary>
        public IList<ushort> Arguments
        {
            get;
        } = new List<ushort>();

        /// <summary>
        /// Writes the entry to an output stream.
        /// </summary>
        /// <param name="writer">The writer to use.</param>
        public void Write(IBigEndianWriter writer)
        {
            writer.Write(MethodRefIndex);
            writer.Write((ushort) Arguments.Count);
            foreach (ushort argument in Arguments)
                writer.Write(argument);
        }
        
    }
}