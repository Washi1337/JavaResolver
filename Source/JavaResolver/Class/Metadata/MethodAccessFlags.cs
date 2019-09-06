using System;

namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Provides all possible accessibility attributes that can be assigned to a method defined in a Java class file.
    /// </summary>
    [Flags]
    public enum MethodAccessFlags : ushort
    {
        Public = 0x1,
        Private = 0x2,
        Protected = 0x4,
        Static = 0x8,
        Final = 0x10,
        Synchronized = 0x20,
        Bridge = 0x40,
        Varargs = 0x80,
        Native = 0x100,
        Abstract = 0x400,
        Strict = 0x800,
        Synthetic = 0x1000
    }
}