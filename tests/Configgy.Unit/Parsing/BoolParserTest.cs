using Configgy.Exception;
using Configgy.Parsing;

namespace Configgy.Unit.Parsing;

public class BoolParserTest
{
    private readonly BoolParser instance;

    public BoolParserTest()
    {
        instance = new BoolParser();
    }

    [Fact]
    public void Parse_True_ReturnsTrue()
    {
        var value = "true";
        var result = instance.Parse(value);
        
        Assert.Equal(true, result);
    }

    [Fact]
    public void Parse_False_ReturnsFalse()
    {
        var value = "false";
        var result = instance.Parse(value);
        
        Assert.Equal(false, result);
    }

    [Theory]
    [InlineData("TRUE", true)]
    [InlineData("FALSE", false)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    [InlineData("yes", true)]
    [InlineData("no", false)]
    public void Parse_NonStandardBool_ReturnsCorrectValue(string value, bool expected)
    {
        var result = instance.Parse(value);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Parse_NotABoolean_ThrowsException()
    {
        var value = "ddd";
        Assert.Throws<ParsingException>(() => instance.Parse(value));
    }
}
