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

    public IConfig Hydrate(Type config, string path = "")
    {
        var instance = Activator.CreateInstance(config);
        if (instance == null) {
            throw new System.Exception("Unable to create config instance");
        }
        
        var meta = metadataFactory.Create(config, path);

        foreach (var field in meta.Fields) {
            object? value;

            if (field.IsCollection) {
                value = ReadCollection(field.Path, field);
            } else {
                value = ReadValue(field.Path, field);
            }

            if (value == null) {
                continue;
            }

            instance.GetType().GetProperty(field.Name)!.SetValue(instance, value);
        }

        return (IConfig) instance;
    }

    private object? ReadValue(string path, FieldMetadata field)
    {
        var value = source.Read(field.Path, field);

        if (value == null) {
            return null;
        }

        return ParseValue(value, field);
    }

    private object? ReadCollection(string path, FieldMetadata field)
    {
        var length = source.ReadLength(field.Path);
        var values = new List<object>();

        for (int i = 0; i < length; i++) {
            var newPath = $"{field.Path}[{i}]";
            
            if (field.IsNestedConfig) {
                values.Add(Hydrate(field.Type, newPath));
                continue;
            }

            var value = ParseValue(source.Read(newPath, field), field);

            if (value != null) {
                values.Add(value);
            }
        }

        var listType = typeof(List<>).MakeGenericType(field.Type);
        var list = (IList) Activator.CreateInstance(listType)!;

        foreach (var listValue in values) {
            if (field.IsNestedConfig) {
                list.Add(listValue);
                continue;
            }
            var parser = FindParser(field.Name, field.Type);
            list.Add(parser.Parse(listValue.ToString()!));
        }

        return list;
    }

    private object? ParseValue(object? value, FieldMetadata field)
    {
        if (value == null) {
            return null;
        }
        
        if (field.IsNestedConfig) {
            return Hydrate(field.Type, field.Path);
        }
        
        return FindParser(field.Name, field.Type).Parse(value.ToString()!);
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
