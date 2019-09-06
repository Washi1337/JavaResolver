namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Provides all possible types of constants that can be stored in a constant pool of a Java class file.
    /// </summary>
    public enum ConstantPoolTag : byte
    {
        Class = 7,
        FieldRef = 9,
        MethodRef = 10,
        InterfaceMethodRef = 11,
        String = 8,
        Integer = 3,
        Float = 4,
        Long = 5,
        Double = 6,
        NameAndType = 12,
        Utf8 = 1,
        MethodHandle = 15,
        MethodType = 16,
        InvokeDynamic = 18
    }
}