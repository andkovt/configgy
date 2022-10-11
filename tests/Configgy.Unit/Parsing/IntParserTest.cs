using Configgy.Exception;
using Configgy.Parsing;

namespace Configgy.Unit.Parsing;

public class IntParserTest
{
    private readonly IntParser instance;

    public IntParserTest()
    {
        instance = new IntParser();
    }

    [Fact]
    public void Parse_SmallIntAsString_ReturnsParsedInt()
    {
        var value = "123";
        var result = instance.Parse(value);
        
        Assert.Equal(int.Parse(value), result);
    }

    [Fact]
    public void Parse_NotInt_ThrowsException()
    {
        var value = "abc";
        Assert.Throws<ParsingException>(() => instance.Parse(value));
    }

    [Fact]
    public void Parse_NegativeInt_ReturnsNegativeInt()
    {
        var value = "-123";
        var result = instance.Parse(value);
        
        Assert.Equal(-123, result);
    }

    [Fact]
    public void Parse_Decimal_ThrowsException()
    {
        var value = "1.45";
        Assert.Throws<ParsingException>(() => instance.Parse(value));
    }
}
