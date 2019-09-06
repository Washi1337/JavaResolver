namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Provides all possible reference kinds that can be used in a <see cref="MethodHandleInfo"/>.
    /// </summary>
    public enum MethodReferenceKind : byte
    {
        GetField = 1,
        GetStatic = 2,
        PutField = 3,
        PutStatic = 4,
        InvokeVirtual = 5,
        InvokeStatic = 6,
        InvokeSpecial = 7,
        NewInvokeSpecial = 8,
        InvokeInterface = 9
    }
}