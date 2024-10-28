using System.Text.RegularExpressions;

namespace TextAdventure;

public static partial class Regexes
{
    [GeneratedRegex(@"(\w+)+", RegexOptions.None)]
    public static partial Regex Command();
}
