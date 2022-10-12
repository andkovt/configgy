using System.Text.RegularExpressions;
using AndKovt.Configgy.Metadata;
using AndKovt.Configgy.Source;
using YamlDotNet.RepresentationModel;

namespace AndKovt.Configgy.Yaml;

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

    public int? ReadLength(string path)
    {
        var document = yamlStream.Documents[0];
        var rootNode = (YamlMappingNode)document.RootNode;
        var splitPath = path.Split('.', StringSplitOptions.RemoveEmptyEntries);

        var value = (YamlSequenceNode) GetValueRecursively(rootNode, splitPath);

        return value.Count();
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
        int index = -1;
        if (nodeName.Contains('[')) {
            var match = new Regex("\\[(\\d*)\\]").Match(nodeName);
            nodeName = nodeName.Substring(0, nodeName.IndexOf('['));
            index = int.Parse(match.Groups[1].Value);
        }
        var childNodeName = new YamlScalarNode(nodeName);
        
        if (!node.Children.ContainsKey(childNodeName)) {
            return null;
        }

        var childNode = node.Children[childNodeName];
        if (index >= 0) {
            childNode = childNode[index];
        }

        if (path.Length == 1) {
            return childNode;
        }
        
        return GetValueRecursively((YamlMappingNode) childNode, path.Skip(1).ToArray());
    }
}
