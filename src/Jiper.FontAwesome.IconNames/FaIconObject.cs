namespace Jiper.FontAwesome.IconNames;

public class FaIconObject
{
    private const string Separator = " ";

    public string? IconPack { get; set; } = Fa.DefaultPack;
    public string Style { get; set; } = Fa.DefaultStyle;
    public string? Icon { get; set; }

    private string? NormalizedIconPack => FaUtils.NormalizeWithPrefix(Fa.Prefix, IconPack);
    private string NormalizedStyle => FaUtils.NormalizeWithPrefix(Fa.Prefix, Style);
    private string NormalizedIconName => FaUtils.NormalizeWithPrefix(Fa.Prefix, Icon) ?? throw new ArgumentNullException(nameof(Icon), "Icon name cannot be null");

    public string Build()
    {
        var parts = new List<string?>
            {
                NormalizedIconPack,
                NormalizedStyle,
                NormalizedIconName
            }
            .Where(x => x != null);

        return string.Join(Separator, parts);
    }
    
    public override string ToString() => Build();
}