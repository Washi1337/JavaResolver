using System.Collections.Generic;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Describes a member that might have extra custom attributes associated to it that are not interpreted or supported
    /// by JavaResolver.
    /// </summary>
    public interface IExtraAttributeProvider
    {
        /// <summary>
        /// Gets a collection of additional attributes that were not interpreted by JavaResolver itself.
        /// </summary>
        IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        }
    }
}