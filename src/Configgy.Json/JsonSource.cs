using AndKovt.Configgy.Metadata;
using AndKovt.Configgy.Source;

namespace AndKovt.Configgy.Json;

public class JsonSource : ISource
{
    public JsonSource(string file)
    {
        
    }
    
    public int? ReadLength(string path)
    {
        throw new NotImplementedException();
    }

    public object? Read(string path, FieldMetadata metadata)
    {
        throw new NotImplementedException();
    }
}
