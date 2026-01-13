using System.Globalization;
using Jiper.FontAwesome.IconExtractor.CodeGeneration;
using Jiper.FontAwesome.IconExtractor.Configuration;
using Jiper.FontAwesome.IconExtractor.Utilities;

namespace Jiper.FontAwesome.IconExtractor.Services;

/// <summary>
/// Orchestrates the icon generation process from YAML parsing to code generation.
/// </summary>
public sealed class IconGenerationService
{
    /// <summary>
    /// Generates icon constants file based on the provided options.
    /// </summary>
    public GenerationResult Generate(GenerationOptions options)
    {
        // Fetch YAML content
        var yamlProvider = YamlProviderFactory.Create(options.Source);
        var yamlContent = yamlProvider.GetIconsYaml();

        // Parse YAML to icon data structures
        var iconsByStyle = FontAwesomeYamlParser.ParseIconsByStyle(yamlContent);

        // Count total icons
        var totalIcons = iconsByStyle.Values.Sum(list => list.Count);

        // Generate C# code
        var generatedCode = IconCodeGenerator.GenerateClassByStyle(
            options.TargetNamespace,
            options.ClassName,
            iconsByStyle);

        // Write to file
        FileSystemHelper.WriteGeneratedCode(options.OutputPath, generatedCode);

        return new GenerationResult
        {
            TotalIcons = totalIcons,
            OutputPath = options.OutputPath,
            Success = true
        };
    }

    /// <summary>
    /// Prints generation result to console.
    /// </summary>
    public void PrintResult(GenerationResult result)
    {
        if (result.Success)
        {
            Console.WriteLine(result.TotalIcons.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine($"Generated {result.TotalIcons.ToString(CultureInfo.InvariantCulture)} " +
                              $"icon name constants grouped by style into: {result.OutputPath}");
        }
    }
}

/// <summary>
/// Result of an icon generation operation.
/// </summary>
public sealed class GenerationResult
{
    public int TotalIcons { get; set; }
    public string OutputPath { get; set; } = string.Empty;
    public bool Success { get; set; }
}
