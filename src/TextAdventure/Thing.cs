using System.Collections.Specialized;

namespace TextAdventure;

public sealed record class Thing
{
    public required string Name;
    public Dictionary<string, object?> State = [];
    public OrderedDictionary Actions = [];
}
