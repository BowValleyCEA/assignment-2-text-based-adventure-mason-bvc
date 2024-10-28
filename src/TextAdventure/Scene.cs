namespace TextAdventure;

public sealed class Scene
{
    public required string Name;
    public required string InitialText;
    public Dictionary<string, string> Links = [];
    public Dictionary<string, Thing> Things = [];
    public bool WasVisited;
}
