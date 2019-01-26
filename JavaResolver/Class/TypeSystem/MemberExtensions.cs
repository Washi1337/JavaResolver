using System;
using System.Linq;

namespace JavaResolver.Class.TypeSystem
{
    internal static class MemberExtensions
    {
        public static string GetFieldFullName(this IField field)
        {
            return field.DeclaringClass == null
                ? $"{field.Descriptor.FieldType.FullName} {field.Name}"
                : $"{field.Descriptor.FieldType.FullName} {field.DeclaringClass.Name}::{field.Name}";
        }

        public static string GetMethodFullName(this IMethod method)
        {
            return method.DeclaringClass == null
                ? string.Format("{0} {1}({2})",
                    method.Descriptor.ReturnType.FullName,
                    method.Name,
                    string.Join(", ", method.Descriptor.ParameterTypes.Select(x => x.FullName)))
                : string.Format("{0} {1}::{2}({3})",
                    method.Descriptor.ReturnType.FullName,
                    method.DeclaringClass.Name,
                    method.Name,
                    string.Join(", ", method.Descriptor.ParameterTypes.Select(x => x.FullName)));
        }
    }
}