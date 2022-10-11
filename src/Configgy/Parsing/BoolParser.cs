namespace Configgy.Parsing;

public class BoolParser : IParser
{
    public Type Type => typeof(bool);
    
    public object Parse(string value)
    {
        return bool.Parse(value);
    }
}
