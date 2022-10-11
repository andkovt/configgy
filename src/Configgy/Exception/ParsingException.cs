namespace AndKovt.Configgy.Exception;

public class ParsingException : System.Exception
{
    public ParsingException(string value, string message) : base($"Unable to parse {value}. Reason: {message}")
    {
        
    }
}
