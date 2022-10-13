using System.Globalization;
using AndKovt.Configgy.Exception;

namespace AndKovt.Configgy.Parsing;

public class FloatParser : IParser
{
    public Type Type => typeof(float);
    public object Parse(string value)
    {
        // Replace periods with commas
        value = value.Replace(',', '.');
        try {
            return float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
        } catch (FormatException e) {
            throw new ParsingException(value, "Value is not a valid float");
        }
    }
}
