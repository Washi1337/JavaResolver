using JavaResolver.Class.Constants;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides a high-level representation of an exception handler in a method body of a Java class file.
    /// </summary>
    public class ExceptionHandler
    {
        public ExceptionHandler()
        {
        }
        
        internal ExceptionHandler(JavaClassImage classImage, ByteCodeMethodBody byteCodeMethodBody, ExceptionHandlerInfo info)
        {
            Start = byteCodeMethodBody.Instructions.GetByOffset(info.StartOffset);
            End = byteCodeMethodBody.Instructions.GetByOffset(info.EndOffset);
            HandlerStart = byteCodeMethodBody.Instructions.GetByOffset(info.HandlerOffset);
            CatchType = classImage.ResolveClass(info.CatchType);
        }
        
        /// <summary>
        /// Gets or sets the first instruction of the protected code range.
        /// </summary>
        public ByteCodeInstruction Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the final instruction of the protected code range.
        /// </summary>
        public ByteCodeInstruction End
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first instruction of the handler block in the protected code range.
        /// </summary>
        public ByteCodeInstruction HandlerStart
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of exception that is caught (when applicable). 
        /// </summary>
        /// <remarks>
        /// When this property is <c>null</c>, any exception type will be caught. This is also how the finally block
        /// is implemented.
        /// </remarks>
        public ClassReference CatchType
        {
            get;
            set;
        }
    }
}