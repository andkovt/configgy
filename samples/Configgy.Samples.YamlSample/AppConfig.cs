namespace AndKovt.Configgy.Samples.YamlSample;

public class AppConfig : IConfig
{
    public IReadOnlyList<ObjectConfig> ObjectArray { get; set; }
    public ObjectConfig Object { get; set; }
    
    public string StringField { get; set; }
    public int IntField { get; set; }
    public bool BoolField { get; set; }
    
    public IReadOnlyList<string> StringArrayField { get; set; }
    public IReadOnlyList<int> IntArrayField { get; set; }
}

public class ObjectConfig : IConfig
{
    public string StringField { get; set; }
    public int IntField { get; set; }
    public bool BoolField { get; set; }
}