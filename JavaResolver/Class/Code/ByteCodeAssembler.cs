using System;
using JavaResolver.Class.Constants;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides a mechanism for writing bytecode instructions to an output stream.
    /// </summary>
    public class ByteCodeAssembler
    {
        private readonly IBigEndianWriter _writer;

        public ByteCodeAssembler(IBigEndianWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <summary>
        /// Gets or sets the operand builder to use for transforming high level representations of operands
        /// into their corresponding indices or bytes.
        /// </summary>
        public IOperandBuilder OperandBuilder
        {
            get;
            set;
        }
        
        /// <summary>
        /// Writes a single instruction to the output stream.
        /// </summary>
        /// <param name="instruction">The instructoin to write.</param>
        public void Write(ByteCodeInstruction instruction)
        {
            _writer.Write((byte) instruction.OpCode.Code);

            WriteOperand(instruction);
        }

        private void WriteOperand(ByteCodeInstruction instruction)
        {
            switch (instruction.OpCode.OperandType)
            {
                case ByteCodeOperandType.None:
                    break;
                case ByteCodeOperandType.Byte:
                case ByteCodeOperandType.LocalIndex:
                case ByteCodeOperandType.PrimitiveType:
                    _writer.Write(Convert.ToByte(instruction.Operand));
                    break;
                case ByteCodeOperandType.Short:
                case ByteCodeOperandType.LocalConst:
                    _writer.Write(Convert.ToInt16(instruction.Operand));
                    break;
                case ByteCodeOperandType.FieldIndex:
                case ByteCodeOperandType.MethodIndex:
                case ByteCodeOperandType.ClassIndex:
                case ByteCodeOperandType.ConstantIndex:
                    _writer.Write((ushort) GetIndex(instruction.Operand));
                    break;
                case ByteCodeOperandType.WideConstantIndex:
                    _writer.Write(GetIndex(instruction.Operand));
                    break;
                case ByteCodeOperandType.BranchOffset:
                    _writer.Write(Convert.ToInt16(instruction.Operand));
                    break;
                case ByteCodeOperandType.WideBranchOffset:
                    _writer.Write(Convert.ToInt32(instruction.Operand));
                    break;
                case ByteCodeOperandType.TableSwitch:
                    _writer.Position = FileSegment.Align((uint) _writer.Position, 4);
                    ((TableSwitch) instruction.Operand).Write(_writer);
                    break;
                case ByteCodeOperandType.LookupSwitch:
                    _writer.Position = FileSegment.Align((uint) _writer.Position, 4);
                    ((LookupSwitch) instruction.Operand).Write(_writer);
                    break;
                case ByteCodeOperandType.WideIndexCountZero:
                case ByteCodeOperandType.WideConstantIndexByte:
                    _writer.Write(Convert.ToInt32(instruction.Operand));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int GetIndex(object operand)
        {
            if (OperandBuilder != null)
            {
                switch (operand)
                {
                    case string text:
                        return OperandBuilder.GetStringIndex(text);
                    case ConstantInfo constantInfo:
                        return OperandBuilder.GetConstantIndex(constantInfo);
                } 
            }

            return Convert.ToInt32(operand);
        }
    }
}