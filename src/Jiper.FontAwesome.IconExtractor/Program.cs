using Jiper.FontAwesome.IconExtractor.Configuration;
using Jiper.FontAwesome.IconExtractor.Services;
using Jiper.FontAwesome.IconExtractor.Utilities;

namespace Jiper.FontAwesome.IconExtractor;

internal static class Program
{
    private const string DefaultNamespace = "Jiper.FontAwesome.IconNames";

    // Default output written into IconNames project; file name is determined from the class name (e.g., FaIconsPro.cs / FaIconsFree.cs)
    private static string GetDefaultOutputPath(string className) =>
        Path.Combine(Environment.CurrentDirectory, "../../../../Jiper.FontAwesome.IconNames", className + ".cs");

    private static int Main(string[] args)
    {
        try
        {
            var service = new IconGenerationService();

            // If no arguments are provided, generate both Pro and Free icon name files by default.
            if (args.Length == 0)
            {
                GenerateDefaultFiles(service);
                return 0;
            }

            // Parse command-line arguments
            var options = ArgumentParser.Parse(args);

            // Generate icon file
            // var result = service.Generate(options);
            // service.PrintResult(result);

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
            Console.Error.WriteLine(ex.ToString());
            return 1;
        }
    }

    /// <summary>
    /// Generates both Pro and Free icon name files by default.
    /// </summary>
    private static void GenerateDefaultFiles(IconGenerationService service)
    {
        // GenerateSingleFile(service, "FaIconsPro", "pro");
        // GenerateSingleFile(service, "FaIconsFree", "free");
        GenerateIconNamesFile(service);
    }

    private static void GenerateSingleFile(IconGenerationService service, string className, string source)
    {
        var options = new GenerationOptions
        {
            ClassName = className,
            TargetNamespace = DefaultNamespace,
            OutputPath = GetDefaultOutputPath(className),
            Source = source
        };

        var result = service.Generate(options);
        service.PrintResult(result);
    }

    private static void GenerateIconNamesFile(IconGenerationService service)
    {
        const string className = "Names";
        const string source = "pro";
        const string solidStyle = "solid";

        var outputPath = GetDefaultOutputPath("Fa.Names");
        var result = service.GenerateIconNames(className, DefaultNamespace, outputPath, source, solidStyle);
        service.PrintResult(result);
    }
}