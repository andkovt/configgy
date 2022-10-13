using AndKovt.Configgy.Attributes;
using AndKovt.Configgy.Metadata;

namespace AndKovt.Configgy.Unit.Metadata;

public class MetadataFactoryTest
{
    private readonly MetadataFactory instance;

    public MetadataFactoryTest()
    {
        instance = new MetadataFactory();
    }

    [Fact]
    public void Create_ClassWithPublicProperties_ReturnsMetadata()
    {
        var result = instance.Create(typeof(TestClass1));
        
        Assert.Equal("", result.Path);
        Assert.Equal(3, result.Fields.Count);
        
        Assert.Equal("Property1", result.Fields[0].Name);
        Assert.Equal("property1", result.Fields[0].Path);
        Assert.Equal(typeof(int), result.Fields[0].Type);
        Assert.False(result.Fields[0].IsCollection);
        Assert.False(result.Fields[0].IsNestedConfig);
        
        Assert.Equal("Property2", result.Fields[1].Name);
        Assert.Equal("property2", result.Fields[1].Path);
        Assert.Equal(typeof(string), result.Fields[1].Type);
        Assert.False(result.Fields[1].IsCollection);
        Assert.False(result.Fields[1].IsNestedConfig);
        
        Assert.Equal("Property3", result.Fields[2].Name);
        Assert.Equal("property3", result.Fields[2].Path);
        Assert.Equal(typeof(bool), result.Fields[2].Type);
        Assert.False(result.Fields[2].IsCollection);
        Assert.False(result.Fields[2].IsNestedConfig);
    }

    [Fact]
    public void Create_PathAttributeSpecified_UsesPathFromAttribute()
    {
        var result = instance.Create(typeof(TestClass2));
        
        Assert.Equal("root", result.Path);
        Assert.Equal(2, result.Fields.Count);
        
        Assert.Equal("Property1", result.Fields[0].Name);
        Assert.Equal("root.property3", result.Fields[0].Path);
        Assert.Equal(typeof(int), result.Fields[0].Type);
        Assert.False(result.Fields[0].IsCollection);
        Assert.False(result.Fields[0].IsNestedConfig);
        
        Assert.Equal("Property2", result.Fields[1].Name);
        Assert.Equal("root.property2", result.Fields[1].Path);
        Assert.Equal(typeof(string), result.Fields[1].Type);
        Assert.False(result.Fields[1].IsCollection);
        Assert.False(result.Fields[1].IsNestedConfig);
    }
    
    [Fact]
    public void Create_PrivateProperties_IgnoresPrivateProperties()
    {
        var result = instance.Create(typeof(TestClass3));
        
        Assert.Equal("", result.Path);
        Assert.Single(result.Fields);
        
        Assert.Equal("Property1", result.Fields[0].Name);
        Assert.Equal("property1", result.Fields[0].Path);
        Assert.Equal(typeof(int), result.Fields[0].Type);
        Assert.False(result.Fields[0].IsCollection);
        Assert.False(result.Fields[0].IsNestedConfig);
    }

    [Fact]
    public void Create_Collections_IdentifiesCollections()
    {
        var result = instance.Create(typeof(TestClass4));
        
        Assert.Equal(3, result.Fields.Count);
        
        Assert.Equal("Property1", result.Fields[0].Name);
        Assert.Equal("property1", result.Fields[0].Path);
        Assert.Equal(typeof(int), result.Fields[0].Type);
        Assert.False(result.Fields[0].IsCollection);
        Assert.False(result.Fields[0].IsNestedConfig);
        
        Assert.Equal("Collection1", result.Fields[1].Name);
        Assert.Equal("collection1", result.Fields[1].Path);
        Assert.Equal(typeof(string), result.Fields[1].Type);
        Assert.True(result.Fields[1].IsCollection);
        Assert.False(result.Fields[1].IsNestedConfig);
        
        Assert.Equal("Collection2", result.Fields[2].Name);
        Assert.Equal("collection2", result.Fields[2].Path);
        Assert.Equal(typeof(string), result.Fields[2].Type);
        Assert.True(result.Fields[2].IsCollection);
        Assert.False(result.Fields[2].IsNestedConfig);
    }

    [Fact]
    public void Create_NestedConfig_IdentifiesNestedConfig()
    {
        var result = instance.Create(typeof(TestClass5));

        Assert.Single(result.Fields);
        Assert.Equal("Property1", result.Fields[0].Name);
        Assert.Equal("property1", result.Fields[0].Path);
        Assert.Equal(typeof(TestClass4), result.Fields[0].Type);
        Assert.False(result.Fields[0].IsCollection);
        Assert.True(result.Fields[0].IsNestedConfig);
    }
    
    public class TestClass1 : IConfig
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }
        public bool Property3 { get; set; }
    }
    
    [Path("root")]
    public class TestClass2 : IConfig
    {
        [Path("property3")]
        public int Property1 { get; set; }
        public string Property2 { get; set; }
    }

    public class TestClass3 : IConfig
    {
        public int Property1 { get; set; }
        private string Property2 { get; set; }
    }
    
    public class TestClass4 : IConfig
    {
        public int Property1 { get; set; }
        
        public List<string> Collection1 { get; set; }
        public IReadOnlyList<string> Collection2 { get; set; }
    }
    
    public class TestClass5 : IConfig
    {
        public TestClass4 Property1 { get; set; }
    }
}
