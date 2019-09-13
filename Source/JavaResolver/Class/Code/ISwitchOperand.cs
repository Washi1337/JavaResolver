using System.Collections.Generic;

namespace JavaResolver.Class.Code
{
    public interface ISwitchOperand
    {
        IEnumerable<int> GetOffsets();
        
        void Write(IBigEndianWriter writer, int baseOffset);
    }
}