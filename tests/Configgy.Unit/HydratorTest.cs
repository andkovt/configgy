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
        
        var configMetadata = new ConfigMetadata();
        configMetadata.Fields.Add(new FieldMetadata {Name = "String", Path = "string", Type = typeof(string)});
        configMetadata.Fields.Add(new FieldMetadata {Name = "Int", Path = "int", Type = typeof(int)});
        configMetadata.Fields.Add(new FieldMetadata {Name = "Bool", Path = "bool", Type = typeof(bool)});

        metadataFactory.Setup(mf => mf.Create(typeof(ScalarConfig), "")).Returns(configMetadata);

        source.MapValue("string", "test-string");
        source.MapValue("int", 123);
        source.MapValue("bool", true);

        var result = (ScalarConfig) instance.Hydrate(typeof(ScalarConfig));
        
        Assert.Equal("test-string", result.String);
        Assert.Equal(123, result.Int);
        Assert.True(result.Bool);
    }
}

class ScalarConfig : IConfig
{
    public string String { get; set; }
    public int Int { get; set; }
    public bool Bool { get; set; }
}
