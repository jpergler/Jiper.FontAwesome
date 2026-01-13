namespace Jiper.FontAwesome.IconNames;

public class FaIconCssClassBuilder : IFaIconCssClassBuilder
{
    private readonly FaIconObject instance = new();

    public IFaIconCssClassBuilder Pack(string? iconPack)
    {
        instance.IconPack = iconPack;
        return this;
    }

    public IFaIconCssClassBuilder Style(string iconStyle)
    {
        instance.Style = iconStyle;
        return this;
    }

    public IFaIconCssClassBuilder Icon(string iconName)
    {
        instance.Icon = iconName;
        return this;
    }

    public string Build() => instance.Build();

    public override string ToString() => Build();
}