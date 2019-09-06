using System;
using System.Collections.Generic;
using System.IO;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;
using JavaResolver.Class.Emit;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a Java class file, exposing a hierarchical view on the metadata that
    /// resembles the structure of reflection based APIs.
    /// </summary>
    public class JavaClassImage
    {
        /// <summary>
        /// Parses a class file located on tbhe disk, and opens a high-level representation of the class file.
        /// </summary>
        /// <param name="file">The path to the file to parse.</param>
        /// <returns>The high-level Java class image.</returns>
        /// <exception cref="BadImageFormatException">Occurs when the file does not provide a valid signature of
        /// a class file.</exception>
        public static JavaClassImage FromFile(string file)
        {
            return new JavaClassImage(JavaClassFile.FromFile(file));
        }
        
        /// <summary>
        /// Parses a class file from a binary reader and opens a high-level representation of the class file.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        /// <returns>The high-level Java class image.</returns>
        /// <exception cref="BadImageFormatException">Occurs when the file does not provide a valid signature of
        /// a class file.</exception>
        public static JavaClassImage FromReader(IBigEndianReader reader)
        {
            return new JavaClassImage(JavaClassFile.FromReader(reader));
        }
        
        private readonly IDictionary<int, ClassReference> _classReferences = new Dictionary<int, ClassReference>();
        private readonly IDictionary<int, FieldReference> _fieldReferences = new Dictionary<int, FieldReference>();
        private readonly IDictionary<int, FieldDescriptor> _fieldDescriptors = new Dictionary<int, FieldDescriptor>();
        private readonly IDictionary<int, MethodReference> _methodReferences = new Dictionary<int, MethodReference>();
        private readonly IDictionary<int, MethodDescriptor> _methodDescriptors = new Dictionary<int, MethodDescriptor>();
        private ClassDefinition _rootClass;

        public JavaClassImage(ClassDefinition rootClass)
        {
            RootClass = rootClass ?? throw new ArgumentNullException(nameof(rootClass));
        }
        
        public JavaClassImage(JavaClassFile classFile)
        {
            ClassFile = classFile;
            RootClass = new ClassDefinition(this);
            MajorVersion = classFile.MajorVersion;
            MinorVersion = classFile.MinorVersion;
        }

        public ushort MajorVersion
        {
            get;
            set;
        } = 0x34;

        public ushort MinorVersion
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets the class file the image was based on.
        /// </summary>
        public JavaClassFile ClassFile
        {
            get;
        }

        /// <summary>
        /// Gets the first and top-most class definition that is defined in the java class file.  
        /// </summary>
        public ClassDefinition RootClass
        {
            get => _rootClass;
            internal set
            {
                if (_rootClass != null)
                    _rootClass.Image = null;
                _rootClass = value;
                if (_rootClass != null)
                    _rootClass.Image = this;
            }
        }

        public ClassReference ResolveClass(int index)
        {
            if (!_classReferences.TryGetValue(index, out var classReference))
            {
                var constantInfo = ClassFile.ConstantPool.ResolveConstant(index);
                if (constantInfo is ClassInfo classInfo)
                {
                    classReference = new ClassReference(this, classInfo);
                    _classReferences.Add(index, classReference);
                }
            }

            return classReference;
        }

        public FieldReference ResolveField(int index)
        {
            if (!_fieldReferences.TryGetValue(index, out var reference))
            {
                var constantInfo = ClassFile.ConstantPool.ResolveConstant(index);
                if (constantInfo is FieldRefInfo fieldRefInfo)
                {
                    reference = new FieldReference(this, fieldRefInfo); 
                    _fieldReferences.Add(index, reference);
                }
            }

            return reference;
        }

        public FieldDescriptor ResolveFieldDescriptor(int index)
        {
            if (!_fieldDescriptors.TryGetValue(index, out var descriptor))
            {
                var constantInfo = ClassFile.ConstantPool.ResolveConstant(index);
                if (constantInfo is Utf8Info utf8Info)
                {
                    descriptor = FieldDescriptor.FromString(utf8Info.Value); 
                    _fieldDescriptors.Add(index, descriptor);
                }
            }

            return descriptor;
        }
        
        public MethodReference ResolveMethod(int index)
        {
            if (!_methodReferences.TryGetValue(index, out var reference))
            {
                var constantInfo = ClassFile.ConstantPool.ResolveConstant(index);
                if (constantInfo is MethodRefInfo methodRefInfo)
                {
                    reference = new MethodReference(this, methodRefInfo); 
                    _methodReferences.Add(index, reference);
                }
            }

            return reference;
        }
        
        public MethodDescriptor ResolveMethodDescriptor(int index)
        {
            if (!_methodDescriptors.TryGetValue(index, out var descriptor))
            {
                var constantInfo = ClassFile.ConstantPool.ResolveConstant(index);
                if (constantInfo is Utf8Info utf8Info)
                {
                    descriptor = MethodDescriptor.FromString(utf8Info.Value); 
                    _methodDescriptors.Add(index, descriptor);
                }
            }

            return descriptor;
        }

        public JavaClassFile CreateClassFile()
        {
            return new JavaClassFileBuilder().CreateClassFile(this);
        }
        
        
    }
}