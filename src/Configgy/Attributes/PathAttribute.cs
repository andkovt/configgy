namespace AndKovt.Configgy.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter)]
public class PathAttribute : Attribute
{
    public string Path { get; private set; }

    public PathAttribute(string path)
    {
        Path = path;
    }
}