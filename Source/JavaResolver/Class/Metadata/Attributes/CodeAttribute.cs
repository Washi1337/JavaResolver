using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.Metadata.Attributes
{
    /// <summary>
    /// Represents the code attribute that is associated to methods in a Java class file.
    /// This attribute contains the body of a method, including the raw instructions, as well as extra metadata
    /// such as exception handlers, and information about local variables.
    /// </summary>
    public class CodeAttribute : IAttributeContents, IAttributeProvider
    {
        public const string AttributeName = "Code";
        
        /// <summary>
        /// Reads a single code attribute at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The code attribute that was read.</returns>
        public static CodeAttribute FromReader(IBigEndianReader reader)
        {
            var contents = new CodeAttribute
            {
                MaxStack = reader.ReadUInt16(),
                MaxLocals = reader.ReadUInt16(),
                Code = reader.ReadBytes(reader.ReadInt32()),
            };

            ushort handlerCount = reader.ReadUInt16();
            for (int i = 0; i < handlerCount; i++)
                contents.ExceptionHandlers.Add(ExceptionHandlerInfo.FromReader(reader));

            ushort attributeCount = reader.ReadUInt16();
            for (int i = 0; i < attributeCount; i++)
                contents.Attributes.Add(AttributeInfo.FromReader(reader));

            return contents;
        }

        /// <inheritdoc />
        public string Name => AttributeName;
        
        /// <summary>
        /// Gets or sets a value indicating the maximum amount of values that can be stored on the stack in this method.
        /// </summary>
        public ushort MaxStack
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the maximum amount of local variables that can be used in this method.
        /// </summary>
        public ushort MaxLocals
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the raw bytes that form the bytecode of this method.
        /// </summary>
        public byte[] Code
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of exception handlers used in this method.
        /// </summary>
        public IList<ExceptionHandlerInfo> ExceptionHandlers
        {
            get;
        } = new List<ExceptionHandlerInfo>();
        
        /// <summary>
        /// Gets a collection of additional attributes associated to this method.
        /// </summary>
        public IList<AttributeInfo> Attributes
        {
            get;
        } = new List<AttributeInfo>();

        public byte[] Serialize(BuildingContext context)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);

                writer.Write(MaxStack);
                writer.Write(MaxLocals);
                writer.Write(Code.Length);
                writer.Write(Code);

                writer.Write((ushort) ExceptionHandlers.Count);
                foreach (var handler in ExceptionHandlers)
                    handler.Write(writer);

                writer.Write((ushort) Attributes.Count);
                var writingContext = new WritingContext(writer);
                foreach (var attribute in Attributes)
                    attribute.Write(writingContext);

                return stream.ToArray();
            }
        }
    }
}