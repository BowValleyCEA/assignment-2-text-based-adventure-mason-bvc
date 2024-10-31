using System.Text.RegularExpressions;

namespace TextAdventure;

public static partial class Regexes
{
    // general

    [GeneratedRegex(@"\b(\w+)\b(?:\s+the)?", RegexOptions.IgnoreCase)]
    public static partial Regex Action();
    [GeneratedRegex(@"inspect(\s+the)?", RegexOptions.IgnoreCase)]
    public static partial Regex Inspect();
    [GeneratedRegex(@"\b(?:go\s+back)")]
    public static partial Regex GoBack();
    [GeneratedRegex(@"\b(go)(?:\s+to(?:\s+the)?)?(?:\s+(\w+))?", RegexOptions.IgnoreCase)]
    public static partial Regex Go();
    [GeneratedRegex(@"take(\s+the)?", RegexOptions.IgnoreCase)]
    public static partial Regex Take();
    [GeneratedRegex(@"\b(what)(?:(?:'s|\s+is)(?:\s+(?:in\s+)?here))?")]
    public static partial Regex What();
    [GeneratedRegex(@"\b(where)\b(?:\s+am\s+i)?", RegexOptions.IgnoreCase)]
    public static partial Regex Where();

    // directions

    [GeneratedRegex(@"(n(?:orth)?)", RegexOptions.IgnoreCase)]
    public static partial Regex North();
    [GeneratedRegex(@"(e(?:ast)?)", RegexOptions.IgnoreCase)]
    public static partial Regex East();
    [GeneratedRegex(@"(s(?:outh)?)", RegexOptions.IgnoreCase)]
    public static partial Regex South();
    [GeneratedRegex(@"(w(?:est)?)", RegexOptions.IgnoreCase)]
    public static partial Regex West();
}
