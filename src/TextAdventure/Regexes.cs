using System.Text.RegularExpressions;

namespace TextAdventure;

public static partial class Regexes
{
    // general

    [GeneratedRegex(@"\b(\w+)\b(?:\s+the)?", RegexOptions.IgnoreCase)]
    public static partial Regex Action();
    [GeneratedRegex(@"^inspect(\s+the)?", RegexOptions.IgnoreCase)]
    public static partial Regex Inspect();
    [GeneratedRegex(@"\b(?:go\s+back)|\b(back)", RegexOptions.IgnoreCase)]
    public static partial Regex GoBack();
    [GeneratedRegex(@"\b(go)(?:\s+to(?:\s+the)?)?(?:\s+(\w+))?", RegexOptions.IgnoreCase)]
    public static partial Regex Go();
    [GeneratedRegex(@"\b(take|pick\s+up)(\s+the)?", RegexOptions.IgnoreCase)]
    public static partial Regex Take();
    [GeneratedRegex(@"\b(what)(?:(?:'s|\s+is)\s+(?:in\s+)?here)?$", RegexOptions.IgnoreCase)]
    public static partial Regex What();
    [GeneratedRegex(@"\b(where)\b(?:\s+am\s+i)?", RegexOptions.IgnoreCase)]
    public static partial Regex Where();

    // directions

    [GeneratedRegex(@"\b(north|n)", RegexOptions.IgnoreCase)]
    public static partial Regex North();
    [GeneratedRegex(@"\b(east|e)", RegexOptions.IgnoreCase)]
    public static partial Regex East();
    [GeneratedRegex(@"\b(south|s)", RegexOptions.IgnoreCase)]
    public static partial Regex South();
    [GeneratedRegex(@"\b(west|w)", RegexOptions.IgnoreCase)]
    public static partial Regex West();
}
