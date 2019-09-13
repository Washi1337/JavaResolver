using System;
using System.IO;
using JavaResolver.Class;
using JavaResolver.Class.Emit;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Sample
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string path = args[0].Replace("\"", "");
            var classFile = JavaClassFile.FromFile(path);
            var image = DumpImage(classFile);

            var newClassFile = image.CreateClassFile();
            string outputPath = Path.Combine(Path.GetDirectoryName(path), "output", Path.GetFileName(path));
            newClassFile.Write(outputPath);
            
            newClassFile = JavaClassFile.FromFile(outputPath);
            DumpImage(newClassFile);

        }

        private static JavaClassImage DumpImage(JavaClassFile classFile)
        {
            var image = new JavaClassImage(classFile);
            var rootClass = image.RootClass;

            Console.WriteLine("Basic information:");
            DumpStructure(rootClass);

            Console.WriteLine("Disassembly of each method:");
            DumpByteCode(rootClass);
            return image;
        }

        private static void DumpStructure(ClassDefinition rootClass)
        {
            // Dump class info:
            Console.WriteLine("Class: " + rootClass.Name);
            Console.WriteLine("\t- Extends: " + rootClass.SuperClass.Name);

            // Dump fields:
            Console.WriteLine("\t- Fields:");
            foreach (var field in rootClass.Fields)
                Console.WriteLine($"\t\t- {field.Name} : {field.Descriptor.FieldType}");

            // Dump methods:
            Console.WriteLine("\t- Methods:");
            foreach (var method in rootClass.Methods)
                Console.WriteLine(
                    $"\t\t- {method.Name}({string.Join(", ", method.Descriptor.ParameterTypes)}) : {method.Descriptor.ReturnType}");
            
            Console.WriteLine();
        }

        private static void DumpByteCode(ClassDefinition rootClass)
        {
            foreach (var method in rootClass.Methods)
            {
                // Print full name:
                Console.WriteLine(
                    $"{method.Name}({string.Join(", ", method.Descriptor.ParameterTypes)}) : {method.Descriptor.ReturnType}");
                
                // Print each instruction:
                foreach (var instruction in method.Body.Instructions)
                    Console.WriteLine(instruction);
                
                Console.WriteLine();
            }
        }
    }
}