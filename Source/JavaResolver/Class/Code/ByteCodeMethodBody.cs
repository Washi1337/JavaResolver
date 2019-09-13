using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using JavaResolver.Class.Emit;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a mutable method body of a method definition stored in a Java class file.
    /// </summary>
    public class ByteCodeMethodBody : IExtraAttributeProvider
    {        
        /// <summary>
        /// Provides information about the state of the stack at a particular point of execution in a method.  
        /// </summary>
        private struct StackState
        {
            /// <summary>
            /// The index of the instruction the state is associated to.
            /// </summary>
            public readonly int InstructionIndex;
            
            /// <summary>
            /// The number of values currently on the stack.
            /// </summary>
            public readonly int StackSize;

            public StackState(int instructionIndex, int stackSize)
            {
                InstructionIndex = instructionIndex;
                StackSize = stackSize;
            }

#if DEBUG
            public override string ToString()
            {
                return $"InstructionIndex: {InstructionIndex}, StackSize: {StackSize}";
            }
#endif
        }
        
        public ByteCodeMethodBody()
        {
        }

        internal ByteCodeMethodBody(JavaClassImage classImage, CodeAttribute attribute)
        {
            MaxStack = attribute.MaxStack;

            // Read instructions.
            var disassembler = new ByteCodeDisassembler(new MemoryBigEndianReader(attribute.Code))
            {
                OperandResolver = new DefaultOperandResolver(classImage)
            };
            foreach (var instruction in disassembler.ReadInstructions())
                Instructions.Add(instruction);

            // Read exception handlers.
            foreach (var handler in attribute.ExceptionHandlers)
                ExceptionHandlers.Add(new ExceptionHandler(classImage, this, handler));

            // Read attributes.
            foreach (var attr in attribute.Attributes)
            {
                string name = classImage.ClassFile.ConstantPool.ResolveString(attr.NameIndex);
                switch (name)
                {
                    // Local variables
                    case LocalVariableTableAttribute.AttributeName :
                        var localsTable = LocalVariableTableAttribute.FromReader(new MemoryBigEndianReader(attr.Contents));
                        foreach (var info in localsTable.LocalVariables)
                            Variables.Add(new LocalVariable(classImage, this, info));
                        break;
                      
                        
                    default:
                        ExtraAttributes.Add(name, attr.Clone());
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="MaxStack"/> field should be recalculated upon rebuilding
        /// the method body. Set to false if a custom value is provided.
        /// </summary>
        public bool ComputeMaxStackOnBuild
        {
            get;
            set;
        } = true;

        /// <summary>
        /// Gets a value indicating the maximum amount of values that can be pushed onto the stack.
        /// </summary>
        public int MaxStack
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets a collection of instructions stored in the method body.
        /// </summary>
        public ByteCodeInstructionCollection Instructions
        {
            get;
        } = new ByteCodeInstructionCollection();

        /// <summary>
        /// Gets a collection of exception handlers that the method body defines. 
        /// </summary>
        public IList<ExceptionHandler> ExceptionHandlers
        {
            get;
        } = new List<ExceptionHandler>();

        /// <summary>
        /// Gets a collection of local variables that the method defines.
        /// </summary>
        public IList<LocalVariable> Variables
        {
            get;
        } = new List<LocalVariable>();
        
        public IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        } = new Dictionary<string, AttributeInfo>();
        
        public CodeAttribute Serialize(BuildingContext context)
        {
            // Verify stack.
            if (ComputeMaxStackOnBuild)
                MaxStack = ComputeMaxStack();

            // Code.
            var result = new CodeAttribute
            {
                Code = GenerateRawCode(context), 
                MaxStack = (ushort) MaxStack,
                MaxLocals = (ushort) Variables.Count
            };

            // Exception handlers.
            foreach (var info in GenerateExceptionHandlerInfos(context))
                result.ExceptionHandlers.Add(info);

            // Variables.
            if (Variables.Count > 0)
            {
                var localsAttribute = new LocalVariableTableAttribute();
                foreach (var variable in Variables)
                {
                    localsAttribute.LocalVariables.Add(new LocalVariableInfo
                    {
                        StartOffset = (ushort) variable.Start.Offset,
                        Length = (ushort) ((variable.End?.Offset ?? result.Code.Length) - variable.Start.Offset),
                        NameIndex = (ushort) context.Builder.ConstantPoolBuffer.GetUtf8Index(variable.Name),
                        DescriptorIndex = (ushort) context.Builder.ConstantPoolBuffer.GetDescriptorIndex(variable.Descriptor),
                        LocalIndex = (ushort) variable.Index,
                    });
                }

                result.Attributes.Add(context.Builder.CreateAttribute(context, localsAttribute));
            }

            // Additional attributes.
            context.Builder.AddAttributes(context, result, this);
            
            return result;
        }

        /// <summary>
        /// Computes the maximum values pushed onto the stack by this method body.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="StackInbalanceException">Occurs when the method body will result in an unbalanced stack.</exception>
        /// <remarks>This method will force the offsets of each instruction to be calculated.</remarks>
        public int ComputeMaxStack()
        {
            if (Instructions.Count == 0)
                return 0;
            
            Instructions.CalculateOffsets();

            var visitedInstructions = new Dictionary<int, StackState>();
            var agenda = new Stack<StackState>();

            // Add entrypoints to agenda.
            agenda.Push(new StackState(0, 0));
            foreach (var handler in ExceptionHandlers)
            {
                agenda.Push(new StackState(Instructions.GetIndexByOffset(handler.Start.Offset), 0));
                agenda.Push(new StackState(Instructions.GetIndexByOffset(handler.HandlerStart.Offset), 1));
            }

            while (agenda.Count > 0)
            {
                var currentState = agenda.Pop();
                if (currentState.InstructionIndex >= Instructions.Count)
                {
                    var last = Instructions[Instructions.Count - 1];
                    throw new StackInbalanceException(this, last.Offset + last.Size);
                }

                var instruction = Instructions[currentState.InstructionIndex];

                if (visitedInstructions.TryGetValue(currentState.InstructionIndex, out var visitedState))
                {
                    // Check if previously visited state is consistent with current observation.
                    if (visitedState.StackSize != currentState.StackSize)
                        throw new StackInbalanceException(this, instruction.Offset);
                }
                else
                {
                    // Mark instruction as visited and store current state.
                    visitedInstructions[currentState.InstructionIndex] = currentState;

                    // Compute next stack size.
                    int nextStackSize = currentState.StackSize - instruction.GetStackPopCount();
                    if (nextStackSize < 0)
                        throw new StackInbalanceException(this, instruction.Offset);
                    nextStackSize += instruction.GetStackPushCount();

                    // Add outgoing edges to agenda.
                    switch (instruction.OpCode.FlowControl)
                    {
                        case ByteCodeFlowControl.Branch:
                            agenda.Push(new StackState(
                                Instructions.GetIndexByOffset(((ByteCodeInstruction) instruction.Operand).Offset),
                                nextStackSize));
                            break;
                        case ByteCodeFlowControl.ConditionalBranch:
                            switch (instruction.OpCode.OperandType)
                            {
                                case ByteCodeOperandType.BranchOffset:
                                    agenda.Push(new StackState(
                                        Instructions.GetIndexByOffset(((ByteCodeInstruction) instruction.Operand).Offset),
                                        nextStackSize));
                                    break;
                                case ByteCodeOperandType.TableSwitch:
                                case ByteCodeOperandType.LookupSwitch:
                                    foreach (int offset in ((ISwitchOperand) instruction.Operand).GetOffsets())
                                        agenda.Push(new StackState(
                                            Instructions.GetIndexByOffset(offset),
                                            nextStackSize));
                                    break;
                            }
                            agenda.Push(new StackState(
                                currentState.InstructionIndex + 1,
                                nextStackSize));
                            break;
                        case ByteCodeFlowControl.Call:
                        case ByteCodeFlowControl.Break:
                        case ByteCodeFlowControl.Meta:
                        case ByteCodeFlowControl.Next:
                            agenda.Push(new StackState(
                                currentState.InstructionIndex + 1,
                                nextStackSize));
                            break;
                        case ByteCodeFlowControl.Return:
                            if (nextStackSize != 1)
                                throw new StackInbalanceException(this, instruction.Offset);
                            break;
                    }
                }
            }

            return visitedInstructions.Max(x => x.Value.StackSize);
        }
        
        private byte[] GenerateRawCode(BuildingContext context)
        {
            byte[] code = null;
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                var assembler = new ByteCodeAssembler(writer)
                {
                    OperandBuilder = new DefaultOperandBuilder(context.Builder)
                };
                
                Instructions.CalculateOffsets();
                foreach (var instruction in Instructions)
                    assembler.Write(instruction);

                code = stream.ToArray();
            }

            return code;
        }

        private IEnumerable<ExceptionHandlerInfo> GenerateExceptionHandlerInfos(BuildingContext context)
        {
            foreach (var handler in ExceptionHandlers)
            {
                yield return new ExceptionHandlerInfo
                {
                    StartOffset = (ushort) handler.Start.Offset,
                    EndOffset = (ushort) handler.End.Offset,
                    HandlerOffset = (ushort) handler.HandlerStart.Offset,
                    CatchType = (ushort) (handler.CatchType != null
                        ? context.Builder.ConstantPoolBuffer.GetClassIndex(handler.CatchType) : 0)
                };
            }
        }
    }
}