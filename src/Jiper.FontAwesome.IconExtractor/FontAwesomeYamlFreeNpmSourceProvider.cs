using System.Diagnostics;

namespace Jiper.FontAwesome.IconExtractor;

public sealed class FontAwesomeYamlFreeNpmSourceProvider
{
    private const string PackageName = "@fortawesome/fontawesome-free";
    private const string IconsYamlRelativePath = "metadata/icons.yml";

    public string GetIconsYaml()
    {
        EnsureNpmAvailable();

        var projectDir = FindClosestWithPackageJson()
                         ?? throw new InvalidOperationException("Could not locate package.json near the executable. Ensure package.json is present in the project directory.");

        InstallDependencies(projectDir);

        var yamlPath = Path.Combine(
            projectDir,
            "node_modules",
            "@fortawesome",
            "fontawesome-pro",
            IconsYamlRelativePath.Replace('/', Path.DirectorySeparatorChar));

        if (!File.Exists(yamlPath))
        {
            throw new FileNotFoundException(
                $"Could not find {IconsYamlRelativePath} in npm package {PackageName}. Please ensure the package is installed.",
                yamlPath);
        }

        return File.ReadAllText(yamlPath);
    }

    private static string? FindClosestWithPackageJson()
    {
        var dir = AppContext.BaseDirectory;
        while (!string.IsNullOrEmpty(dir))
        {
            var pkg = Path.Combine(dir, "package.json");
            if (File.Exists(pkg)) return dir;

            var parent = Directory.GetParent(dir);
            dir = parent?.FullName ?? string.Empty;
        }

        var cwdPkg = Path.Combine(Environment.CurrentDirectory, "package.json");
        return File.Exists(cwdPkg) ? Environment.CurrentDirectory : null;
    }

    private static void InstallDependencies(string workingDir)
    {
        var lockPath = Path.Combine(workingDir, "package-lock.json");
        if (File.Exists(lockPath))
        {
            RunProcess("npm", "ci", workingDir);
        }
        else
        {
            RunProcess("npm", "install", workingDir);
        }
    }

    private static void EnsureNpmAvailable()
    {
        try
        {
            RunProcess("npm", "--version", Environment.CurrentDirectory);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("npm is required but was not found on PATH. Please install Node.js/npm and try again.", ex);
        }
    }

    private static void RunProcess(string fileName, string arguments, string workingDirectory)
    {
        using var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
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
                $"Process `{fileName} {arguments}` in '{workingDirectory}' failed with code {proc.ExitCode}.{Environment.NewLine}" +
                $"STDOUT: {stdout}{Environment.NewLine}STDERR: {stderr}");
        }
    }
}
