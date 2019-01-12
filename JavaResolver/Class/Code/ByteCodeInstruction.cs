namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a single instruction in a Java bytecode method body.
    /// </summary>
    public class ByteCodeInstruction
    {
        /// <summary>
        /// Gets or sets the offset of the instruction relative to the start of the method body.
        /// </summary>
        public int Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the operation that the instruction executes.
        /// </summary>
        public ByteOpCode OpCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the operand used by the operation (if any).
        /// </summary>
        public object Operand
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{Offset}: {OpCode} {Operand}";
        }
    }
}