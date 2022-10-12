namespace AndKovt.Configgy.Metadata;

public interface IMetadataFactory
{
    public ConfigMetadata Create(Type configType, string path = "");
}
