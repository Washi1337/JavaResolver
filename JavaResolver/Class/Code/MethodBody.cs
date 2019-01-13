using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using JavaResolver.Class.Emit;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Represents a mutable method body of a method definition stored in a Java class file.
    /// </summary>
    public class MethodBody 
    {
        public MethodBody()
        {
        }

        internal MethodBody(JavaClassImage classImage, CodeAttribute attribute)
        {
            var disassembler = new ByteCodeDisassembler(new MemoryBigEndianReader(attribute.Code));
            foreach (var instruction in disassembler.ReadInstructions())
                Instructions.Add(instruction);

            foreach (var handler in attribute.ExceptionHandlers)
                ExceptionHandlers.Add(new ExceptionHandler(classImage, this, handler));

            foreach (var attr in attribute.Attributes)
                ExtraAttributes.Add(attr);
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

        public IList<AttributeInfo> ExtraAttributes
        {
            get;
        } = new List<AttributeInfo>();
        
        public CodeAttribute Serialize(BuildingContext context)
        {
            var result = new CodeAttribute();

            result.Code = GenerateRawCode(context);

            foreach (var info in GenerateExceptionHandlerInfos(context))
                result.ExceptionHandlers.Add(info);

            foreach (var attr in ExtraAttributes)
                result.Attributes.Add(attr);
            
            return result;
        }

        private byte[] GenerateRawCode(BuildingContext context)
        {
            byte[] code = null;
            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                var assembler = new ByteCodeAssembler(writer)
                {
                    OperandBuilder = new DefaultOperandBuilder(context.Builder.ConstantPoolBuffer)
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