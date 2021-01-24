using System.Collections.Generic;

namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Represents the constant pool in a Java class file. This is a pool containing all literals
    /// (strings, integers, etc.) as well as references to external members and other metadata.
    /// </summary>
    public class ConstantPool : FileSegment
    {
        private readonly SortedList<int, ConstantInfo> _constants = new SortedList<int, ConstantInfo>();

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

            for (var index = 1; index < count; )
            {
                var constant = ConstantInfo.FromReader(reader);
                pool._constants.Add(index, constant);
                index += constant.Size;
            }

            return pool;
        }

        /// <summary>
        /// Gets a dictionary of all constants stored in the constant pool, keyed by pool index.
        /// </summary>
        public IDictionary<int, ConstantInfo> Constants => _constants;
        
        /// <summary>
        /// Gets a constant by its index.
        /// </summary>
        /// <param name="index">Index of a constant.</param>
        /// <returns>An instance of <see cref="ConstantInfo"/> or <c>null</c> if no constant with given index exists.</returns>
        public ConstantInfo ResolveConstant(int index)
        {
            _constants.TryGetValue(index, out var constant);
            return constant;
        }

        /// <summary>
        /// Gets a constant string by its index.
        /// </summary>
        /// <param name="index">Index of an UTF8 constant.</param>
        /// <returns>Value of an UTF8 constant or <c>null</c> if a constant with given index does not exists or is not an UTF8 constant.</returns>
        public string ResolveString(int index)
        {
            var constantInfo = ResolveConstant(index);
            return constantInfo is Utf8Info utf8Info
                ? utf8Info.Value
                : null;
        }

        /// <summary>
        /// Adds a constant to the pool.
        /// </summary>
        /// <param name="constant">The constant to add.</param>
        /// <returns>The index assigned to the constant.</returns>
        public int AddConstant(ConstantInfo constant)
        {
            var index = GetIndexCount();
            _constants.Add(index, constant);
            return index;
        }

        public override void Write(WritingContext context)
        {
            var writer = context.Writer;
            var count = GetIndexCount();
            writer.Write((ushort) count);
            foreach (var constant in _constants.Values)
                constant.Write(context);
        }
        
        private int GetIndexCount()
        {
            if (_constants.Count == 0)
                return 1;

            var lastIndex = _constants.Keys[_constants.Count - 1];
            var lastConstant = _constants[lastIndex];
            return lastIndex + lastConstant.Size;
        }
    }
}