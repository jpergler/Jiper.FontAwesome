using Jiper.FontAwesome.IconNames;

namespace Jiper.FontAwesome.Tests;

public class FaFactoryTests
{
    [Fact]
    public void TestPackMethods()
    {
        Assert.Equal("fa-solid fa-home", Fa.Classic.Solid("home"));
        Assert.Equal("fa-duotone fa-solid fa-home", Fa.Duotone.Solid("home"));
        Assert.Equal("fa-sharp fa-light fa-home", Fa.Sharp.Light("home"));
        Assert.Equal("fa-sharp-duotone fa-solid fa-home", Fa.SharpDuotone.Solid("home"));
    }

    [Fact]
    public void TestBaseIconMethod()
    {
        Assert.Equal("fa-notdog fa-solid fa-circle", Fa.Icon("notdog", "solid", "circle"));
    }
}