using AndKovt.Configgy.Metadata;

namespace AndKovt.Configgy.Source;

public interface ISource
{
    object? Read(string path, FieldMetadata metadata);
}
