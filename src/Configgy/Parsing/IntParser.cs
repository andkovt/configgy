using Configgy.Exception;

namespace Configgy.Parsing;

public class IntParser : IParser
{
    public Type Type => typeof(int);
    
    public object Parse(string value)
    {
        try {
            return int.Parse(value);
        }
        catch (FormatException e) {
            throw new ParsingException(value, "Value is not a valid integer");
        } 
    }
}
