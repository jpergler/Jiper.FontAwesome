using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Jiper.FontAwesome.IconExtractor.Utilities;

/// <summary>
/// Utility for converting strings to valid C# identifiers.
/// </summary>
public static class IdentifierHelper
{
    private const string DefaultClassName = "FaIcons";

    /// <summary>
    /// Converts a kebab-case or other string to a valid PascalCase C# identifier.
    /// </summary>
    public static string ToPascalCaseIdentifier(string kebab)
    {
        if (string.IsNullOrWhiteSpace(kebab))
            return "_";

        string id = ConvertToIdentifier(kebab);
        id = EnsureValidStart(id);
        id = CleanInvalidCharacters(id);
        id = EscapeKeywords(id);
        id = AvoidObjectMemberConflicts(id);

        return id;
    }

    /// <summary>
    /// Ensures a valid type name (class name).
    /// </summary>
    public static string SanitizeTypeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return DefaultClassName;

        var id = ToPascalCaseIdentifier(name);

        // Remove leading @ if any (not valid for type names)
        if (id.StartsWith("@", StringComparison.Ordinal))
            id = id.Substring(1);

        return id;
    }

    private static string ConvertToIdentifier(string input)
    {
        // If the input is a single alphanumeric token (no separators) and appears to be
        // camelCase/PascalCase already, preserve its internal casing and just ensure
        // the first character is uppercase (when it's a letter).
        if (Regex.IsMatch(input, @"^[A-Za-z0-9]+$") && (char.IsUpper(input[0]) || input.Skip(1).Any(char.IsUpper)))
        {
            return char.IsLetter(input[0])
                ? char.ToUpper(input[0], CultureInfo.InvariantCulture) + input.Substring(1)
                : input;
        }

        // Split on non-alphanumeric characters and PascalCase each part
        var parts = Regex.Split(input, "[^A-Za-z0-9]+")
            .Where(p => p.Length > 0)
            .ToArray();

        var sb = new StringBuilder();
        foreach (var part in parts)
        {
            if (part.Length == 0)
                continue;

            // If the part is all digits, keep as is
            if (part.All(char.IsDigit))
            {
                sb.Append(part);
                continue;
            }

            // Title case: first letter upper, rest lower (preserve digits)
            var first = part[0];
            var rest = part.Length > 1 ? part.Substring(1) : string.Empty;

            sb.Append(char.ToUpper(first, CultureInfo.InvariantCulture));
            sb.Append(rest.ToLower(CultureInfo.InvariantCulture));
        }

        return sb.Length > 0 ? sb.ToString() : "_";
    }

    private static string EnsureValidStart(string id)
    {
        // If starts with a digit or invalid start char, prefix underscore
        if (id.Length > 0 && !IsValidIdentifierStart(id[0]))
            id = "_" + id;

        return id;
    }

    private static string CleanInvalidCharacters(string id)
    {
        var cleaned = new StringBuilder(id.Length);
        foreach (var ch in id)
        {
            cleaned.Append(IsValidIdentifierPart(ch) ? ch : '_');
        }

        return cleaned.ToString();
    }

    private static string EscapeKeywords(string id)
    {
        return CSharpKeywords.Contains(id) ? "@" + id : id;
    }

    private static string AvoidObjectMemberConflicts(string id)
    {
        // Avoid conflicts with System.Object member names (e.g., Equals, ToString, GetHashCode, GetType, ...)
        return ObjectMemberNames.Contains(id) ? id + "_" : id;
    }

    private static bool IsValidIdentifierStart(char ch) =>
        ch == '_' || char.IsLetter(ch);

    private static bool IsValidIdentifierPart(char ch) =>
        ch == '_' || char.IsLetterOrDigit(ch);

    private static readonly HashSet<string> CSharpKeywords = new(StringComparer.Ordinal)
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
        "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
        "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if",
        "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new",
        "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
        "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static",
        "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
        "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while",
        // contextual keywords
        "add", "and", "alias", "ascending", "async", "await", "by", "descending", "dynamic", "equals",
        "file", "from", "get", "global", "group", "init", "into", "join", "let", "managed", "nameof",
        "nint", "not", "notnull", "nuint", "on", "or", "orderby", "partial", "record", "remove", "required",
        "scoped", "select", "set", "unmanaged", "value", "var", "when", "where", "with", "yield"
    };

    private static readonly HashSet<string> ObjectMemberNames = new(StringComparer.Ordinal)
    {
        "Equals",
        "GetHashCode",
        "ToString",
        "GetType",
        "Finalize",
        "MemberwiseClone",
        "ReferenceEquals"
    };
}
