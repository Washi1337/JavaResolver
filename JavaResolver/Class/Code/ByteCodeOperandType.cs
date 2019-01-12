namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides all possible operand types found in the Java bytecode.
    /// </summary>
    public enum ByteCodeOperandType
    {
        None,
        Byte,
        Short,
        ConstantIndex,
        WideConstantIndex,
        LocalIndex,
        LocalConst,
        BranchOffset,
        TableSwitch,
        LookupSwitch,
        WideIndexCountZero,

        WideConstantIndexByte,
        WideBranchOffset,
        FieldIndex,
        MethodIndex
    }
}