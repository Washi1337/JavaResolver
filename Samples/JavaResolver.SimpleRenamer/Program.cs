using System;
using System.IO;
using JavaResolver.Class.Metadata;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.SimpleRenamer
{
    internal class Program
    {
        private static readonly Random Random = new Random();
        
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: JavaResolver.SimpleRenamer inputfile.class");
                return;
            }

            // Open file.
            string filePath = args[0].Replace("\"", "");
            var classImage = JavaClassImage.FromFile(filePath);
            
            // Rename symbols.
            RenameInClass(classImage.RootClass);

            // Reassemble.
            var newClassFile = classImage.CreateClassFile();
            
            // Save.
            newClassFile.Write(Path.ChangeExtension(filePath, "obfuscated.class"));
        }

        public static string GenerateRandomName(int length)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var name = new char[length];
            for (int i = 0; i < length; i++)
                name[i] = alphabet[Random.Next(alphabet.Length)];
            return new string(name);
        }

        public static void RenameInClass(ClassDefinition classDef)
        {
            // Rename fields.
            foreach (var field in classDef.Fields)
            {
                if (!field.AccessFlags.HasFlag(FieldAccessFlags.Public)
                    && !field.AccessFlags.HasFlag(FieldAccessFlags.Protected))
                {
                    field.Name = GenerateRandomName(10);
                }
            }    
            
            // Rename methods.
            foreach (var method in classDef.Methods)
            {
                if (!method.AccessFlags.HasFlag(MethodAccessFlags.Public)
                    && !method.AccessFlags.HasFlag(FieldAccessFlags.Protected))
                {
                    method.Name = GenerateRandomName(10);
                }
            }    
        }
        
        
    }
}