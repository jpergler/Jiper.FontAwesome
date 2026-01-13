using Jiper.FontAwesome.IconNames;

namespace Jiper.FontAwesome.Tests;

public class DefaultStaticValuesTests
{
    [Fact]
    public void IconMethodsWithDifferentDefaultStyle_ReturnExpectedValue()
    {
        // Arrange
        Fa.DefaultStyle = FaIconStyles.Light;
        
        // Act & Assert
        Assert.Equal("fa-light fa-home", Fa.Classic.Icon("home"));
        Assert.Equal("fa-duotone fa-light fa-home", Fa.Duotone.Icon("home"));
        Assert.Equal("fa-sharp fa-light fa-home", Fa.Sharp.Icon("home"));
        Assert.Equal("fa-sharp-duotone fa-light fa-home", Fa.SharpDuotone.Icon("home"));
    } 
    
    [Fact]
    public void BaseIconMethodWithDifferentDefaultPack_ReturnsExpectedValue()
    {
        // Arrange
        Fa.DefaultPack = FaIconPacks.Duotone;
        
        // Act & Assert
        Assert.Equal("fa-duotone fa-solid fa-home", Fa.Icon("home"));
    }
}