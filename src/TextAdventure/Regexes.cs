using System.Text.RegularExpressions;

namespace TextAdventure;

public static partial class Regexes
{
    [GeneratedRegex(@"\b(go)(?:\s+(?:to(?:\s+the)?\s+)?(\w+)?)?", RegexOptions.IgnoreCase)]
    public static partial Regex Go();
    [GeneratedRegex(@"\b(inspect)(?:\s+the)?\s+(\w+)?", RegexOptions.IgnoreCase)]
    public static partial Regex Inspect();
}
