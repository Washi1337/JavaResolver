using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Emit
{
    public class JavaClassFileBuilder
    {
        public ConstantPoolBuffer ConstantPoolBuffer
        {
            get;
        } = new ConstantPoolBuffer();
        
        public JavaClassFile CreateClassFile(JavaClassImage image)
        {
            var context = new BuildingContext(this);
            
            // Basic structure.
            var file = new JavaClassFile
            {
                MajorVersion = image.ClassFile.MajorVersion,
                MinorVersion = image.ClassFile.MinorVersion,
            };

            var thisReference = new ClassReference(image.RootClass.Name);
            file.ThisClass = (ushort) ConstantPoolBuffer.GetClassIndex(thisReference);

            if (image.RootClass.SuperClass != null)
                file.SuperClass = (ushort) ConstantPoolBuffer.GetClassIndex(image.RootClass.SuperClass);

            file.AccessFlags = image.RootClass.AccessFlags;

            // Fields
            foreach (var field in image.RootClass.Fields)
                file.Fields.Add(CreateFieldInfo(context, field));
            
            // Methods
            foreach (var method in image.RootClass.Methods)
                file.Methods.Add(CreateMethodInfo(context, method));
            
            AddAttributes(context, file, image.RootClass);

            file.ConstantPool = ConstantPoolBuffer.CreateConstantPool();
            return file;
        }

        private FieldInfo CreateFieldInfo(BuildingContext context,FieldDefinition definition)
        {
            // Basic structure.
            var info = new FieldInfo
            {
                AccessFlags = definition.AccessFlags,
                NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(definition.Name),
                DescriptorIndex = (ushort) ConstantPoolBuffer.GetDescriptorIndex(definition.Descriptor),
            };

            // Constant
            if (definition.Constant != null)
            {
                info.Attributes.Add(CreateAttribute(context,
                    new ConstantValueAttribute((ushort) ConstantPoolBuffer.GetLiteralIndex(definition.Constant))));
            }

            AddAttributes(context, info, definition);

            return info;
        }
        
        private MethodInfo CreateMethodInfo(BuildingContext context, MethodDefinition definition)
        {
            // Basic structure.
            var info = new MethodInfo
            {
                AccessFlags = definition.AccessFlags,
                NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(definition.Name),
                DescriptorIndex = (ushort) ConstantPoolBuffer.GetDescriptorIndex(definition.Descriptor),
            };

            // Body.
            if (definition.Body != null)
                info.Attributes.Add(CreateAttribute(context, definition.Body.Serialize(context)));

            AddAttributes(context, info, definition);
            return info;
        }

        public void AddAttributes(BuildingContext context, IAttributeProvider destination, IExtraAttributeProvider provider)
        {
            foreach (var attribute in provider.ExtraAttributes)
            {
                attribute.Value.NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(attribute.Key);
                destination.Attributes.Add(attribute.Value);
            }
        }

        private AttributeInfo CreateAttribute(BuildingContext context, IAttributeContents contents)
        {
            return CreateAttribute(contents.Name, contents.Serialize(context));
        }
        
        private AttributeInfo CreateAttribute(string name, byte[] contents)
        {
            return new AttributeInfo
            {
                NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(name),
                Contents = contents
            };
        }
    }
}