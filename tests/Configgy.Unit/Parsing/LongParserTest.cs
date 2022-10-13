using AndKovt.Configgy.Parsing;

namespace AndKovt.Configgy.Unit.Parsing;

public class LongParserTest
{
    private readonly LongParser instance;

    public LongParserTest()
    {
        instance = new LongParser();
    }

    [Fact]
    public void Parse_ValidLong_ReturnsParsedLong()
    {
        var value = long.MaxValue.ToString();
        var result = instance.Parse(value);
        
        Assert.Equal(long.MaxValue, result);
    }
}
