using System.Text.RegularExpressions;

Scene? startingRoom = null;
Scene? corridor0 = null;

startingRoom = new Scene()
{
    Name = "Lavish Room",
    InitialText = "You appear in a lavish room, adorned with red and purple. To the west is a small corridor, to your east is the entrance to the Great Hall.",
    Links = new()
    {
        ["west"] = corridor0,
    },
};

corridor0 = new Scene()
{
    Name = "Corridor",
    InitialText = "You venture down the corridor. Its end is guarded by a portcullis.",
    Links = new()
    {
        ["east"] = startingRoom,
    },
    Things = new()
    {
        ["portcullis"] = new()
        {
            Name = "Portcullis",
            InteractWith = (_) =>
            {
                Console.WriteLine("The portcullis is shut. You try prying it open from the bottom, but it's no use.");
            },
        },
    },
};

Scene currentScene = startingRoom;

Dictionary<string, Action<string>> actions = new()
{
    ["go"] = (string where) =>
    {
        if (currentScene.Links.TryGetValue(where, out Scene? value) && value is not null)
        {
            currentScene = value;
        }
    },
};

while (true)
{
    Console.WriteLine(currentScene.InitialText);
    Console.Write("> ");

    string? input = Console.ReadLine();

    if (input is null)
    {
        continue;
    }

    Match match = Regexes.Go().Match(input);

    if (!match.Success)
    {
        Console.WriteLine("Come again?");
        continue;
    }

    // actions[match.Captures[1].Value]?.Invoke(match.Captures[2].Value);
    foreach (var capture in match.Captures)
    {
        Console.WriteLine(capture);
    }
}

class Scene
{
    public required string Name;
    public required string InitialText;
    public Dictionary<string, Scene?> Links = [];
    public Dictionary<string, Thing?> Things = [];
}

record class Thing
{
    public required string Name;
    public Action<Thing>? InteractWith;
}

static partial class Regexes
{
    [GeneratedRegex(@"(?i)\b(go)\s+(north|east|south|west|back)", RegexOptions.None, "en-CA")]
    public static partial Regex Go();
}
