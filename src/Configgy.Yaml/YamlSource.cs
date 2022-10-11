using Configgy.Metadata;
using Configgy.Source;
using YamlDotNet.RepresentationModel;

namespace Configgy.Yaml;

public class YamlSource : ISource
{
    private readonly YamlStream yamlStream;

    public YamlSource(YamlStream yamlStream)
    {
        this.yamlStream = yamlStream;
    }

    public YamlSource(string filePath)
    {
        var fs = new StreamReader(File.OpenRead(filePath));
        var ys = new YamlStream();
        ys.Load(fs);

        yamlStream = ys;
    }

    public object? Read(string path, FieldMetadata metadata)
    {
        var document = yamlStream.Documents[0];
        var rootNode = (YamlMappingNode)document.RootNode;
        var splitPath = path.Split('.', StringSplitOptions.RemoveEmptyEntries);

        var value = GetValueRecursively(rootNode, splitPath);

        if (value is YamlSequenceNode) {
            var list = new List<object>();

            foreach (var node in ((YamlSequenceNode) value)) {
                list.Add(((YamlScalarNode) node).ToString());
            }

            return list;
        }

        return value?.ToString();
    }

    private YamlNode GetValueRecursively(YamlMappingNode node, string[] path)
    {
        var nodeName = path[0];
        var childNodeName = new YamlScalarNode(nodeName);

        if (!node.Children.ContainsKey(childNodeName)) {
            return null;
        }

        if (path.Length == 1) {
            return node.Children[childNodeName];
        }
        
        return GetValueRecursively((YamlMappingNode) node.Children[childNodeName], path.Skip(1).ToArray());
    }
}
