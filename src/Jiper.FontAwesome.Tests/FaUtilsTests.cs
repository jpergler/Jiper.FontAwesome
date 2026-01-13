using Jiper.FontAwesome.IconNames;

namespace Jiper.FontAwesome.Tests;

public class FaUtilsTests
{
    [Theory]
    [InlineData("fa", "fa-home", "fa-home")]
    [InlineData("fa", "home", "fa-home")]
    [InlineData("fas", "fas-home", "fas-home")]
    [InlineData("fas", "home", "fas-home")]
    [InlineData("fa-", "home", "fa-home")]
    public void NormalizeWithPrefix_ReturnsExpectedValue(string prefix, string value, string expected)
    {
        // Act
        var result = FaUtils.NormalizeWithPrefix(prefix, value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void NormalizeWithPrefix_NullValue_ReturnsNull()
    {
        // Act
        var result = FaUtils.NormalizeWithPrefix("fa", null);

        // Assert
        Assert.Null(result);
    }
}