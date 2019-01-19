namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a single bytecode opcode that can be used in an instruction.
    /// It provides also additional information about the opcode such as operand type.
    /// </summary>
    /// <remarks>
    /// This structure is not meant to be instantiated by itself. Rather, make use of the <see cref="ByteOpCodes"/>
    /// class to obtain any of the available opcodes.
    /// </remarks>
    public struct ByteOpCode
    {
        internal ByteOpCode(ByteCode code, int attributes)
        {
            Code = code;
            OperandType = (ByteCodeOperandType) (attributes & 0xFF);
            StackBehaviourPush = (ByteCodeStackBehaviour) ((attributes >> 8) & 0xFF);
            StackBehaviourPop = (ByteCodeStackBehaviour) ((attributes >> 16) & 0xFF);
            FlowControl = (ByteCodeFlowControl) ((attributes >> 24) & 0xFF);
            ByteOpCodes.All[(int) code] = this;
        }

        /// <summary>
        /// Gets the raw opcode value. 
        /// </summary>
        /// <remarks>
        /// This value can be casted to a <see cref="byte"/> to obtain the raw byte representation of the opcode.
        /// </remarks>
        public ByteCode Code
        {
            get;
        }

        /// <summary>
        /// Gets the type of operand this opcode is using.
        /// </summary>
        public ByteCodeOperandType OperandType
        {
            get;
        }

        public ByteCodeStackBehaviour StackBehaviourPush
        {
            get;
            set;
        }

        public ByteCodeStackBehaviour StackBehaviourPop
        {
            get;
            set;
        }

        public ByteCodeFlowControl FlowControl
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Code.ToString().ToLowerInvariant();
        }
    }
}