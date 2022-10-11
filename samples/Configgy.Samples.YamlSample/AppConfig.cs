namespace AndKovt.Configgy.Samples.YamlSample;

public class AppConfig : IConfig
{
    public string StringField { get; set; }
    public int IntField { get; set; }
    public bool BoolField { get; set; }
    
    public string RandomField { get; set; }
    
    public IReadOnlyList<string> StringArrayField { get; set; }
    public IReadOnlyList<int> IntArrayField { get; set; }
}
