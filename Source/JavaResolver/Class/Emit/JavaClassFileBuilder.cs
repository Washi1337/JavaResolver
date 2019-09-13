using System.Collections.Generic;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Emit
{
    public class JavaClassFileBuilder
    {
        private readonly IDictionary<BootstrapMethodInfo, int> _bootstrapInfos = new Dictionary<BootstrapMethodInfo, int>();
        
        public ConstantPoolBuffer ConstantPoolBuffer
        {
            get;
        } = new ConstantPoolBuffer();
        

        public BootstrapMethodsAttribute BootstrapMethodsAttribute
        {
            get;
        } = new BootstrapMethodsAttribute();
        
        public JavaClassFile CreateClassFile(JavaClassImage image)
        {
            var context = new BuildingContext(this);
            
            // Basic structure.
            var file = new JavaClassFile
            {
                MajorVersion = image.MajorVersion,
                MinorVersion = image.MinorVersion,
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
            
            // Source file
            if (image.SourceFile != null)
            {
                file.Attributes.Add(CreateAttribute(context, new SingleIndexAttribute(
                    SingleIndexAttribute.SourceFileAttribute,
                    (ushort) ConstantPoolBuffer.GetUtf8Index(image.SourceFile))));
            }

            if (BootstrapMethodsAttribute.BootstrapMethods.Count > 0)
                file.Attributes.Add(CreateAttribute(context, BootstrapMethodsAttribute));
            
            AddAttributes(context, file, image);

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
                info.Attributes.Add(CreateAttribute(context, new SingleIndexAttribute(
                    SingleIndexAttribute.ConstantValueAttribute,
                    (ushort) ConstantPoolBuffer.GetLiteralIndex(definition.Constant))));
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

            // Exceptions
            if (definition.Exceptions.Count > 0)
            {
                var attribute = new ExceptionsAttribute();
                foreach (var exception in definition.Exceptions)
                    attribute.Exceptions.Add((ushort) context.Builder.ConstantPoolBuffer.GetClassIndex(exception));
                info.Attributes.Add(CreateAttribute(context, attribute));
            }
            
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

        public AttributeInfo CreateAttribute(BuildingContext context, IAttributeContents contents)
        {
            return CreateAttribute(contents.Name, contents.Serialize(context));
        }
        
        public AttributeInfo CreateAttribute(string name, byte[] contents)
        {
            return new AttributeInfo
            {
                NameIndex = (ushort) ConstantPoolBuffer.GetUtf8Index(name),
                Contents = contents
            };
        }

        public int GetBootstrapMethodIndex(BootstrapMethod bootstrapMethod)
        {
            var info = new BootstrapMethodInfo
            {
                MethodRefIndex = (ushort) ConstantPoolBuffer.GetMethodHandleIndex(bootstrapMethod.Handle),
            };

            foreach (var arg in bootstrapMethod.Arguments) 
                info.Arguments.Add((ushort) ConstantPoolBuffer.GetStaticConstantIndex(arg));

            if (!_bootstrapInfos.TryGetValue(info, out int index))
            {
                var methods = BootstrapMethodsAttribute.BootstrapMethods;
                index = methods.Count;
                methods.Add(info);
                _bootstrapInfos.Add(info, index);
            }

            return index;
        }

    }
}