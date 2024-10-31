namespace TextAdventure;

public sealed record class Thing
{
    public required string Name;
    public Dictionary<string, object?> State = [];
    public Action<Thing, Thing>? InteractWith;
}
