using JavaResolver.Class.TypeSystem;

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
        FieldReference ResolveField(int fieldIndex);

        /// <summary>
        /// Resolves an index to a method in the constant pool.
        /// </summary>
        /// <param name="methodIndex">The method index to resolve.</param>
        /// <returns>The resolved method.</returns>
        MethodReference ResolveMethod(int methodIndex);

        /// <summary>
        /// Resolves an index to a constant in the constant pool.
        /// </summary>
        /// <param name="constantIndex">The constant index to resolve.</param>
        /// <returns>The resolved constant.</returns>
        object ResolveConstant(int constantIndex);

        /// <summary>
        /// Resolves an index to a class in the constant pool.
        /// </summary>
        /// <param name="classIndex">The class index to resolve.</param>
        /// <returns>The resolved class.</returns>
        ClassReference ResolveClass(int classIndex);

        /// <summary>
        /// Resolves an index to a dynamic invocation in the constant pool.
        /// </summary>
        /// <param name="dynamicIndex">The invocation index to resolve.</param>
        /// <returns>The resolved invocation.</returns>
        DynamicInvocation ResolveDynamic(int dynamicIndex);
    }
}