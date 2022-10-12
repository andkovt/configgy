using AndKovt.Configgy.Metadata;
using AndKovt.Configgy.Source;

namespace Configgy.Unit.Mocks;

public class MockSource : ISource
{
    private readonly Dictionary<string, object> map = new ();
    private readonly Dictionary<string, int> lengthMap = new();

    public void MapValue(string path, object value)
    {
        map[path] = value;
    }

    public void MapLength(string path, int length)
    {
        lengthMap[path] = length;
    }
    
    public int? ReadLength(string path)
    {
        if (lengthMap.ContainsKey(path)) {
            return lengthMap[path];
        }

        return null;
    }

    public object? Read(string path, FieldMetadata metadata)
    {
        if (map.ContainsKey(path)) {
            return map[path];
        }

        return null;
    }
}
