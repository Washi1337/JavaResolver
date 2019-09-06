using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides members for describing a reference to a member.
    /// </summary>
    public interface IMemberReference : INameProvider
    {
        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        new string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the enclosing class that defines the member.
        /// </summary>
        ClassReference DeclaringClass
        {
            get;
        }
        
        /// <summary>
        /// Gets the descriptor of the member, which includes the member type and possibly additional parameters.
        /// </summary>
        IMemberDescriptor Descriptor
        {
            get;
        }
    }

    public interface IField : IMemberReference
    {
        new FieldDescriptor Descriptor
        {
            get;
        }
    }

    public interface IMethod : IMemberReference
    {
        new MethodDescriptor Descriptor
        {
            get;
        }
    }
}