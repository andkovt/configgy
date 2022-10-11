namespace Configgy.Exception;

public class UnknownFieldTypeException : System.Exception
{
    public UnknownFieldTypeException(string fieldName, string fieldType) : base(
        $"Unknown field type {fieldType} for field {fieldName}. Create a parser to support {fieldType}"
    )
    {
    }
}
