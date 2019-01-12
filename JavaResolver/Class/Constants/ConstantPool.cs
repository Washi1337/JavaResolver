using System.Collections.Generic;

namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents the constant pool in a Java class file. This is a pool containing all literals
    /// (strings, integers, etc.) as well as references to external members and other metadata.
    /// </summary>
    public class ConstantPool : FileSegment
    {
        /// <summary>
        /// Reads an entire constant pool at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The constant pool that was read.</returns>
        public static ConstantPool FromReader(IBigEndianReader reader)
        {
            var pool = new ConstantPool
            {
                StartOffset = reader.StartPosition
            };
            
            ushort count = reader.ReadUInt16();

            for (int i = 0; i < count - 1; i++)
                pool.Constants.Add(ConstantInfo.FromReader(reader));

            return pool;
        }

        /// <summary>
        /// Gets a collection of all constants stored in the constant pool.
        /// </summary>
        public IList<ConstantInfo> Constants
        {
            get;
        } = new List<ConstantInfo>();

        public ConstantInfo ResolveConstant(int index)
        {
            index--;
            return index >= 0 && index < Constants.Count 
                ? Constants[index] 
                : null;
        }

        public string ResolveString(int index)
        {
            var constantInfo = ResolveConstant(index);
            return constantInfo is Utf8Info utf8Info
                ? utf8Info.Value
                : null;
        }

        public override void Write(WritingContext context)
        {
            var writer = context.Writer;
            writer.Write((ushort) (Constants.Count + 1));
            foreach (var constant in Constants)
                constant.Write(context);
        }
    }
}