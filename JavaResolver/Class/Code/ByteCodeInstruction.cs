using System;
using System.Collections;

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
        
        /// <summary>
        /// Gets the size in bytes of the instruction.  
        /// </summary>
        /// <remarks>
        /// The size of the tableswitch and lookupswitch opcodes depend on the offset they are located at.
        /// When querying the size of such instructions, make sure the <see cref="Offset"/> property is set properly. 
        /// </remarks>
        public int Size => 1 + GetOperandSize();

        private int GetOperandSize()
        {
            switch (OpCode.OperandType)
            {
                case ByteCodeOperandType.None:
                    return 0;
                case ByteCodeOperandType.Byte:
                case ByteCodeOperandType.LocalIndex:
                case ByteCodeOperandType.PrimitiveType:
                    return 1;
                case ByteCodeOperandType.Short:
                case ByteCodeOperandType.ConstantIndex:
                case ByteCodeOperandType.LocalConst:
                case ByteCodeOperandType.BranchOffset:
                case ByteCodeOperandType.FieldIndex:
                case ByteCodeOperandType.MethodIndex:
                case ByteCodeOperandType.ClassIndex:
                    return 2;
                case ByteCodeOperandType.WideConstantIndex:
                case ByteCodeOperandType.WideBranchOffset:
                case ByteCodeOperandType.WideIndexCountZero:
                case ByteCodeOperandType.WideConstantIndexByte:
                    return 4;
                case ByteCodeOperandType.TableSwitch:
                {
                    int padding = (int) (FileSegment.Align((uint) Offset, 4) - Offset);
                    return padding + (3 + ((TableSwitch) Operand).Offsets.Count) * 4;
                }
                case ByteCodeOperandType.LookupSwitch:
                {
                    int padding = (int) (FileSegment.Align((uint) Offset, 4) - Offset);
                    return padding + (2 + ((LookupSwitch) Operand).Table.Count * 2) * 4;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public override string ToString()
        {
            return $"{Offset}: {OpCode} {Operand}";
        }
    }
}