namespace Jiper.FontAwesome.IconExtractor.Configuration;

/// <summary>
/// Configuration options for icon code generation.
/// </summary>
public sealed class GenerationOptions
{
    public string OutputPath { get; set; } = string.Empty;
    public string TargetNamespace { get; set; } = "Jiper.FontAwesome.IconNames";
    public string ClassName { get; set; } = "FaIcons";
    public string Source { get; set; } = "pro"; // "pro" or "free"
}
