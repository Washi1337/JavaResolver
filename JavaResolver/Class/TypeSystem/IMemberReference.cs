using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    public interface IMemberReference
    {
        string Name
        {
            get;
            set;
        }

        ClassReference DeclaringClass
        {
            get;
        }

        string FullName
        {
            get;
        }
        
        IMemberDescriptor Descriptor
        {
            get;
        }
    }
}