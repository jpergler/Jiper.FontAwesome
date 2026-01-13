namespace Jiper.FontAwesome.IconExtractor.Configuration;

/// <summary>
/// Parses command-line arguments into generation options.
/// </summary>
public static class ArgumentParser
{
    private const string DefaultNamespace = "Jiper.FontAwesome.IconNames";
    private const string DefaultClassName = "FaIcons";

    private static string GetDefaultOutputPath(string className) =>
        Path.Combine(Environment.CurrentDirectory, "../../../../Jiper.FontAwesome.IconNames",
            Utilities.IdentifierHelper.SanitizeTypeName(className) + ".cs");

    /// <summary>
    /// Parses command-line arguments into generation options.
    /// Args: [outputPath] [targetNamespace] [className] [source]
    /// </summary>
    public static GenerationOptions Parse(string[] args)
    {
        var options = new GenerationOptions
        {
            TargetNamespace = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
                ? args[1]
                : DefaultNamespace,

            ClassName = args.Length > 2 && !string.IsNullOrWhiteSpace(args[2])
                ? args[2]
                : DefaultClassName,

            Source = args.Length > 3 && !string.IsNullOrWhiteSpace(args[3])
                ? args[3]
                : "pro"
        };

        options.OutputPath = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? Path.GetFullPath(args[0])
            : GetDefaultOutputPath(options.ClassName);

        return options;
    }
}
