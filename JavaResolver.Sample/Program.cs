using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JavaResolver.Class;
using JavaResolver.Class.Code;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Emit;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.Metadata.Attributes;
using JavaResolver.Class.TypeSystem;

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
            
            var image = new JavaClassImage(classFile);

            Disassemble(image, "main");
            Console.WriteLine();
            
            var builder = new JavaClassFileBuilder();
            var newClassFile = builder.CreateClassFile(image);

            string newPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.GetFileName(path));
            using (var fs = File.Create(newPath))
            {
                var writer = new BigEndianStreamWriter(fs);
                newClassFile.Write(new WritingContext(writer));
            }

            Disassemble(newClassFile, "main");
            Console.WriteLine();
            
            reader = new MemoryBigEndianReader(File.ReadAllBytes(newPath));
            newClassFile = JavaClassFile.FromReader(reader);
            
            var newImage = new JavaClassImage(newClassFile);
            Disassemble(newImage, "main");
            Console.WriteLine();
        }
        
        private static void Disassemble(JavaClassImage image, string methodName)
        {
            var method = image.RootClass.Methods.First(x => x.Name == methodName);
            foreach (var instr in method.Body.Instructions)
                PrintInstruction(instr);
        }

        private static void Disassemble(JavaClassFile file, string methodName)
        {
            var method = file.Methods.First(x => file.ConstantPool.ResolveString(x.NameIndex) == methodName);
            
            var attr = method.Attributes.First(x => file.ConstantPool.ResolveString(x.NameIndex) == "Code");
            var codeAttr = CodeAttribute.FromReader(new MemoryBigEndianReader(attr.Contents));
            var disassembler = new ByteCodeDisassembler(new MemoryBigEndianReader(codeAttr.Code));

            foreach (var instr in disassembler.ReadInstructions())
                PrintInstruction(instr);
        }

        private static void PrintInstruction(ByteCodeInstruction instruction)
        {
            Console.Write(instruction);
            Console.WriteLine(" (" + instruction.OpCode.StackBehaviourPop + ", " + instruction.OpCode.StackBehaviourPush
                              + ", " + instruction.OpCode.FlowControl + ")");
        }
    }
}