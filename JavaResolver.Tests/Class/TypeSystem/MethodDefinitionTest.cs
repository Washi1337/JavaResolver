using System.Collections.Generic;
using System.Linq;
using JavaResolver.Class;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.TypeSystem
{
    public class MethodDefinitionTest
    {
        private static readonly DescriptorComparer Comparer = new DescriptorComparer();
        private MethodDefinition _constructor;

        public MethodDefinitionTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.SimpleModel));
            var image = new JavaClassImage(classFile);
            var classDef = image.RootClass;
            _constructor = classDef.Methods.FirstOrDefault(x => x.Name == "<init>");
        }

        private static MethodDefinition CreateDummyMethod()
        {
            var method = new MethodDefinition("SomeMethod", new MethodDescriptor(BaseType.Int));
            var classDef = new ClassDefinition("SomeClass", new ClassReference("java/lang/Object"));
            classDef.Methods.Add(method);
            var image = new JavaClassImage(classDef);
            return method;
        }

        private static MethodDefinition RebuildImageWithMethod(MethodDefinition method)
        {
            var newImage = Utils.RebuildClassImage(method.DeclaringClass.Image);
            return newImage.RootClass.Methods.First(x => x.Name == method.Name);
        }
        
        [Fact]
        public void NameTest()
        {
            Assert.NotNull(_constructor);
        }

        [Fact]
        public void PersistentName()
        {
            var method = CreateDummyMethod();
            var newMethod = RebuildImageWithMethod(method);
            Assert.Equal(method.Name, newMethod.Name);
        }
        
        [Fact]
        public void AccessFlagsTest()
        {
            Assert.Equal(MethodAccessFlags.Public, _constructor.AccessFlags);
        }

        [Fact]
        public void PersistentAccessFlags()
        {
            const MethodAccessFlags flags = MethodAccessFlags.Public | MethodAccessFlags.Static;
            
            var method = CreateDummyMethod();
            method.AccessFlags = flags;
            
            var newMethod = RebuildImageWithMethod(method);
            Assert.Equal(flags, newMethod.AccessFlags);
        }

        [Fact]
        public void DescriptorTest()
        {
            Assert.Equal(new MethodDescriptor(
                    new BaseType(BaseTypeValue.Void),
                    new ObjectType("java/lang/String"),
                    new BaseType(BaseTypeValue.Int), new ObjectType("SimpleModel")),
                _constructor.Descriptor, Comparer);
        }

        [Fact]
        public void PersistentDescriptor()
        {
            var descriptor = new MethodDescriptor(BaseType.Void, ObjectType.String.CreateArrayType());
            
            var method = CreateDummyMethod();
            method.Descriptor = descriptor;
            
            var newMethod = RebuildImageWithMethod(method);
            Assert.Equal(descriptor.ReturnType.FullName, newMethod.Descriptor.ReturnType.FullName);
            Assert.Single(newMethod.Descriptor.ParameterTypes);
            Assert.Equal(descriptor.ParameterTypes[0].FullName, newMethod.Descriptor.ParameterTypes[0].FullName);
        }
        
        [Fact]
        public void ExceptionsTest()
        {
            var classFile = JavaClassFile.FromReader(new MemoryBigEndianReader(Properties.Resources.Exceptions));
            var image = new JavaClassImage(classFile);
            var classDef = image.RootClass;
            var throwsMethod = classDef.Methods.First(x => x.Name == "Throws");

            Assert.Single(throwsMethod.Exceptions);
            Assert.Equal("CustomException", throwsMethod.Exceptions[0].Name);
        }

        [Fact]
        public void PersistentExceptions()
        {
            var exceptions = new List<ClassReference>
            {
                new ClassReference("java/io/EOFException"),
                new ClassReference("java/io/FileNotFoundException")
            };

            var method = CreateDummyMethod();
            foreach (var ex in exceptions)
                method.Exceptions.Add(ex);
            
            var newMethod = RebuildImageWithMethod(method);
            Assert.Equal(method.Exceptions.Select(x => x.Name), newMethod.Exceptions.Select(x => x.Name));
        }
    }
}