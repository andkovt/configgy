using AndKovt.Configgy.Exception;

namespace AndKovt.Configgy.Parsing;

public class BoolParser : IParser
{
    private readonly Dictionary<string, bool> map = new()
    {
        {"1", true},
        {"0", false},
        {"yes", true},
        {"no", false}
    };
    
    public Type Type => typeof(bool);
    
    public object Parse(string value)
    {
        value = value.ToLower();
        if (map.ContainsKey(value)) {
            return map[value];
        }

        try {
            return bool.Parse(value);
        } catch (FormatException e) {
            throw new ParsingException(value, "Not a valid boolean");
        }
    }
}
