using System;
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

        private FieldType ReadFieldType()
        {
            char nextChar = (char) _reader.Read();

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
                int nextChar = _reader.Read();
                if (nextChar == -1)
                    throw new EndOfStreamException();
                if (nextChar == ';')
                    break;
                builder.Append((char) nextChar);
            }
            return new ObjectType(builder.ToString());
        }

        private ArrayType ReadArrayType()
        {
            return new ArrayType(ReadFieldType());
        }
    }
}