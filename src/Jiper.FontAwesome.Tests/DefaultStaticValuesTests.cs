using Jiper.FontAwesome.IconNames;

namespace Jiper.FontAwesome.Tests;

public class DefaultStaticValuesTests : IDisposable
{
    private readonly string initialStyle = Fa.DefaultStyle;
    private readonly string? initialPack = Fa.DefaultPack;

    [Fact]
    public void IconMethodsWithDifferentDefaultStyle_ReturnExpectedValue()
    {
        // Arrange
        // var initialStyle = Fa.DefaultStyle;
        Fa.DefaultStyle = FaIconStyles.Light;

        // Act & Assert
        Assert.Equal("fa-light fa-home", Fa.Classic.Icon("home"));
        Assert.Equal("fa-duotone fa-light fa-home", Fa.Duotone.Icon("home"));
        Assert.Equal("fa-sharp fa-light fa-home", Fa.Sharp.Icon("home"));
        Assert.Equal("fa-sharp-duotone fa-light fa-home", Fa.SharpDuotone.Icon("home"));

        // Fa.DefaultStyle = initialStyle;
    }

    [Fact]
    public void BaseIconMethodWithDifferentDefaultPack_ReturnsExpectedValue()
    {
        // Arrange
        // var initialPack = Fa.DefaultPack;
        Fa.DefaultPack = FaIconPacks.Duotone;

        // Act & Assert
        Assert.Equal("fa-duotone fa-solid fa-home", Fa.Icon("home"));

        // Fa.DefaultPack = initialPack;
    }

    public void Dispose()
    {
        Fa.DefaultStyle = initialStyle;
        Fa.DefaultPack = initialPack;
    }
}