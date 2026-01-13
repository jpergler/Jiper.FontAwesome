using Jiper.FontAwesome.IconNames;

namespace Jiper.FontAwesome.Tests;

public class FaIconBuilderTests
{
    [Theory]
    [InlineData(null, "solid", "home", "fa-solid fa-home")]
    [InlineData("duotone", "solid", "home", "fa-duotone fa-solid fa-home")]
    [InlineData("duotone", "regular", "home", "fa-duotone fa-regular fa-home")]
    [InlineData("notdog", "light", "home", "fa-notdog fa-light fa-home")]
    public void TestBuilder(string? iconPack, string style, string iconName, string expected)
    {
        // Arrange
        var builder = new FaIconCssClassBuilder();

        // Act
        var result = builder.Pack(iconPack).Style(style).Icon(iconName).Build();

        // Assert
        Assert.Equal(expected, result);
    }
}