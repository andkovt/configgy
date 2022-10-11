using System.Collections;
using AndKovt.Configgy.Exception;
using AndKovt.Configgy.Metadata;
using AndKovt.Configgy.Parsing;
using AndKovt.Configgy.Source;

namespace AndKovt.Configgy;

public class Hydrator
{
    private readonly IList<IParser> parsers;
    private readonly ISource source;
    private readonly MetadataFactory metadataFactory;

    public Hydrator(IList<IParser> parsers, ISource source, MetadataFactory metadataFactory)
    {
        this.parsers = parsers;
        this.source = source;
        this.metadataFactory = metadataFactory;
    }

    public IConfig Hydrate(Type config)
    {
        var instance = Activator.CreateInstance(config);
        if (instance == null) {
            throw new System.Exception("Unable to create config instance");
        }
        
        var meta = metadataFactory.Create(config);

        foreach (var field in meta.Fields) {
            var value = source.Read(field.Path, field);

            if (value == null) {
                continue;
            }

            value = ParseValue(value, field);
            
            instance.GetType().GetProperty(field.Name)!.SetValue(instance, value);
        }

        return (IConfig) instance;
    }

    private object ParseValue(object value, FieldMetadata field)
    {
        if (!field.IsCollection) {
            return FindParser(field.Name, field.Type).Parse(value.ToString()!);
        }
        
        if (!value.GetType().IsAssignableTo(typeof(IList))) {
            throw new ParsingException(value.ToString()!, "Unable to parse as a collection");
        }
        
        var genericType = field.Type.GenericTypeArguments[0];
        var listType = typeof(List<>).MakeGenericType(genericType);
        var list = (IList) Activator.CreateInstance(listType)!;
        var parser = FindParser(field.Name, genericType);

        foreach (var listValue in (List<object>) value) {
            list.Add(parser.Parse(listValue.ToString()!));
        }

        return list;
    }

    private IParser FindParser(string fieldName, Type type)
    {
        var parser = parsers.FirstOrDefault(p => p.Type == type);

        if (parser == null) {
            throw new UnknownFieldTypeException(fieldName, type.Name);
        }

        return parser;
    }
}
