using System;
using JavaResolver.Class.Constants;
using JavaResolver.Class.TypeSystem;

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
        /// <param name="instruction">The instruction to write.</param>
        /// <remarks>
        /// Some instructions have different byte layouts depending on the offsets they are located at.
        /// Make sure the offsets have been calculated before passing on the instructions to the assembler. 
        /// </remarks>
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
                    _writer.Write(instruction.Operand is FieldReference fieldRef
                        ? (ushort) OperandBuilder.GetFieldIndex(fieldRef)
                        : Convert.ToUInt16(instruction.Operand));
                    break;
                case ByteCodeOperandType.MethodIndex:
                    _writer.Write(instruction.Operand is MethodReference methodRef
                        ? (ushort) OperandBuilder.GetMethodIndex(methodRef)
                        : Convert.ToUInt16(instruction.Operand));
                    break;
                case ByteCodeOperandType.ClassIndex:
                    _writer.Write(instruction.Operand is ClassReference classRef
                        ? (ushort) OperandBuilder.GetClassIndex(classRef)
                        : Convert.ToUInt16(instruction.Operand));
                    break;
                case ByteCodeOperandType.ConstantIndex:
                    _writer.Write((byte) OperandBuilder.GetLiteralIndex(instruction.Operand));
                    break;
                case ByteCodeOperandType.WideConstantIndex:
                    _writer.Write((ushort) OperandBuilder.GetLiteralIndex(instruction.Operand));
                    break;
                case ByteCodeOperandType.BranchOffset:
                {
                    short relativeOffset;
                    if (instruction.Operand is ByteCodeInstruction target)
                        relativeOffset = (short) (target.Offset - instruction.Offset);
                    else
                        relativeOffset = Convert.ToInt16(instruction.Operand);

                    _writer.Write(relativeOffset);
                    break;
                }
                case ByteCodeOperandType.WideBranchOffset:
                {
                    int relativeOffset;
                    if (instruction.Operand is ByteCodeInstruction target)
                        relativeOffset = (target.Offset - instruction.Offset);
                    else
                        relativeOffset = Convert.ToInt32(instruction.Operand);

                    _writer.Write(relativeOffset);
                    break;
                }
                case ByteCodeOperandType.TableSwitch:
                case ByteCodeOperandType.LookupSwitch:
                    _writer.Position = FileSegment.Align((uint) _writer.Position, 4);
                    ((ISwitchOperand) instruction.Operand).Write(_writer, instruction.Offset);
                    break;
                case ByteCodeOperandType.WideIndexCountZero:
                case ByteCodeOperandType.WideIndexByte:
                    _writer.Write(Convert.ToInt32(instruction.Operand));
                    break;
                case ByteCodeOperandType.DynamicIndex:
                    _writer.Write(instruction.Operand is DynamicInvocation invocation
                        ? OperandBuilder.GetDynamicIndex(invocation) << 16
                        : Convert.ToInt32(instruction.Operand));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}