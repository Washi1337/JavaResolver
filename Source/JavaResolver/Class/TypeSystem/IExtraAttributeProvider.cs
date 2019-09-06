using System.Collections.Generic;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.TypeSystem
{
    public interface IExtraAttributeProvider
    {
        IDictionary<string, AttributeInfo> ExtraAttributes
        {
            get;
        }
    }
}