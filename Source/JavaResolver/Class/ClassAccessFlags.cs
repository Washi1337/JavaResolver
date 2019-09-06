using System;

namespace JavaResolver.Class
{
    /// <summary>
    /// Provides all possible accessibility flags that can be assigned to a class in a class file.
    /// </summary>
    [Flags]
    public enum ClassAccessFlags : ushort
    {
        Public = 0x1,
        Private = 0x2,
        Protected = 0x4,
        Static = 0x8,
        Final = 0x10,
        Super = 0x20,
        Interface = 0x200,
        Abstract = 0x400,
        Synthetic = 0x1000,
        Annotation = 0x2000,
        Enum = 0x4000
    }
}