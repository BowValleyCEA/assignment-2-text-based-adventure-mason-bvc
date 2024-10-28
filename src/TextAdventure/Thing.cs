namespace TextAdventure;

public sealed record class Thing
{
    public required string Name;
    public Action<Thing>? InteractWith;
}
