using System.Collections.Generic;
using JavaResolver.Class.Constants;
using JavaResolver.Class.Descriptors;

namespace JavaResolver.Class.TypeSystem
{
    /// <summary>
    /// Provides a high-level representation of a Java class file, exposing a hierarchical view on the metadata that
    /// resembles the structure of reflection based APIs.
    /// </summary>
    public class JavaClassImage
    {
        private readonly IDictionary<int, ClassReference> _classReferences = new Dictionary<int, ClassReference>();
        private readonly IDictionary<int, FieldReference> _fieldReferences = new Dictionary<int, FieldReference>();
        private readonly IDictionary<int, FieldDescriptor> _fieldDescriptors = new Dictionary<int, FieldDescriptor>();
        private readonly IDictionary<int, MethodReference> _methodReferences = new Dictionary<int, MethodReference>();
        private readonly IDictionary<int, MethodDescriptor> _methodDescriptors = new Dictionary<int, MethodDescriptor>();
        
        public JavaClassImage(JavaClassFile classFile)
        {
            ClassFile = classFile;
            RootClass = new ClassDefinition(this);
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
            get;
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
    }
}