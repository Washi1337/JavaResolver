using System;
using System.IO;
using System.Linq;
using JavaResolver.Class;
using JavaResolver.Class.Code;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Sample
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path.");
                return;
            }

            string path = args[0].Replace("\"", "");
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found!");
                return;
            }

            // Read class file.
            var reader = new MemoryBigEndianReader(File.ReadAllBytes(path));
            var classFile = JavaClassFile.FromReader(reader);
            
            // Obtain main method's code.
            foreach (var method in classFile.Methods)
            {
                Console.WriteLine(classFile.ConstantPool.ResolveConstant(method.NameIndex));
                Console.WriteLine(classFile.ConstantPool.ResolveConstant(method.DescriptorIndex));

                // Parse code attribute.
                var codeInfo = method.Attributes.First(x =>
                    ((Utf8Info) classFile.ConstantPool.Constants[x.NameIndex - 1]).Value == "Code");
                reader = new MemoryBigEndianReader(codeInfo.Contents);
                var code = CodeAttribute.FromReader(reader);
            
                // Set up new disassembler.
                reader = new MemoryBigEndianReader(code.Code);
                var disassembler = new ByteCodeDisassembler(reader);
            
                // Disassemble!
                var instructions = disassembler.ReadInstructions();
                foreach (var instruction in instructions)
                    Console.WriteLine(instruction);

//                // Reassemble!
//                using (var stream = new MemoryStream())
//                {
//                    var codeWriter = new BigEndianStreamWriter(stream);
//                    var assembler = new ByteCodeAssembler(codeWriter);
//                    foreach (var instruction in instructions)
//                        assembler.Write(instruction);
//
//                    code.Code = stream.ToArray();
//                }
                
                Console.WriteLine();
            }

            using (var fs = File.Create(Path.ChangeExtension(path, "patched.class")))
            {
                var writer = new BigEndianStreamWriter(fs);
                classFile.Write(new WritingContext(writer));
            }
        }
    }
}