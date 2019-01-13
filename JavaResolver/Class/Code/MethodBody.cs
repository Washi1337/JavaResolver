using System.Collections.Generic;

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

        internal MethodBody(JavaClassFile classFile, CodeAttribute attribute)
        {
            var disassembler = new ByteCodeDisassembler(new MemoryBigEndianReader(attribute.Code));
            foreach (var instruction in disassembler.ReadInstructions())
                Instructions.Add(instruction);

            foreach (var handler in attribute.ExceptionHandlers)
                ExceptionHandlers.Add(new ExceptionHandler(classFile, this, handler));
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

    }
}