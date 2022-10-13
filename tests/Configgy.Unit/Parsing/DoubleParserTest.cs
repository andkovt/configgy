using AndKovt.Configgy.Parsing;

namespace AndKovt.Configgy.Unit.Parsing;

public class DoubleParserTest
{
    private readonly DoubleParser instance;
    
    public DoubleParserTest()
    {
        instance = new DoubleParser();
    }

    [Fact]
    public void Parse_PeriodSeparatedDouble_ReturnsParsedDouble()
    {
        var value = "12.12";
        var result = instance.Parse(value);
        
        Assert.Equal(12.12, result);
    }
    
    [Fact]
    public void Parse_CommaSeparatedDouble_ReturnsParsedDouble()
    {
        var value = "12,12";
        var result = instance.Parse(value);
        
        Assert.Equal(12.12, result);
    }
}
