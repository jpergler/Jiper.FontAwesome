using System;
using System.Collections.Generic;

namespace Jiper.FontAwesome.IconNames;

public class FaIcons
{
    private static readonly HashSet<string> ValidStylePrefixes = new(StringComparer.OrdinalIgnoreCase)
    {
        "fal", "far", "fas", "fab", "fat", "fad"
    };

    public static string Icon(string iconName, string? iconStylePrefix = null)
    {
        if (string.IsNullOrWhiteSpace(iconName))
            throw new ArgumentException("iconName cannot be null or empty.", nameof(iconName));

        iconName = iconName.Trim();
        iconStylePrefix = iconStylePrefix?.Trim();

        // Determine existing style and icon token from iconName
        string? existingStyle = null;
        string? iconToken = null;

        var parts = iconName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            if (existingStyle == null && ValidStylePrefixes.Contains(part))
            {
                existingStyle = part;
                continue;
            }

            if (iconToken == null && part.StartsWith("fa-", StringComparison.OrdinalIgnoreCase))
            {
                iconToken = part;
                continue;
            }
        }

        // If icon token was not found (e.g., "arrow-right"), build it
        if (iconToken == null)
        {
            foreach (var part in parts)
            {
                if (ValidStylePrefixes.Contains(part))
                    continue;

                var candidate = part;
                if (!candidate.StartsWith("fa-", StringComparison.OrdinalIgnoreCase))
                {
                    candidate = "fa-" + candidate.TrimStart('-');
                }

                iconToken = candidate;
                break;
            }

            // Fallback if still not found
            if (iconToken == null)
            {
                var candidate = iconName;
                if (!candidate.StartsWith("fa-", StringComparison.OrdinalIgnoreCase))
                {
                    candidate = "fa-" + candidate.TrimStart('-');
                }
                iconToken = candidate;
            }
        }

        // Determine desired style: override with provided valid style if given;
        // otherwise keep existing style; otherwise default to "fal".
        string desiredStyle = existingStyle ?? "fal";
        if (!string.IsNullOrWhiteSpace(iconStylePrefix) && ValidStylePrefixes.Contains(iconStylePrefix))
        {
            desiredStyle = iconStylePrefix!;
        }

        return $"{desiredStyle} {iconToken}";
    }
}