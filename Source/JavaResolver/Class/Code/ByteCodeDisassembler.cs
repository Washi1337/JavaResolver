using System;
using System.Collections.Generic;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides a mechanism for parsing method bodies written in the Java bytecode format targeting the Java
    /// virtual machine.
    /// </summary>
    public class ByteCodeDisassembler
    {
        private readonly IBigEndianReader _reader;

        public ByteCodeDisassembler(IBigEndianReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <summary>
        /// Gets or sets the operand resolver used when resolving indices in operands to meaningful objects.
        /// </summary>
        /// <remarks>
        /// When this property is set to <c>null</c>, no resolution of operands will be performed.
        /// </remarks>
        public IOperandResolver OperandResolver
        {
            get;
            set;
        }
        
        /// <summary>
        /// Disassembles the entire input buffer into Java bytecode instructions, and resolves all operands wen possible.
        /// </summary>
        /// <returns>The list of disassembled instructions.</returns>
        public IList<ByteCodeInstruction> ReadInstructions()
        {
            var result = new ByteCodeInstructionCollection();

            while (_reader.Position < _reader.StartPosition + _reader.Length)
                result.Add(ReadNextInstruction());

            foreach (var instruction in result)
                instruction.Operand = ResolveOperand(result, instruction);
            
            return result;
        }

        private ByteCodeInstruction ReadNextInstruction()
        {
            int offset = (int) (_reader.Position - _reader.StartPosition);
            var opcode = ByteOpCodes.All[_reader.ReadByte()];
            var rawOperand = ReadRawOperand(opcode);

            return new ByteCodeInstruction
            {
                Offset = offset,
                OpCode = opcode,
                Operand = rawOperand
            };
        }

        private object ReadRawOperand(ByteOpCode opcode)
        {
            switch (opcode.OperandType)
            {
                case ByteCodeOperandType.None:
                    return null;
                case ByteCodeOperandType.Byte:
                case ByteCodeOperandType.ConstantIndex:
                case ByteCodeOperandType.LocalIndex:
                case ByteCodeOperandType.PrimitiveType:
                    return _reader.ReadByte();
                case ByteCodeOperandType.Short:
                case ByteCodeOperandType.WideConstantIndex:
                case ByteCodeOperandType.BranchOffset:
                case ByteCodeOperandType.LocalConst:
                case ByteCodeOperandType.FieldIndex:
                case ByteCodeOperandType.MethodIndex:
                case ByteCodeOperandType.ClassIndex:
                    return _reader.ReadInt16();
                    
                case ByteCodeOperandType.TableSwitch:
                    _reader.Position = FileSegment.Align((uint) _reader.Position, 4);
                    return TableSwitch.FromReader(_reader);
                
                case ByteCodeOperandType.LookupSwitch:
                    _reader.Position = FileSegment.Align((uint) _reader.Position, 4);
                    return LookupSwitch.FromReader(_reader);
                
                case ByteCodeOperandType.WideIndexCountZero:
                case ByteCodeOperandType.WideIndexByte:
                case ByteCodeOperandType.WideBranchOffset:
                case ByteCodeOperandType.DynamicIndex:
                    return _reader.ReadInt32();
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private object ResolveOperand(ByteCodeInstructionCollection allInstructions, ByteCodeInstruction instruction)
        {    
            switch (instruction.OpCode.OperandType)
            {
                case ByteCodeOperandType.PrimitiveType:
                    return (PrimitiveType) (byte) instruction.Operand;
                
                case ByteCodeOperandType.ConstantIndex:
                case ByteCodeOperandType.WideConstantIndex:
                    return OperandResolver?.ResolveConstant(Convert.ToInt32(instruction.Operand)) ?? instruction.Operand;
                
                case ByteCodeOperandType.FieldIndex:
                    return OperandResolver?.ResolveField(Convert.ToInt32(instruction.Operand)) ?? instruction.Operand;
                
                case ByteCodeOperandType.MethodIndex:
                    return OperandResolver?.ResolveMethod(Convert.ToInt32(instruction.Operand)) ?? instruction.Operand;
                
                case ByteCodeOperandType.ClassIndex:
                    return OperandResolver?.ResolveClass(Convert.ToInt32(instruction.Operand)) ?? instruction.Operand;
                
                case ByteCodeOperandType.DynamicIndex:
                    return OperandResolver?.ResolveDynamic(Convert.ToInt32(instruction.Operand) >> 16)?? instruction.Operand;
                
                case ByteCodeOperandType.BranchOffset:
                case ByteCodeOperandType.WideBranchOffset:
                    int offset = Convert.ToInt32(instruction.Operand);
                    return allInstructions.GetByOffset(instruction.Offset + offset) ?? instruction.Operand;

                case ByteCodeOperandType.TableSwitch:
                    var table = (TableSwitch) instruction.Operand;
                    table.DefaultOffset += instruction.Offset;
                    for (int i = 0; i < table.Offsets.Count; i++)
                        table.Offsets[i] += instruction.Offset;
                    return table;

                case ByteCodeOperandType.LookupSwitch:
                    var lookup = (LookupSwitch) instruction.Operand;
                    lookup.DefaultOffset += instruction.Offset;
                    foreach (int key in lookup.Table.Keys)
                        lookup.Table[key] += instruction.Offset;
                    return lookup;
            }

            return instruction.Operand;
        }
    }
}