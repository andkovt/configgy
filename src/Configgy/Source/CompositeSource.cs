using AndKovt.Configgy.Metadata;

namespace AndKovt.Configgy.Source;

public class CompositeSource : ISource
{
    private readonly IList<ISource> sources;

    public CompositeSource(IList<ISource> sources)
    {
        this.sources = sources;
    }

    public object? Read(string path, FieldMetadata metadata)
    {
        foreach (var source in sources) {
            var value = source.Read(path, metadata);

            if (value != null) {
                return value;
            }
        }

        return null;
    }
}