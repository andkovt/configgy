namespace AndKovt.Configgy.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class PathAttribute : Attribute
{
    public string Path { get; private set; }

    public PathAttribute(string path)
    {
        Path = path;
    }
}