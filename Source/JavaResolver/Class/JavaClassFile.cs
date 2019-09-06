using System;
using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Metadata;

namespace JavaResolver.Class
{
    /// <summary>
    /// Represents a file containing a class targeting the Java runtime. 
    /// </summary>
    public class JavaClassFile : FileSegment, IAttributeProvider
    {
        /// <summary>
        /// The signature every class file starts with.
        /// </summary>
        public const uint Magic = 0xCAFEBABE;

        public static JavaClassFile FromFile(string path)
        {
            var reader = new MemoryBigEndianReader(File.ReadAllBytes(path));
            return FromReader(reader);
        }
        
        /// <summary>
        /// Parses a class file from a binary reader.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <returns>The parsed class file.</returns>
        /// <exception cref="BadImageFormatException">Occurs when the file does not provide a valid signature of
        /// a class file.</exception>
        public static JavaClassFile FromReader(IBigEndianReader reader)
        {
            uint magic = reader.ReadUInt32();
            if (magic != Magic)
                throw new BadImageFormatException("Image does not contain a valid class file.");

            var file = new JavaClassFile
            {
                MinorVersion = reader.ReadUInt16(),
                MajorVersion = reader.ReadUInt16(),
                ConstantPool = ConstantPool.FromReader(reader),
                AccessFlags = (ClassAccessFlags) reader.ReadUInt16(),
                ThisClass = reader.ReadUInt16(),
                SuperClass = reader.ReadUInt16(),
            };

            ushort interfaceCount = reader.ReadUInt16();
            for (int i = 0; i < interfaceCount; i++) 
                file.Interfaces.Add(reader.ReadUInt16());

            ushort fieldCount = reader.ReadUInt16();
            for (int i = 0; i < fieldCount; i++)
                file.Fields.Add(FieldInfo.FromReader(reader));

            ushort methodCount = reader.ReadUInt16();
            for (int i = 0; i < methodCount; i++)
                file.Methods.Add(MethodInfo.FromReader(reader));

            ushort attributeCount = reader.ReadUInt16();
            for (int i = 0; i < attributeCount; i++)
                file.Attributes.Add(AttributeInfo.FromReader(reader));

            return file;
        }

        /// <summary>
        /// Gets or sets the minor version the class file format is build for.
        /// </summary>
        public ushort MinorVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the major version the class file format is build for.
        /// </summary>
        public ushort MajorVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the constant pool of the class file, containing all the literals
        /// and references to external members.
        /// </summary>
        public ConstantPool ConstantPool
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the accessibility attributes for the class stored in the file. 
        /// </summary>
        public ClassAccessFlags AccessFlags
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an index into the constant pool that references a <see cref="ClassInfo"/>
        /// representing the class defined in the file. 
        /// </summary>
        public ushort ThisClass
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an index into the constant pool that references a <see cref="ClassInfo"/>
        /// representing the direct super class of the class defined in the file.
        /// </summary>
        public ushort SuperClass
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of indices into the constant pool referencing interfaces that the class implements.
        /// </summary>
        public IList<ushort> Interfaces
        {
            get;
        } = new List<ushort>();
        

        /// <summary>
        /// Gets a collection of fields defined by the class.
        /// </summary>
        public IList<FieldInfo> Fields
        {
            get;
        } = new List<FieldInfo>();
        
        /// <summary>
        /// Gets a collection of methods defined by the class.
        /// </summary>
        public IList<MethodInfo> Methods
        {
            get;
        } = new List<MethodInfo>();
        
        /// <summary>
        /// Gets a collection of attributes defined by the class.
        /// </summary>
        public IList<AttributeInfo> Attributes
        {
            get;
        } = new List<AttributeInfo>();

        /// <summary>
        /// Saves the Java class file to the disk. 
        /// </summary>
        /// <param name="path">The path of the new file to create.</param>
        public void Write(string path)
        {
            using (var fs = File.Create(path))
            {
                Write(fs);
            }    
        }

        /// <summary>
        /// Saves the Java class file to an output stream. 
        /// </summary>
        /// <param name="outputStream">The stream to write to.</param>
        public void Write(Stream outputStream)
        {
            Write(new BigEndianStreamWriter(outputStream));
        }

        /// <summary>
        /// Writes the Java class file to a specified binary writer. 
        /// </summary>
        /// <param name="writer">The writer to use.</param>
        public void Write(IBigEndianWriter writer)
        {
            Write(new WritingContext(writer));
        }
        
        public override void Write(WritingContext context)
        {
            var writer = context.Writer;
            writer.Write(Magic);
            writer.Write(MinorVersion);
            writer.Write(MajorVersion);
            ConstantPool.Write(context);
            writer.Write((ushort) AccessFlags);
            writer.Write(ThisClass);
            writer.Write(SuperClass);

            writer.Write((ushort) Interfaces.Count);
            foreach (var @interface in Interfaces)
                writer.Write(@interface);

            writer.Write((ushort) Fields.Count);
            foreach (var field in Fields)
                field.Write(context);

            writer.Write((ushort) Methods.Count);
            foreach (var method in Methods)
                method.Write(context);

            writer.Write((ushort) Attributes.Count);
            foreach (var attribute in Attributes)
                attribute.Write(context);
        }
        
    }
}