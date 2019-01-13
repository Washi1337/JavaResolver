using System.Collections.Generic;

namespace JavaResolver.Class.Metadata
{
    public interface IAttributeProvider
    {
        IList<AttributeInfo> Attributes
        {
            get;
        }
    }
}