namespace Jiper.FontAwesome.IconExtractor.Services;

/// <summary>
/// Factory for creating appropriate YAML provider based on source type.
/// </summary>
public static class YamlProviderFactory
{
    /// <summary>
    /// Creates a YAML provider based on the source type ("pro" or "free").
    /// </summary>
    /// <param name="source">"pro" uses npm (requires token), "free" uses GitHub (no token).</param>
    public static IIconYamlProvider Create(string source)
    {
        return source.Equals("free", StringComparison.OrdinalIgnoreCase)
            ? new FontAwesomeYamlFreeGitHubSourceProvider()
            : new FontAwesomeYamlFreeNpmSourceProvider();
    }
}
