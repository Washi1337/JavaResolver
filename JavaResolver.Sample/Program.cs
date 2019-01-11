using System;
using System.IO;
using JavaResolver.Class;

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

            var reader = new MemoryBigEndianReader(File.ReadAllBytes(path));
            var classFile = JavaClassFile.FromReader(reader);
            
            // ...
        }
    }
}