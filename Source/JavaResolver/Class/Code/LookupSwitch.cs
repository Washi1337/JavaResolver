using System.Collections.Generic;
using System.Linq;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a lookup table used by the lookupswitch bytecode instruction.
    /// This table is essentially a key-value collection containing all cases and their corresponding values.
    /// </summary>
    public class LookupSwitch : ISwitchOperand
    {
        /// <summary>
        /// Reads a single lookup switch at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The lookup switch</returns>
        public static LookupSwitch FromReader(IBigEndianReader reader)
        {
            var lookup = new LookupSwitch
            {
                DefaultOffset = reader.ReadInt32(),
            };
            
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int key = reader.ReadInt32();
                int offset = reader.ReadInt32();
                lookup.Table[key] = offset;
            }

            return lookup;
        }

        /// <summary>
        /// Gets or sets the default offset of the switch.
        /// </summary>
        public int DefaultOffset
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets the key-offset mapping present in the switch. Each key represents the value to be tested, and
        /// each value is the offset to the instruction to jump to.
        /// </summary>
        public IDictionary<int, int> Table
        {
            get;
        } = new SortedDictionary<int, int>();
        
        public void Write(IBigEndianWriter writer, int baseOffset)
        {
            writer.Write(DefaultOffset - baseOffset);
            writer.Write(Table.Count);
            foreach (var entry in Table)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value - baseOffset);
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Table.Select(x => $"{x.Key}: {x.Value}")) + " default: " + DefaultOffset;
        }

        public IEnumerable<int> GetOffsets()
        {
            return new HashSet<int>(Table.Values) {DefaultOffset};
        }
    }
}