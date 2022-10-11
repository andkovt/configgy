using System.ComponentModel.DataAnnotations;
using Configgy.Metadata;
using Configgy.Parsing;
using Configgy.Source;

namespace Configgy;

public class ConfigBuilder<T> where T : IConfig
{
    private readonly List<ISource> sources = new();
    private readonly List<IParser> parsers = new();

    public ConfigBuilder<T> AddSource(ISource source)
    {
        sources.Add(source);

        return this;
    }

    public ConfigBuilder<T> AddParser(IParser parser)
    {
        parsers.Add(parser);

        return this;
    }

    public T Build()
    {
        AddDefaultParsers();

        var metadataFactory = new MetadataFactory();
        var hydrator = new Hydrator(parsers, new CompositeSource(sources), metadataFactory);

        var result = (T) hydrator.Hydrate(typeof(T));
        
        Validator.ValidateObject(result, new ValidationContext(result));
        
        return result;
    }

    private void AddDefaultParsers()
    {
        parsers.Add(new IntParser());
        parsers.Add(new BoolParser());
        parsers.Add(new StringParser());
    }
}
