namespace AndKovt.Configgy.Parsing;

public class StringParser : IParser
{
    public Type Type => typeof(string);
    
    public object Parse(string value)
    {
        return value;
    }
}
