using System.Collections.Generic;

namespace JavaResolver.Class.Code
{
    public class ByteCodeInstructionCollection : List<ByteCodeInstruction>
    {
        /// <summary>
        /// Calculates the offsets of each instruction in the list. 
        /// </summary>
        public void CalculateOffsets()
        {
            int currentOffset = 0;
            foreach (var instruction in this)
            {
                instruction.Offset = currentOffset;
                currentOffset += instruction.Size;
            }
        }
        
        /// <summary>
        /// Searches for an instruction with the given offset.
        /// </summary>
        /// <param name="offset">The offset of the instruction to find.</param>
        /// <returns>The index the instruction is located at, or -1 if an instruction with the provided offset could not
        /// be found.</returns>
        /// <remarks>Requires the offsets of the instructions pre-calculated. This can be done by calling
        /// <see cref="CalculateOffsets"/> prior to calling this method.</remarks>
        public int GetIndexByOffset(int offset)
        {
            int left = 0;
            int right = Count - 1;

            while (left <= right)
            {
                int m = (left + right) / 2;
                int currentOffset = this[m].Offset;

                if (currentOffset > offset)
                    right = m - 1;
                else if (currentOffset < offset)
                    left = m + 1;
                else
                    return m;
            }

            return -1;
        }
        
        /// <summary>
        /// Searches for an instruction with the given offset.
        /// </summary>
        /// <param name="offset">The offset of the instruction to find.</param>
        /// <returns>The instruction with the provided offset, or null if none could be found.</returns>
        /// <remarks>Requires the offsets of the instructions pre-calculated. This can be done by calling
        /// <see cref="CalculateOffsets"/> prior to calling this method.</remarks>
        public ByteCodeInstruction GetByOffset(int offset)
        {
            int index = GetIndexByOffset(offset);
            return index == -1 ? null : this[index];
        }
        
    }
}