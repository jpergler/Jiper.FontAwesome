using System.Text;

namespace Jiper.FontAwesome.IconExtractor.Utilities;

/// <summary>
/// Helper methods for file system operations.
/// </summary>
public static class FileSystemHelper
{
    /// <summary>
    /// Writes generated code to a file with UTF-8 BOM encoding.
    /// Creates the output directory if it doesn't exist.
    /// </summary>
    public static void WriteGeneratedCode(string outputPath, string content)
    {
        var outputDir = Path.GetDirectoryName(outputPath) ?? Environment.CurrentDirectory;
        Directory.CreateDirectory(outputDir);
        File.WriteAllText(outputPath, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }
}
