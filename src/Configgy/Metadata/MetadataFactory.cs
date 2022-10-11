using System.Collections;
using System.Reflection;
using Configgy.Attributes;

namespace Configgy.Metadata;

public class MetadataFactory
{
    public ConfigMetadata Create(Type configType)
    {
        var data = new ConfigMetadata();
        var basePath = GetPath(configType);
        var properties = GetProperties(configType);

        foreach (var property in properties) {
            var fieldMeta = new FieldMetadata
            {
                Name = property.Name,
                Path = GetPropertyPath(basePath, property),
                Type = property.PropertyType,
                IsCollection = IsCollection(property)
            };
            
            data.Fields.Add(fieldMeta);
        }
        
        return data;
    }

    private string GetPath(Type configType)
    {
        var pathAttribute = configType.GetCustomAttribute<PathAttribute>();
        return pathAttribute != null ? pathAttribute.Path : "";
    }
    
    private string GetPropertyPath(string basePath, PropertyInfo property)
    {
        var pathAttribute = property.GetCustomAttribute<PathAttribute>();
        if (pathAttribute != null) {
            return $"{basePath}.${pathAttribute.Path}";
        }

        var name = property.Name;
        return name.Substring(0, 1).ToLowerInvariant() + name.Substring(1);
    }

    private IReadOnlyList<PropertyInfo> GetProperties(Type configTypes)
    {
        return configTypes.GetProperties().ToList();
    }
    
    private bool IsCollection(PropertyInfo property)
    {
        if (property.PropertyType.IsAssignableTo(typeof(IList))) {
            return true;
        }

        if (property.PropertyType.Name == "IReadOnlyList`1") {
            return true;
        }

        return false;
    }
}
