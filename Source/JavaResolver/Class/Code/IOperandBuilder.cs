using JavaResolver.Class.Constants;
using JavaResolver.Class.TypeSystem;

namespace JavaResolver.Class.Code
{
    public interface IOperandBuilder
    {
        int GetFieldIndex(FieldReference reference);

        int GetMethodIndex(MethodReference reference);

        int GetClassIndex(ClassReference reference);

        int GetDynamicIndex(DynamicInvocation invocation);

        int GetLiteralIndex(object constant);

        int GetStringIndex(string text);
    }
}