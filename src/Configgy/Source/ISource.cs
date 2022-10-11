using Configgy.Metadata;

namespace Configgy.Source;

public interface ISource
{
    object? Read(string path, FieldMetadata metadata);
}
