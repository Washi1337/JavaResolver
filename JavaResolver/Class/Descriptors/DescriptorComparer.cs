using System;
using System.Collections.Generic;
using System.Linq;

namespace JavaResolver.Class.Descriptors
{
    public class DescriptorComparer : 
        IEqualityComparer<FieldDescriptor>,
        IEqualityComparer<MethodDescriptor>,
        IEqualityComparer<FieldType>
    {
        public bool Equals(FieldDescriptor x, FieldDescriptor y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x == null || y == null)
                return false;
            return Equals(x.FieldType, y.FieldType);
        }

        public int GetHashCode(FieldDescriptor obj)
        {
            return GetHashCode(obj.FieldType);
        }

        public bool Equals(MethodDescriptor x, MethodDescriptor y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x == null || y == null)
                return false;

            return Equals(x.ReturnType, y.ReturnType)
                   && x.ParameterTypes.SequenceEqual(y.ParameterTypes, this);
        }

        public int GetHashCode(MethodDescriptor obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Equals(FieldType x, FieldType y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x == null || y == null)
                return false;
            
            switch (x)
            {
                case ArrayType arrayType:
                    return y is ArrayType arrayType2 && Equals(arrayType.ComponentType, arrayType2.ComponentType);
                case BaseType baseType:
                    return y is BaseType baseType2 && baseType.Value == baseType2.Value;
                case ObjectType objectType:
                    return y is ObjectType objectType2 && objectType.ClassName == objectType2.ClassName;
            }

            return false;
        }

        public int GetHashCode(FieldType obj)
        {
            throw new System.NotImplementedException();
        }
    }
}