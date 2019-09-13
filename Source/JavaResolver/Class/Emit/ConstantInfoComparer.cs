using System.Collections.Generic;
using JavaResolver.Class.Constants;

namespace JavaResolver.Class.Emit
{
    public class ConstantInfoComparer :
        IEqualityComparer<ClassInfo>,
        IEqualityComparer<InvokeDynamicInfo>,
        IEqualityComparer<MemberRefInfo>,
        IEqualityComparer<MethodHandleInfo>,
        IEqualityComparer<MethodTypeInfo>,
        IEqualityComparer<NameAndTypeInfo>,
        IEqualityComparer<PrimitiveInfo>,
        IEqualityComparer<StringInfo>,
        IEqualityComparer<Utf8Info>
        

    {
        public bool Equals(ClassInfo x, ClassInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.NameIndex == y.NameIndex;
        }

        public int GetHashCode(ClassInfo obj)
        {
            return obj.NameIndex.GetHashCode();
        }

        public bool Equals(InvokeDynamicInfo x, InvokeDynamicInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.NameAndTypeIndex == y.NameAndTypeIndex && x.BootstrapMethodIndex == y.BootstrapMethodIndex;
        }

        public int GetHashCode(InvokeDynamicInfo obj)
        {
            return (obj.BootstrapMethodIndex << 16) | obj.NameAndTypeIndex;
        }

        public bool Equals(MemberRefInfo x, MemberRefInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.ClassIndex == y.ClassIndex && x.NameAndTypeIndex == y.NameAndTypeIndex;
        }

        public int GetHashCode(MemberRefInfo obj)
        {
            return (obj.ClassIndex << 16) | obj.NameAndTypeIndex;
        }
        
        public bool Equals(MethodHandleInfo x, MethodHandleInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.ReferenceKind == y.ReferenceKind && x.ReferenceIndex == y.ReferenceIndex;
        }

        public int GetHashCode(MethodHandleInfo obj)
        {
            return ((byte) obj.ReferenceKind << 16) | obj.ReferenceIndex;
        }

        public bool Equals(MethodTypeInfo x, MethodTypeInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.DescriptorIndex == y.DescriptorIndex;
        }

        public int GetHashCode(MethodTypeInfo obj)
        {
            return obj.DescriptorIndex;
        }

        public bool Equals(NameAndTypeInfo x, NameAndTypeInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.NameIndex == y.NameIndex && x.DescriptorIndex == y.DescriptorIndex;
        }

        public int GetHashCode(NameAndTypeInfo obj)
        {
            return (obj.NameIndex << 16) | obj.DescriptorIndex;
        }
        
        public bool Equals(PrimitiveInfo x, PrimitiveInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.Value == y.Value;
        }

        public int GetHashCode(PrimitiveInfo obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(StringInfo x, StringInfo y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.StringIndex == y.StringIndex;
        }

        public int GetHashCode(StringInfo obj)
        {
            return obj.StringIndex;
        }

        public bool Equals(Utf8Info x, Utf8Info y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.Value == y.Value;
        }

        public int GetHashCode(Utf8Info obj)
        {
            return obj.Value != null ? obj.Value.GetHashCode() : 0;
        }

    }
}