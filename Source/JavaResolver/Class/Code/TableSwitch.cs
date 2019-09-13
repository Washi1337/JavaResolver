using System.Collections.Generic;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents an offset table switch used by the tableswitch bytecode instruction.
    /// </summary>
    public class TableSwitch : ISwitchOperand
    {
        public static TableSwitch FromReader(IBigEndianReader reader)
        {
            var table = new TableSwitch
            {
                DefaultOffset = reader.ReadInt32(),
                Low = reader.ReadInt32()
            };

            int high = reader.ReadInt32();
            for (int i = 0; i < (high - table.Low + 1); i++)
                table.Offsets.Add(reader.ReadInt32());

            return table;
        }
        
        /// <summary>
        /// Gets or sets the default offset to jump to when no cases are applicable.
        /// </summary>
        public int DefaultOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lower bound of the switch block.
        /// </summary>
        public int Low
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the upper bound of the switch block.
        /// </summary>
        public int High => Low + Offsets.Count - 1; 

        /// <summary>
        /// Gets an ordered list of offsets to jump to. The index of each block indicates the case label of the switch
        /// statement.
        /// </summary>
        public IList<int> Offsets
        {
            get;
        } = new List<int>();

        public void Write(IBigEndianWriter writer, int baseOffset)
        {
            writer.Write(DefaultOffset - baseOffset);
            writer.Write(Low);
            writer.Write(High);
            foreach (var offset in Offsets)
                writer.Write(offset - baseOffset);
        }

        public override string ToString()
        {
            return " " + Low + " " + High + " " + string.Join(" ", Offsets) + " default: " + DefaultOffset;
        }

        public IEnumerable<int> GetOffsets()
        {
            return new HashSet<int>(Offsets) {DefaultOffset};
        }
    }
}