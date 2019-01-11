using System;

namespace JavaResolver.Class.Metadata
{
    /// <summary>
    /// Provides all possible accessibility attributes that can be assigned to a field.
    /// </summary>
    [Flags]
    public enum FieldAccessFlags : ushort 
    {
        Public = 0x1,
        Private = 0x2,
        Protected = 0x4,
        Static = 0x8,
        Final = 0x10,
        Volatile = 0x40,
        Transient = 0x80,
        Synthetic = 0x1000,
        Enum = 0x4000
    }
}