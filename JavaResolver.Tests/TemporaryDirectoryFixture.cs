using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using JavaResolver.Class;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Emit;
using JavaResolver.Class.TypeSystem;
using Xunit;

namespace JavaResolver.Tests
{
    /// <summary>
    /// Represents a fixture that operates on a temporary directory to put in obfuscated applications.
    /// This class can also be used to verify the integrity of the output files.
    /// </summary>
    public class TemporaryDirectoryFixture : IDisposable
    {
        public TemporaryDirectoryFixture()
        {
            // Define and create temp directory.
            var now = DateTime.Now;
            string directoryName = $"Fixture_{now.Year}{now.Month:00}{now.Day:00}{now.Hour:00}{now.Minute:00}{now.Second:00}{now.Millisecond:000}";
            OutputDirectory = Path.Combine(
                Path.GetDirectoryName(typeof(TemporaryDirectoryFixture).Assembly.Location),
                "temp", directoryName);

            if (!Directory.Exists(OutputDirectory))
                Directory.CreateDirectory(OutputDirectory);
        }

        /// <summary>
        /// Gets the temporary output directory the fixture is operating on.
        /// </summary>
        public string OutputDirectory
        {
            get;
        }

        /// <summary>
        /// Builds the class image and verifies the output of the file.
        /// </summary>
        /// <param name="image">The class to compile.</param>
        /// <param name="expectedOutput">The expected output of the file.</param>
        /// <param name="regex">Specifies if <paramref name="expectedOutput"/> should be interpreted as a regular expression pattern.</param>
        public void BuildAndVerifyOutput(JavaClassImage image, string expectedOutput, bool regex = false)
        {
            var builder = new JavaClassFileBuilder();
            var file = builder.CreateClassFile(image);
            WriteAndVerifyOutput(file, expectedOutput, regex);
        }   
        
        /// <summary>
        /// Builds the class image and verifies the output of the file.
        /// </summary>
        /// <param name="image">The class to compile.</param>
        /// <param name="expectedOutput">The expected output of the file.</param>
        /// <param name="regex">Specifies if <paramref name="expectedOutput"/> should be interpreted as a regular expression pattern.</param>
        public void WriteAndVerifyOutput(JavaClassFile classFile, string expectedOutput, bool regex = false)
        {
            var classInfo = (ClassInfo) classFile.ConstantPool.ResolveConstant(classFile.ThisClass);
            string name = classFile.ConstantPool.ResolveString(classInfo.NameIndex);

            using (var fs = File.Create(Path.Combine(OutputDirectory, name + ".class")))
            {
                var writer = new BigEndianStreamWriter(fs);
                var context = new WritingContext(writer);
                classFile.Write(context);
            }

            VerifyOutput(name, expectedOutput, regex);
        }   
        
        /// <summary>
        /// Verifies the output of a Java class file.
        /// </summary>
        /// <param name="className">The path to the file to run.</param>
        /// <param name="expectedOutput">The expected output of the file.</param>
        /// <param name="regex">Specifies if <paramref name="expectedOutput"/> should be interpreted as a regular expression pattern.</param>
        public void VerifyOutput(string className, string expectedOutput, bool regex = false)
        {
            var process = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    ErrorDialog = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    FileName = "java",
                    WorkingDirectory = OutputDirectory,
                    Arguments = className
                }
            };
            
            process.Start();
            
            if (process.WaitForExit(5000))
            {
                try
                {
                    process.Kill();
                    Thread.Sleep(1000);
                }
                catch (InvalidOperationException)
                {
                    // Process closed by itself.
                }
            }
            
            string output = process.StandardOutput.ReadToEnd();

            if (regex)
                Assert.True(Regex.IsMatch(expectedOutput, output));
            else
                Assert.Equal(expectedOutput, output);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            const int maximumAttempts = 5;
            
            // Try to delete directory.
            for (int i = 0; i < maximumAttempts && Directory.Exists(OutputDirectory); i++)
            {
                try
                {
                    Directory.Delete(OutputDirectory, true);
                }
                catch (AccessViolationException)
                {
                    // Deleting can fail because a process has not exited yet in windows.
                    // Wait for a bit and try again.
                    Thread.Sleep(1000);
                }
            }
            
            // If directory is still not removed after the maximum amount of attempts, notify tester. 
            if (Directory.Exists(OutputDirectory))
            {
                throw new IOException(
                    $"Failed to delete temporary directory. Exceeded maximum amount of {maximumAttempts} attempts.");
            }
        }
        
    }
}