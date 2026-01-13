namespace Jiper.FontAwesome.IconNames;

public interface IFaIconCssClassBuilder
{
    IFaIconCssClassBuilder Pack(string? iconPack);
    IFaIconCssClassBuilder Style(string iconStyle);
    IFaIconCssClassBuilder Icon(string iconName);
    string Build();
}