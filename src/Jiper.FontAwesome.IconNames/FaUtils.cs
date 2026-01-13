using System.Diagnostics.CodeAnalysis;

namespace Jiper.FontAwesome.IconNames;

public static class FaUtils
{
    [return: NotNullIfNotNull("value")]
    public static string? NormalizeWithPrefix(string prefix, string? value)
    {
        var normalizedPrefixWithDash = prefix.EndsWith("-") ? prefix : prefix + "-";
        return value is null
            ? null
            : value.StartsWith(normalizedPrefixWithDash)
                ? value
                : $"{normalizedPrefixWithDash}{value}";
    }
}