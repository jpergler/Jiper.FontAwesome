namespace Jiper.FontAwesome.IconExtractor.CodeGeneration;

/// <summary>
/// Maps Font Awesome style names to their CSS class prefixes.
/// </summary>
public static class StylePrefixMapper
{
    /// <summary>
    /// Maps FA style names to their class prefixes (e.g., "solid" -> "fas").
    /// </summary>
    public static string GetFaStylePrefix(string style)
    {
        if (string.IsNullOrWhiteSpace(style))
            return "fa";

        var normalizedStyle = style.Trim().ToLowerInvariant();

        // Handle common styles and combinations (e.g., sharp-solid contains "solid")
        if (normalizedStyle.Contains("brand")) return "fab";
        if (normalizedStyle.Contains("regular")) return "far";
        if (normalizedStyle.Contains("solid")) return "fas";
        if (normalizedStyle.Contains("light")) return "fal";
        if (normalizedStyle.Contains("thin")) return "fat";
        if (normalizedStyle.Contains("duotone")) return "fad";

        return "fa";
    }
}
