namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides members for all possible control flow behaviours a Java bytecode instruction can have.
    /// </summary>
    public enum ByteCodeFlowControl : byte
    {
        Next,
        Branch,
        ConditionalBranch,
        Call,
        Return,
        Meta,
        Throw,
        Break
    }
}