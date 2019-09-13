using System;
using System.Collections;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a single instruction in a Java bytecode method body.
    /// </summary>
    public class ByteCodeInstruction
    {
        public ByteCodeInstruction()
        {
        }

        public ByteCodeInstruction(ByteOpCode opCode)
            : this(0, opCode, null)
        {
        }

        public ByteCodeInstruction(ByteOpCode opCode, object operand)
            : this(0, opCode, operand)
        {
        }

        public ByteCodeInstruction(int offset, ByteOpCode opCode, object operand)
        {
            Offset = offset;
            OpCode = opCode;
            Operand = operand;
        }
        
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
                case ByteCodeOperandType.ConstantIndex:
                    return 1;
                case ByteCodeOperandType.Short:
                case ByteCodeOperandType.LocalConst:
                case ByteCodeOperandType.BranchOffset:
                case ByteCodeOperandType.FieldIndex:
                case ByteCodeOperandType.MethodIndex:
                case ByteCodeOperandType.ClassIndex:
                    return 2;
                case ByteCodeOperandType.WideConstantIndex:
                case ByteCodeOperandType.WideBranchOffset:
                case ByteCodeOperandType.WideIndexCountZero:
                case ByteCodeOperandType.WideIndexByte:
                case ByteCodeOperandType.DynamicIndex:
                    return 4;
                case ByteCodeOperandType.TableSwitch:
                {
                    int padding = (int) (FileSegment.Align((uint) Offset, 4) - Offset) - 1;
                    return padding + (3 + ((TableSwitch) Operand).Offsets.Count) * 4;
                }
                case ByteCodeOperandType.LookupSwitch:
                {
                    int padding = (int) (FileSegment.Align((uint) Offset, 4) - Offset) - 1;
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

        public int GetStackPopCount()
        {
            switch (OpCode.StackBehaviourPop)
            {
                case ByteCodeStackBehaviour.None:
                    return 0;
                case ByteCodeStackBehaviour.PopRef:
                case ByteCodeStackBehaviour.PopValue1:
                    return 1;
                case ByteCodeStackBehaviour.PopRefPopValue:
                case ByteCodeStackBehaviour.PopValue2:
                    return 2;
                case ByteCodeStackBehaviour.PopRefPopValuePopValue:
                case ByteCodeStackBehaviour.PopValue3:
                    return 3;
                case ByteCodeStackBehaviour.PopValue4:
                    return 4;
                case ByteCodeStackBehaviour.VarPop:
                    var descriptor = Operand is DynamicInvocation invocation
                        ? invocation.MethodDescriptor
                        : ((MethodReference) Operand).Descriptor;
                    
                    int values = descriptor.ParameterTypes.Count;
                    switch (OpCode.Code)
                    {
                        case ByteCode.InvokeVirtual:
                        case ByteCode.InvokeInterface:
                        case ByteCode.InvokeSpecial:
                            values++;
                            break;
                    }

                    return values;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int GetStackPushCount()
        {
            switch (OpCode.StackBehaviourPush)
            {
                case ByteCodeStackBehaviour.None:
                    return 0;
                case ByteCodeStackBehaviour.PushValue1:
                case ByteCodeStackBehaviour.PushRef:
                case ByteCodeStackBehaviour.PushEmpty:
                case ByteCodeStackBehaviour.PushAddress:
                    return 1;
                case ByteCodeStackBehaviour.PushValue2:
                case ByteCodeStackBehaviour.PushEmptyPushRef:
                    return 2;
                case ByteCodeStackBehaviour.PushValue3:
                    return 3;
                case ByteCodeStackBehaviour.PushValue4:
                    return 4;
                case ByteCodeStackBehaviour.PushValue5:
                    return 5;
                case ByteCodeStackBehaviour.PushValue6:
                    return 6;
                case ByteCodeStackBehaviour.VarPush:
                    var descriptor = Operand is DynamicInvocation invocation
                        ? invocation.MethodDescriptor
                        : ((MethodReference) Operand).Descriptor;
                    
                    return descriptor.ReturnType.Prefix == 'V' ? 0 : 1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}