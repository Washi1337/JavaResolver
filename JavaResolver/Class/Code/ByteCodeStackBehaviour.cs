namespace JavaResolver.Class.Code
{
    /// <summary>
    /// Provides all possible stack behaviours a Java bytecode instruction can have.
    /// </summary>
    public enum ByteCodeStackBehaviour : byte
    {
        None,
        
        PushValue1,
        PushValue2,
        PushValue3,
        PushValue4,
        PushValue5,
        PushValue6,
        
        PushRef,
        PushEmpty,
        
        VarPush,
        
        PopRef,
        PopRefPopValue,
        PopRefPopValuePopValue,
        PopValue1,
        PopValue2,
        PopValue3,
        PopValue4,
        VarPop,
        PushAddress,
        PushEmptyPushRef
    }
}