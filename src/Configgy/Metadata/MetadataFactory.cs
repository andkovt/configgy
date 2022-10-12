using System.Collections;
using System.Reflection;
using AndKovt.Configgy.Attributes;

namespace AndKovt.Configgy.Metadata;

public class MetadataFactory : IMetadataFactory
{
    public ConfigMetadata Create(Type configType, string path = "")
    {
        var data = new ConfigMetadata();
        var basePath = GetPath(configType, path);
        var properties = GetProperties(configType);
        data.Path = basePath;
        
        foreach (var property in properties) {
            var fieldMeta = new FieldMetadata
            {
                Name = property.Name,
                Path = GetPropertyPath(basePath, property),
                IsCollection = IsCollection(property)
            };

            fieldMeta.Type = GetPropertyType(fieldMeta, property.PropertyType);
            fieldMeta.IsNestedConfig = IsNestedConfig(fieldMeta);
            
            data.Fields.Add(fieldMeta);
        }
        
        return data;
    }

    private string GetPath(Type configType, string path)
    {
        if (!string.IsNullOrEmpty(path)) {
            return path;
        }
        
        var pathAttribute = configType.GetCustomAttribute<PathAttribute>();
        return pathAttribute != null ? pathAttribute.Path : "";
    }
    
    private string GetPropertyPath(string basePath, PropertyInfo property)
    {
        var pathAttribute = property.GetCustomAttribute<PathAttribute>();
        if (pathAttribute != null) {
            return $"{basePath}.{pathAttribute.Path}";
        }

        var name = property.Name.Substring(0, 1).ToLowerInvariant() + property.Name.Substring(1);
        return $"{basePath}.{name}";
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

    private Type GetPropertyType(FieldMetadata metadata, Type type)
    {
        if (!metadata.IsCollection) {
            return type;
        }
        
        return type.GenericTypeArguments[0];
    }

    private bool IsNestedConfig(FieldMetadata metadata)
    {
        if (metadata.Type.IsAssignableTo(typeof(IConfig))) {
            return true;
        }

        // if (metadata.IsCollection) {
        //     var collectionType = metadata.Type.GenericTypeArguments[0];
        //     if (collectionType.IsAssignableTo(typeof(IConfig))) {
        //         return true;
        //     }
        // }

        return false;
    }
}
