using System.Collections.Specialized;

namespace TextAdventure;

public sealed class Scene
{
    public required string Name;
    public required string InitialText;
    public OrderedDictionary Links = [];
    public OrderedDictionary Things = [];
    public bool WasVisited;
}
