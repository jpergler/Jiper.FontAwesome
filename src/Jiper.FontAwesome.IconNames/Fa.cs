namespace Jiper.FontAwesome.IconNames;

public class Fa
{
    public static string? DefaultPack = FaIconPacks.Classic;
    public static string DefaultStyle = FaIconStyles.Solid;

    public static string Prefix = "fa";

    public static string Icon(string pack, string style, string icon) => new FaIconCssClassBuilder().Pack(pack).Style(style).Icon(icon).Build();
    public static string Icon(string icon) => new FaIconCssClassBuilder().Pack(DefaultPack).Style(DefaultStyle).Icon(icon).Build();

    public static class Classic
    {
        public static string Icon(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Classic).Style(DefaultStyle).Icon(icon).Build();

        public static string Solid(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Classic).Style(FaIconStyles.Solid).Icon(icon).Build();
        public static string Regular(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Classic).Style(FaIconStyles.Regular).Icon(icon).Build();
        public static string Light(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Classic).Style(FaIconStyles.Light).Icon(icon).Build();
        public static string Thin(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Classic).Style(FaIconStyles.Thin).Icon(icon).Build();
    }

    public class Duotone
    {
        public static string Icon(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Duotone).Style(DefaultStyle).Icon(icon).Build();

        public static string Solid(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Duotone).Style(FaIconStyles.Solid).Icon(icon).Build();
        public static string Regular(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Duotone).Style(FaIconStyles.Regular).Icon(icon).Build();
        public static string Light(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Duotone).Style(FaIconStyles.Light).Icon(icon).Build();
        public static string Thin(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Duotone).Style(FaIconStyles.Thin).Icon(icon).Build();
    }

    public class Sharp
    {
        public static string Icon(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Sharp).Style(DefaultStyle).Icon(icon).Build();

        public static string Solid(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Sharp).Style(FaIconStyles.Solid).Icon(icon).Build();
        public static string Regular(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Sharp).Style(FaIconStyles.Regular).Icon(icon).Build();
        public static string Light(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Sharp).Style(FaIconStyles.Light).Icon(icon).Build();
        public static string Thin(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.Sharp).Style(FaIconStyles.Thin).Icon(icon).Build();
    }

    public class SharpDuotone
    {
        public static string Icon(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.SharpDuotone).Style(DefaultStyle).Icon(icon).Build();

        public static string Solid(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.SharpDuotone).Style(FaIconStyles.Solid).Icon(icon).Build();
        public static string Regular(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.SharpDuotone).Style(FaIconStyles.Regular).Icon(icon).Build();
        public static string Light(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.SharpDuotone).Style(FaIconStyles.Light).Icon(icon).Build();
        public static string Thin(string icon) => new FaIconCssClassBuilder().Pack(FaIconPacks.SharpDuotone).Style(FaIconStyles.Thin).Icon(icon).Build();
    }
}