using System.Diagnostics;

namespace Jiper.FontAwesome.IconExtractor;

public sealed class FontAwesomeYamlFreeGitHubSourceProvider : IIconYamlProvider
{
    private const string RepoUrl = "https://github.com/FortAwesome/Font-Awesome.git";
    private const string IconsYamlRelativePath = "metadata/icons.yml";

    public string GetIconsYaml()
    {
        EnsureGitAvailable();

        var tempDir = Path.Combine(Path.GetTempPath(), "fa-repo-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            RunProcess("git", $"clone --depth 1 {RepoUrl} \"{tempDir}\"");

            var yamlPath = Path.Combine(tempDir, IconsYamlRelativePath.Replace('/', Path.DirectorySeparatorChar));
            if (!File.Exists(yamlPath))
            {
                throw new FileNotFoundException("Could not find metadata/icons.yml in cloned repository.", yamlPath);
            }

            return File.ReadAllText(yamlPath);
        }
        finally
        {
            TryDeleteDirectory(tempDir);
        }
    }

    private static void EnsureGitAvailable()
    {
        try
        {
            RunProcess("git", "--version");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Git is required but was not found on PATH. Please install Git and try again.", ex);
        }
    }

    private static void RunProcess(string fileName, string arguments)
    {
        using var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };

        proc.Start();
        var stdout = proc.StandardOutput.ReadToEnd();
        var stderr = proc.StandardError.ReadToEnd();
        proc.WaitForExit();

        if (proc.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"Process `{fileName} {arguments}` failed with code {proc.ExitCode}.{Environment.NewLine}" +
                $"STDOUT: {stdout}{Environment.NewLine}STDERR: {stderr}");
        }
    }

    private static void TryDeleteDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return;
        try
        {
            foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                try
                {
                    var attr = File.GetAttributes(file);
                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        File.SetAttributes(file, attr & ~FileAttributes.ReadOnly);
                }
                catch
                {
                    // ignore
                }
            }

            Directory.Delete(path, recursive: true);
        }
        catch
        {
            // ignore cleanup errors
        }
    }
}
