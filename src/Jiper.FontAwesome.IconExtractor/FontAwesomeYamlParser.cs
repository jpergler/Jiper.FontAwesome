using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Jiper.FontAwesome.IconExtractor
{
    /// <summary>
    /// Parses Font Awesome YAML metadata and groups icons by style.
    /// </summary>
    public static class FontAwesomeYamlParser
    {
        private static readonly IDeserializer Deserializer =
            new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

        /// <summary>
        /// Parse YAML content and return a dictionary where:
        ///   - key: style (e.g., "solid", "brands", "regular", etc.)
        ///   - value: list of icons available in that style
        /// </summary>
        public static Dictionary<string, List<IconInfo>> ParseIconsByStyle(string yamlContent)
        {
            if (string.IsNullOrWhiteSpace(yamlContent))
                throw new ArgumentException("YAML content is null or empty.", nameof(yamlContent));

            var root = Deserializer.Deserialize<Dictionary<string, IconYaml>>(yamlContent)
                       ?? new Dictionary<string, IconYaml>(StringComparer.OrdinalIgnoreCase);

            var byStyle = new Dictionary<string, List<IconInfo>>(StringComparer.OrdinalIgnoreCase);

            foreach (var (iconName, icon) in root)
            {
                if (icon == null)
                    continue;

                // If styles are missing, skip this icon (or assign a placeholder if desired).
                if (icon.styles == null || icon.styles.Count == 0)
                    continue;

                foreach (var style in icon.styles)
                {
                    if (string.IsNullOrWhiteSpace(style))
                        continue;

                    if (!byStyle.TryGetValue(style, out var list))
                    {
                        list = new List<IconInfo>();
                        byStyle[style] = list;
                    }

                    list.Add(new IconInfo
                    {
                        Name = iconName,
                        Label = icon.label,
                        Unicode = icon.unicode,
                        Changes = icon.changes ?? new List<string>(),
                        SearchTerms = icon.search?.terms ?? new List<string>(),
                        Aliases = icon.aliases?.names ?? new List<string>(),
                        AliasUnicodesComposite = icon.aliases?.unicodes?.composite ?? new List<string>(),
                        Voted = icon.voted ?? false
                    });
                }
            }

            return byStyle;
        }

        /// <summary>
        /// Parse a YAML file and return a dictionary grouped by style.
        /// </summary>
        public static async Task<Dictionary<string, List<IconInfo>>> ParseIconsByStyleFromFileAsync(
            string yamlFilePath,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(yamlFilePath))
                throw new ArgumentException("File path is null or empty.", nameof(yamlFilePath));

            await using var fs = File.OpenRead(yamlFilePath);
            using var sr = new StreamReader(fs);
            var content = await sr.ReadToEndAsync();

            cancellationToken.ThrowIfCancellationRequested();

            return ParseIconsByStyle(content);
        }
    }

    /// <summary>
    /// Icon information normalized for consumers.
    /// </summary>
    public sealed class IconInfo
    {
        public string Name { get; set; } = "";
        public string? Label { get; set; }
        public string? Unicode { get; set; }
        public List<string> Changes { get; set; } = new();
        public List<string> SearchTerms { get; set; } = new();
        public List<string> Aliases { get; set; } = new();
        public List<string> AliasUnicodesComposite { get; set; } = new();
        public bool Voted { get; set; }
    }

    // Strongly-typed models for YAML input
    // These match the structure of the Font Awesome YAML snippet in the prompt.
    internal sealed class IconYaml
    {
        public Aliases? aliases { get; set; }
        public List<string>? changes { get; set; }
        public string? label { get; set; }
        public Search? search { get; set; }
        public List<string>? styles { get; set; }
        public string? unicode { get; set; }
        public bool? voted { get; set; }
    }

    internal sealed class Aliases
    {
        public List<string>? names { get; set; }
        public Unicodes? unicodes { get; set; }
    }

    internal sealed class Unicodes
    {
        public List<string>? composite { get; set; }
    }

    internal sealed class Search
    {
        public List<string>? terms { get; set; }
    }
}