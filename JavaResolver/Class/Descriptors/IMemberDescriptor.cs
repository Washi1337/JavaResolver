namespace JavaResolver.Class.Descriptors
{
    public interface IMemberDescriptor
    {
        FieldType MemberType
        {
            get;
        }

        string Serialize();
    }
}