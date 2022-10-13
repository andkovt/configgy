using AndKovt.Configgy.Exception;

namespace AndKovt.Configgy.Parsing;

public class DoubleParser : IParser
{
    public Type Type => typeof(double);
    public object Parse(string value)
    {
        // Replace periods with commas
        value = value.Replace('.', ',');
        
        try {
            return double.Parse(value);
        }
        catch (FormatException e) {
            throw new ParsingException(value, "Value is not a valid double");
        }
    }
}
