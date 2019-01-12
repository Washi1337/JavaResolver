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
            var info = classFile.Methods[1].Attributes.First(x =>
                ((Utf8Info) classFile.ConstantPool.Constants[x.NameIndex - 1]).Value == "Code");

            // Parse code attribute.
            reader = new MemoryBigEndianReader(info.Contents);
            var code = CodeAttribute.FromReader(reader);
            
            // Set up new disassembler.
            reader = new MemoryBigEndianReader(code.Code);
            var disassembler = new ByteCodeDisassembler(reader);
            disassembler.OperandResolver = new DefaultOperandResolver(classFile);
            
            // Disassemble!
            foreach (var instruction in disassembler.ReadInstructions())
                Console.WriteLine(instruction);
        }
    }
}