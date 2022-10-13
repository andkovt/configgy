using AndKovt.Configgy.Metadata;
using AndKovt.Configgy.Parsing;
using Configgy.Unit.Mocks;
using Moq;

namespace AndKovt.Configgy.Unit;

public class HydratorTest
{
    private readonly Hydrator instance;
    private readonly List<IParser> parsers;
    private readonly MockSource source;
    private readonly Mock<IMetadataFactory> metadataFactory;
    
    public HydratorTest()
    {
        source = new MockSource();
        metadataFactory = new Mock<IMetadataFactory>();
        parsers = new List<IParser>();
        instance = new Hydrator(parsers, source, metadataFactory.Object);
    }

    [Fact]
    public void Hydrate_ScalarValues_CreatesAndHydratesObject()
    {
        parsers.Add(new StringParser());
        parsers.Add(new IntParser());
        parsers.Add(new BoolParser());
        parsers.Add(new DoubleParser());
        parsers.Add(new LongParser());
        parsers.Add(new FloatParser());
        
        var configMetadata = new ConfigMetadata();

        MapScalarValue(configMetadata, "String", typeof(string), "test-string");
        MapScalarValue(configMetadata, "Int", typeof(int), "123");
        MapScalarValue(configMetadata, "Bool", typeof(bool), "true");
        MapScalarValue(configMetadata, "Double", typeof(double), "12,12");
        MapScalarValue(configMetadata, "Float", typeof(float), "13.13");
        MapScalarValue(configMetadata, "Long", typeof(long), long.MaxValue.ToString());
        
        metadataFactory.Setup(mf => mf.Create(typeof(ScalarConfig), "")).Returns(configMetadata);

        var result = (ScalarConfig) instance.Hydrate(typeof(ScalarConfig));
        
        Assert.Equal("test-string", result.String);
        Assert.Equal(123, result.Int);
        Assert.True(result.Bool);
        Assert.Equal(12.12, result.Double);
        Assert.Equal(13.13f, result.Float);
        Assert.Equal(long.MaxValue, result.Long);
    }

    private void MapScalarValue(ConfigMetadata metadata, string name, Type type, string value)
    {
        metadata.Fields.Add(new FieldMetadata {Name = name, Path = name.ToLower(), Type = type});
        source.MapValue(name.ToLower(), value);
    }
}

class ScalarConfig : IConfig
{
    public string String { get; set; }
    public int Int { get; set; }
    public bool Bool { get; set; }
    public double Double { get; set; }
    public float Float { get; set; }
    public long Long { get; set; }
}
