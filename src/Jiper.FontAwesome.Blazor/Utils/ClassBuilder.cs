using System.Text;
using Jiper.FontAwesome.Blazor.Enums;

namespace Jiper.FontAwesome.Blazor.Utils;

internal static class ClassBuilder
{
    // Fluent builder factory
    public static Builder Create() => new();

    // Fluent builder implementation
    public sealed class Builder
    {
        private readonly StringBuilder sb = new();

        public Builder Add(string? part)
        {
            if (!string.IsNullOrWhiteSpace(part))
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(part);
            }

            return this;
        }

        public Builder Add(params string?[] parts)
        {
            foreach (var part in parts)
            {
                Add(part);
            }

            return this;
        }

        public Builder AddIf(bool condition, string? part)
        {
            if (condition)
            {
                Add(part);
            }

            return this;
        }

        public string Build() => sb.ToString();

        public override string ToString() => sb.ToString();
    }

    public static string Build(params string?[] parts)
    {
        var sb = new StringBuilder();
        foreach (var part in parts)
        {
            if (!string.IsNullOrWhiteSpace(part))
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(part);
            }
        }

        return sb.ToString();
    }

    public static string? StyleToClass(IconStyle style) => style switch
    {
        IconStyle.Solid => "fa-solid",
        IconStyle.Regular => "fa-regular",
        IconStyle.Light => "fa-light",
        IconStyle.Thin => "fa-thin",
        IconStyle.Duotone => "fa-duotone",
        IconStyle.Brands => "fa-brands",
        _ => null
    };

    public static string? SizeToClass(IconSize size) => size switch
    {
        IconSize.None => null,
        IconSize.Xs => "fa-xs",
        IconSize.Sm => "fa-sm",
        IconSize.Lg => "fa-lg",
        IconSize.X1 => "fa-1x",
        IconSize.X2 => "fa-2x",
        IconSize.X3 => "fa-3x",
        IconSize.X4 => "fa-4x",
        IconSize.X5 => "fa-5x",
        IconSize.X6 => "fa-6x",
        IconSize.X7 => "fa-7x",
        IconSize.X8 => "fa-8x",
        IconSize.X9 => "fa-9x",
        IconSize.X10 => "fa-10x",
        _ => null
    };

    public static string? AnimationToClass(IconAnimation anim) => anim switch
    {
        IconAnimation.None => null,
        IconAnimation.Spin => "fa-spin",
        IconAnimation.Pulse => "fa-pulse",
        IconAnimation.Beat => "fa-beat",
        IconAnimation.Bounce => "fa-bounce",
        IconAnimation.Fade => "fa-fade",
        IconAnimation.BeatFade => "fa-beat-fade",
        IconAnimation.Shake => "fa-shake",
        IconAnimation.Flip => "fa-flip",
        _ => null
    };

    public static string? RotationToClass(IconRotation rotation) => rotation switch
    {
        IconRotation.None => null,
        IconRotation.Rotate90 => "fa-rotate-90",
        IconRotation.Rotate180 => "fa-rotate-180",
        IconRotation.Rotate270 => "fa-rotate-270",
        _ => null
    };

    public static string? FlipToClass(IconFlip flip) => flip switch
    {
        IconFlip.None => null,
        IconFlip.Horizontal => "fa-flip-horizontal",
        IconFlip.Vertical => "fa-flip-vertical",
        IconFlip.Both => "fa-flip-both",
        _ => null
    };

    public static string? PullToClass(IconPull pull) => pull switch
    {
        IconPull.None => null,
        IconPull.Left => "fa-pull-left",
        IconPull.Right => "fa-pull-right",
        _ => null
    };

    public static string? StackLayerToClass(StackLayerSize layer) => layer switch
    {
        StackLayerSize.None => null,
        StackLayerSize.OneX => "fa-stack-1x",
        StackLayerSize.TwoX => "fa-stack-2x",
        _ => null
    };

    public static string NormalizeIconName(string icon)
    {
        if (string.IsNullOrWhiteSpace(icon)) return icon;
        var trimmed = icon.Trim();
        if (trimmed.StartsWith("fa-", System.StringComparison.OrdinalIgnoreCase))
        {
            return trimmed.ToLowerInvariant();
        }

        // Convert to kebab case and prefix with fa-
        var sb = new StringBuilder();
        for (var i = 0; i < trimmed.Length; i++)
        {
            var c = trimmed[i];
            if (char.IsWhiteSpace(c) || c == '_' || c == '.')
            {
                sb.Append('-');
            }
            else if (char.IsUpper(c))
            {
                if (i > 0 && char.IsLower(trimmed[i - 1])) sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(char.ToLowerInvariant(c));
            }
        }

        var text = sb.ToString().Trim('-');
        return string.IsNullOrEmpty(text) ? "fa-question" : $"fa-{text}";
    }
}