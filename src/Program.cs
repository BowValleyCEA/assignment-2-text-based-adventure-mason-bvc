using System.Text.RegularExpressions;
using TextAdventure;

Dictionary<string, Scene> scenes = new()
{
    ["StartingRoom"] = new()
    {
        Name = "Lavish Room",
        InitialText = "You appear in a lavish room, adorned with red and purple. To the west is a small corridor, to your east is the entrance to the Great Hall.",
        Links = new()
        {
            ["west"] = "Corridor0",
        },
    },
    ["Corridor0"] = new()
    {
        Name = "Corridor",
        InitialText = "You venture down the corridor; wooden casks line its flanks and its end is guarded by a portcullis.",
        Links = new()
        {
            ["east"] = "StartingRoom",
        },
        Things = new()
        {
            ["portcullis"] = new()
            {
                Name = "Portcullis",
                InteractWith = (_, _) =>
                {
                    Console.WriteLine("The portcullis is shut. You try prying it open from the bottom, but it's no use. There is a key hole, however.");
                },
            },
            ["casks"] = new()
            {
                Name = "Casks",
                State = new()
                {
                    ["IsOpened"] = false,
                },
                InteractWith = (Thing self, Thing activator) =>
                {
                    // needed to start a new scope because i repeatedly use the
                    // identifier "value" for out args

                    {if (activator.State.TryGetValue("Inventory", out object? value) && value is Dictionary<string, Thing> inventory && inventory.TryGetValue("Crowbar", out Thing? crowbar))
                    {
                        Console.WriteLine("You descend the flanks, prying open the caskets one-by-one with your crowbar. In one of them, you find a key.");
                        return;
                    }}

                    Console.WriteLine("You try opening the casks with your bare hands, but they appear to be shut.");
                },
            },
        },
    },
    // ["GreatHall"] = new()
    // {
        // InitialText = "You now stand in the Great Hall. A sizable room whose walls are adorned with red and purple flags. Long tables ",
    // },
};

Scene currentScene = scenes["StartingRoom"];
Thing player = new()
{
    Name = "Player",
};

Console.WriteLine(currentScene.InitialText);

while (true)
{
    Console.Write("> ");

    string? input = Console.ReadLine();

    if (input is null)
    {
        continue;
    }

    input = input.Trim();

    // go

    var matchAndGroups = ProcessGroups(Regexes.Go().Match(input));

    if (matchAndGroups.Match.Success)
    {
        if (matchAndGroups.Groups.Length < 3)
        {
            Console.WriteLine("Where should I go?");
            continue;
        }

        if (!currentScene.Links.TryGetValue(matchAndGroups.Groups[^1].Value, out string? linkString))
        {
            Console.WriteLine("I cannot go there.");
            continue;
        }

        currentScene = scenes[linkString];

        string text = currentScene.InitialText;

        if (currentScene.WasVisited)
        {
            text = currentScene.Name;
        }

        Console.WriteLine(text);

        continue;
    }

    // inspect

    matchAndGroups = ProcessGroups(Regexes.Inspect().Match(input));

    if (matchAndGroups.Match.Success)
    {
        if (matchAndGroups.Groups.Length < 2)
        {
            Console.WriteLine("What should I inspect?");
            continue;
        }

        if (!currentScene.Things.TryGetValue(matchAndGroups.Groups[^1].Value, out Thing? thing))
        {
            Console.WriteLine($"I can't find such a thing in here.");
            continue;
        }

        thing?.InteractWith?.Invoke(thing, player);

        continue;
    }

    Console.WriteLine("Come again?");
}

static (Match Match, Group[] Groups) ProcessGroups(Match match)
{
    Group[] groups = new Group[match.Groups.Count];

    match.Groups.CopyTo(groups, 0);
    groups = groups.Where(g => !string.IsNullOrWhiteSpace(g.Value)).ToArray();

    return (match, groups);
}
