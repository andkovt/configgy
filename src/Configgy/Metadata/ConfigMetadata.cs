namespace AndKovt.Configgy.Metadata;

public class ConfigMetadata
{
    public string Path { get; set; }
    public List<FieldMetadata> Fields { get; set; } = new();
}
