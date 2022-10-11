namespace AndKovt.Configgy.Parsing;

public interface IParser
{
    Type Type { get; }
    object Parse(string value);
}
