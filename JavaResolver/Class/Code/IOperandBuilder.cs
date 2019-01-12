using JavaResolver.Class.Constants;

namespace JavaResolver.Class.Code
{
    public interface IOperandBuilder
    {
        int GetConstantIndex(ConstantInfo constantInfo);

        int GetStringIndex(string text);
    }
}