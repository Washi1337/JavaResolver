namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides members for resolving raw operands in the bytecode of a method to higher-level objects.
    /// </summary>
    public interface IOperandResolver
    {
        /// <summary>
        /// Resolves an index to a field.
        /// </summary>
        /// <param name="fieldIndex">The field index to resolve.</param>
        /// <returns>The resolved field.</returns>
        object ResolveField(int fieldIndex);
        
        /// <summary>
        /// Resolves an index to a method in the constant pool.
        /// </summary>
        /// <param name="methodIndex">The method index to resolve.</param>
        /// <returns>The resolved method.</returns>
        object ResolveMethod(int methodIndex);

        /// <summary>
        /// Resolves an index to a constant in the constant pool.
        /// </summary>
        /// <param name="constantIndex"></param>
        /// <returns></returns>
        object ResolveConstant(int constantIndex);
    }
}