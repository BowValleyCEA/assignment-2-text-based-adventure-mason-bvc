using System.Text.RegularExpressions;

namespace TextAdventure;

public static partial class Regexes
{
    [GeneratedRegex(@"\w+")]
    public static partial Regex Command();
    [GeneratedRegex(@"(go)(?:\s+to\s+the)?\s+(n(?:orth)?|e(?:ast)?|s(?:outh)?|w(?:est)?)")]
    public static partial Regex Go();
}
