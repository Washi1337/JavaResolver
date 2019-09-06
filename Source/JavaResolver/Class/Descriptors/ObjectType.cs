using System;

namespace JavaResolver.Class.Descriptors
{
    /// <summary>
    /// Represents an object type that is specified by a class name.
    /// </summary>
    public class ObjectType : FieldType
    {
        public static readonly ObjectType Object = new ObjectType("java/lang/Object");
        public static readonly ObjectType String = new ObjectType("java/lang/String");
        
        public ObjectType(string className)
        {
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
        }
        
        /// <inheritdoc />
        public override char Prefix => 'L';

        /// <summary>
        /// Gets the fully qualified name of the class that is referenced.
        /// </summary>
        public string ClassName
        {
            get;
        }

        /// <inheritdoc />
        public override string FullName => ClassName;

        /// <inheritdoc />
        public override string Serialize()
        {
            return Prefix + ClassName + ";";
        }

        protected bool Equals(ObjectType other)
        {
            return string.Equals(ClassName, other.ClassName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((ObjectType) obj);
        }

        public override int GetHashCode()
        {
            return (ClassName != null ? ClassName.GetHashCode() : 0);
        }
    }
}