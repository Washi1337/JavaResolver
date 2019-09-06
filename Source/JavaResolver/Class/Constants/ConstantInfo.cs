using System;

namespace JavaResolver.Class.Constants
{
    /// <summary>
    /// Provides a base for all constants stored in the constant pool of a Java class file.
    /// </summary>
    public abstract class ConstantInfo : FileSegment
    {
        /// <summary>
        /// Reads a single constant entry at the current position of the provided reader.
        /// </summary>
        /// <param name="reader">The reader to use.</param>
        /// <returns>The constant that was read.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when the reader does not point to a valid constant entry.
        /// </exception>
        public static ConstantInfo FromReader(IBigEndianReader reader)
        {
            var tag = (ConstantPoolTag) reader.ReadByte();
            switch (tag)
            {
                case ConstantPoolTag.Class:
                    return ClassInfo.FromReader(reader);
                case ConstantPoolTag.FieldRef:
                    return FieldRefInfo.FromReader(reader);
                case ConstantPoolTag.MethodRef:
                    return MethodRefInfo.FromReader(reader);
                case ConstantPoolTag.InterfaceMethodRef:
                    return InterfaceMethodRefInfo.FromReader(reader);
                case ConstantPoolTag.String:
                    return StringInfo.FromReader(reader);
                case ConstantPoolTag.Integer:
                    return new PrimitiveInfo(reader.ReadInt32());
                case ConstantPoolTag.Float:
                    return new PrimitiveInfo(reader.ReadSingle());
                case ConstantPoolTag.Long:
                    return new PrimitiveInfo(reader.ReadInt64());
                case ConstantPoolTag.Double:
                    return new PrimitiveInfo(reader.ReadDouble());
                case ConstantPoolTag.NameAndType:
                    return NameAndTypeInfo.FromReader(reader);
                case ConstantPoolTag.Utf8:
                    return Utf8Info.FromReader(reader);
                case ConstantPoolTag.MethodHandle:
                    return MethodHandleInfo.FromReader(reader);
                case ConstantPoolTag.MethodType:
                    return MethodTypeInfo.FromReader(reader);
                case ConstantPoolTag.InvokeDynamic:
                    return InvokeDynamicInfo.FromReader(reader);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// Gets the tag associated to the constant.
        /// </summary>
        public abstract ConstantPoolTag Tag
        {
            get;
        }

        public override void Write(WritingContext context)
        {
            context.Writer.Write((byte) Tag);
        }
    }
}