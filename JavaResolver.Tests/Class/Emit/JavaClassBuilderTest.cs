using System;
using JavaResolver.Class.Code;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests.Class.Emit
{
    public class JavaClassBuilderTest : IClassFixture<TemporaryDirectoryFixture>
    {
        private readonly TemporaryDirectoryFixture _fixture;

        public JavaClassBuilderTest(TemporaryDirectoryFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void BuildSimpleHelloWorld()
        {
            // Create main method.
            var mainMethod = new MethodDefinition("main",
                new MethodDescriptor(BaseType.Void, ObjectType.String.CreateArrayType()))
            {
                AccessFlags = MethodAccessFlags.Public | MethodAccessFlags.Static,
            };

            // Build references.
            var printStream = new ClassReference("java/io/PrintStream");
            
            var systemOut = new FieldReference("out", new ClassReference("java/lang/System"),
                new FieldDescriptor(new ObjectType("java/io/PrintStream")));

            var println = new MethodReference("println", printStream,
                new MethodDescriptor(BaseType.Void,ObjectType.String));

            // Build body.
            mainMethod.Body = new ByteCodeMethodBody
            {
                Variables = 
                {
                    new LocalVariable("args", new FieldDescriptor(mainMethod.Descriptor.ParameterTypes[0]))
                },
                Instructions =
                {
                    new ByteCodeInstruction(ByteOpCodes.GetStatic, systemOut),
                    new ByteCodeInstruction(ByteOpCodes.Ldc, "Hello, world!"),
                    new ByteCodeInstruction(ByteOpCodes.InvokeVirtual, println),
                    new ByteCodeInstruction(ByteOpCodes.Return)
                }
            };
            mainMethod.Body.Variables[0].Start = mainMethod.Body.Instructions[0];
            
            // Create container class.
            var classDef = new ClassDefinition("HelloWorld")
            {
                Methods = {mainMethod},
                SuperClass = new ClassReference("java/lang/Object"),
            };
            var classImage = new JavaClassImage(classDef);

            // Verify.
            _fixture.BuildAndVerifyOutput(classImage, "Hello, world!" + Environment.NewLine);
        }
    }
}