using System;

namespace JavaResolver.Class.Code
{
    public class StackInbalanceException : Exception
    {
        public StackInbalanceException(ByteCodeMethodBody body, int offset)
            : base(string.Format("Stack inbalance was detected at offset {0:X4} in method body of {1}", offset, body))
        {
            Body = body;
            Offset = offset;
        }
  
        public ByteCodeMethodBody Body
        {
            get;
        }
  
        public int Offset
        {
            get;
        }
    }
}