using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JavaResolver.Class.Descriptors
{
    internal class DescriptorParser
    {
        private readonly TextReader _reader;

        public DescriptorParser(string rawDescriptor)
            : this(new StringReader(rawDescriptor))
        {
        }
        
        public DescriptorParser(TextReader reader)
        {
            _reader = reader;
        }

        public FieldDescriptor ReadFieldDescriptor()
        {
            return new FieldDescriptor(ReadFieldType());   
        }

        public MethodDescriptor ReadMethodDescriptor()
        {
            char nextChar = ReadNextCharacter();
            if (nextChar != '(')
                throw new FormatException("Expected '('.");

            var parameterTypes = new List<FieldType>();
            while (PeekNextCharacter() != ')')
                parameterTypes.Add(ReadFieldType());

            ReadNextCharacter();
            
            return new MethodDescriptor(ReadFieldType(), parameterTypes);
        }

        private FieldType ReadFieldType()
        {
            char nextChar = ReadNextCharacter();

            switch (nextChar)
            {
                case 'B':
                case 'C':
                case 'D':
                case 'F':
                case 'I':
                case 'J':
                case 'S':
                case 'Z':
                case 'V':
                    return new BaseType((BaseTypeValue) nextChar);
                
                case 'L':
                    return ReadObjectType();
                
                case '[':
                    return ReadArrayType();
                
                default:
                    throw new FormatException("Unrecognized character " + nextChar + ".");
            }
        }

        private ObjectType ReadObjectType()
        {
            var builder = new StringBuilder();
            while (true)
            {
                char nextChar = ReadNextCharacter();
                if (nextChar == ';')
                    break;
                builder.Append(nextChar);
            }
            return new ObjectType(builder.ToString());
        }

        private ArrayType ReadArrayType()
        {
            return new ArrayType(ReadFieldType());
        }

        private char ReadNextCharacter()
        {
            int nextChar = _reader.Read();
            if (nextChar == -1)
                throw new EndOfStreamException();
            return (char) nextChar;
        }

        private char PeekNextCharacter()
        {
            int nextChar = _reader.Peek();
            if (nextChar == -1)
                throw new EndOfStreamException();
            return (char) nextChar;   
        }
    }
}