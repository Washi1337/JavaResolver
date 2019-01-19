JavaResolver
============

JavaResolver is a Java class file inspection library allowing .NET programmers to read, modify and write Java class files. The library allows for low level access of the `.class` file format (e.g. direct access to the constants pool and raw member and attribute structures), as well as a higher level representation that provides a more hierarchical view on the metadata.

JavaResolver is released under the MIT license.

Features
========
- Create, read and edit any `.class` file using the `JavaClassFile` class.
- Inspect and edit the constant pool.
- Add, inspect, edit and remove members such as methods, fields and attributes.
- Disassemble and assemble bytecode of methods (or arbitrary byte arrays).

Quick starters guide
====================

Creating and reading class files
--------------------------------
The `JavaClassFile` represents the basic raw structure of a class file. You can open one using for example:
```csharp
var classFile = JavaClassFile.FromFile(@"C:\path\to\your\file.class");
```

Creating new class files can be done through the constructors
```csharp
var classFile = new JavaClassFile();
```

The `JavaClassFile` is a __low level representations__ of the class file. If you want a more higher level representation for easier access, you have to open a new `JavaClassImage` from the `JavaClassFile`:

(Note: the following snippet is subject to change)
```csharp
var classImage = new JavaClassImage(classFile);
```

Creating new class images can also be done directly, by simply calling the other constructor:
```csharp
var classImage = new JavaClassImage(new ClassDefinition("MyClass"))
{
    SuperClass = new ClassReference("java/lang/Object"),
};
```

Fields and methods
----------------------
Fields and methods can be obtained through the representative properties of `JavaClassImage`:
```csharp
foreach (var field in classImage.Fields)
    Console.WriteLine(field.Name);

foreach (var method in classImage.Methods)
    Console.WriteLine(method.Name);
```

Fields and methods are represented using the `FieldDefinition` and `MethodDefinition` classes, and can be created using their constructors.
```csharp
var field = new FieldDefinition("myIntField", new FieldDescriptor(BaseType.Int));
var method = new MethodDefinition("myMethod", new MethodDescriptor(BaseType.Void));
```

A more low level approach, where we iterate over raw method, field and attribute structures can be done through the representative properties of the `JavaClassFile` class:
```csharp
foreach (var methodInfo in classFile.Methods) 
{
    string methodName = classFile.ConstantPool.ResolveString(methodInfo.NameIndex);
    Console.WriteLine(methodName);
    // ...
}
```

Inspecting method bodies
------------------------
In high level mode, simply access the `Body` property of a `MethodDefinition`. It contains __mutable__ collections for instructions, local variables, exception handlers and more:

```csharp
var method = classImage.Methods.First(m => m.Name == "main");
foreach (var instruction in method.Body.Instructions)
    Console.WriteLine(instruction);
```

You can also opt for a more low level approach. Java stores the method body as an attribute in the raw method info structure with the name `"Code"`. You can find it yourself using:
```csharp
var method = classFile.Methods.First(m => ...);

// Look up attribute:
var codeAttribute = method.Attributes.First(a => classFile.ConstantPool.ResolveString(a.NameIndex) == CodeAttribute.AttributeName);

// Deserialize contents:
var contents = CodeAttribute.FromReader(new MemoryBigEndianReader(codeAttribute.Contents));

// Disassemble bytecode:
var disassembler = new ByteCodeDisassembler(new MemoryBigEndianReader(contents.Code));
foreach (var instruction in disassembler.ReadInstructions())
    Console.WriteLine(instruction);
```

To write instructions, use the `ByteCodeAssembler` instead to get a `byte[]` of the new code.

Inspecting the raw constants pool:
------------------------------
Iterating over each constant defined in the pool can be done using:
```csharp
var constantPool = classFile.ConstantPool

foreach (var constant in constantPool.Constants) 
{
    // ...
}
```
Resolving constant indices can be done through
```csharp
var resolvedConstant = constantPool.ResolveConstant(index);
```

Since constants are often UTF8 string constants, there is a shortcut for it to make life a little bit easier:
```csharp
string myString = constantPool.ResolveString(index);
```

