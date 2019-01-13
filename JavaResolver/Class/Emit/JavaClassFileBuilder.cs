using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Emit
{
    public class JavaClassFileBuilder
    {
        public ConstantPoolBuffer ConstantPoolBuffer
        {
            get;
        } = new ConstantPoolBuffer();

        public AttributeBuilder AttributeBuilder
        {
            get;
            set;
        } = new AttributeBuilder();
        
        public JavaClassFile CreateClassFile(JavaClassImage image)
        {
            var context = new BuildingContext(this);
            
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

            foreach (var field in image.RootClass.Fields)
                file.Fields.Add(CreateFieldInfo(context, field));
            
            foreach (var method in image.RootClass.Methods)
                file.Methods.Add(CreateMethodInfo(context, method));
            
            AddAttributes(context, file, image.RootClass);

            file.ConstantPool = ConstantPoolBuffer.CreateConstantPool();
            return file;
        }

        private FieldInfo CreateFieldInfo(BuildingContext context,FieldDefinition definition)
        {
            var info = new FieldInfo
            {
                AccessFlags = definition.AccessFlags,
                NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(definition.Name),
                DescriptorIndex = (ushort) ConstantPoolBuffer.GetDescriptorIndex(definition.Descriptor),
            };

            AddAttributes(context, info, definition);

            return info;
        }
        
        private MethodInfo CreateMethodInfo(BuildingContext context, MethodDefinition definition)
        {
            var info = new MethodInfo
            {
                AccessFlags = definition.AccessFlags,
                NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(definition.Name),
                DescriptorIndex = (ushort) ConstantPoolBuffer.GetDescriptorIndex(definition.Descriptor),
            };

            if (definition.Body != null)
                info.Attributes.Add(AttributeBuilder.CreateCodeAttribute(context, definition.Body));

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
    }
}