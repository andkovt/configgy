namespace AndKovt.Configgy.Metadata;

public class FieldMetadata
{
    public string Name { get; set; }
    public string Path { get; set; }
    public Type Type { get; set; }
    public bool IsCollection { get; set; }
}
