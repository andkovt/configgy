using AndKovt.Configgy.Parsing;

namespace AndKovt.Configgy.Unit.Parsing;

public class FloatParserTest
{
    private readonly FloatParser instance;
    
    public FloatParserTest()
    {
        instance = new FloatParser();
    }

    [Fact]
    public void Parse_PeriodSeparatedFloat_ReturnsParsedFloat()
    {
        var value = "12.12";
        var result = instance.Parse(value);
        
        Assert.Equal(12.12f, result);
    }
    
    [Fact]
    public void Parse_CommaSeparatedFloat_ReturnsParsedFloat()
    {
        var value = "12,12";
        var result = instance.Parse(value);
        
        Assert.Equal(12.12f, result);
    }
}
