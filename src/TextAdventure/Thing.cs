using System.Collections.Specialized;

namespace TextAdventure;

public sealed record class Thing
{
    public struct ActionArgs()
    {
        public required Thing Self;
        public required Thing Activator;
        public required string[] Command;
    }

    public required string Name;
    public bool IsHidden = false;
    public bool CanPickUp = false;
    public Dictionary<string, object?> State = [];
    public OrderedDictionary Actions = [];
    public OrderedDictionary? Query = null;
}
