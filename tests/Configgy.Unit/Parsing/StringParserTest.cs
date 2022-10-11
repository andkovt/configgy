using Configgy.Parsing;

namespace Configgy.Unit.Parsing;

public class StringParserTest
{
    private readonly StringParser instance;
    
    public StringParserTest()
    {
        instance = new StringParser();
    }

    [Fact]
    public void Parse_GenericString_ReturnsSameString()
    {
        var value = "test-string";
        var result = instance.Parse(value);
        
        Assert.Equal(value, result);
    }
}
