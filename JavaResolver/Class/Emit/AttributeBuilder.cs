using System.IO;
using JavaResolver.Class.Code;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class.Emit
{
    public class AttributeBuilder
    {
        public virtual AttributeInfo CreateCodeAttribute(BuildingContext context, MethodBody body)
        {
            var code = body.Serialize(context);

            using (var stream = new MemoryStream())
            {
                var writer = new BigEndianStreamWriter(stream);
                code.Write(new WritingContext(writer));

                return new AttributeInfo
                {
                    NameIndex = (ushort) context.Builder.ConstantPoolBuffer.GetUtf8Index("Code"),
                    Contents = stream.ToArray()
                };
            }
        }
    }
}