using AndKovt.Configgy.Metadata;

namespace AndKovt.Configgy.Source;

public interface ISource
{
    int? ReadLength(string path);
    object? Read(string path, FieldMetadata metadata);
}
