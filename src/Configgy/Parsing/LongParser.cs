using AndKovt.Configgy.Exception;

namespace AndKovt.Configgy.Parsing;

public class LongParser : IParser
{
    public Type Type => typeof(long);
    public object Parse(string value)
    {
        try {
            return long.Parse(value);
        } catch (FormatException e) {
            throw new ParsingException(value, "Value is not a valid long");
        }
    }
}
