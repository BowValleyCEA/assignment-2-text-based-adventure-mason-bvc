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
        InitialText = "You venture down the corridor. Its end is guarded by a portcullis.",
        Links = new()
        {
            ["east"] = "StartingRoom",
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
    },
};

Scene currentScene = scenes["StartingRoom"];

Dictionary<string, Action<MatchCollection>> actions = new()
{
    ["go"] = (MatchCollection args) =>
    {
    },
    ["inspect"] = (MatchCollection args) =>
    {
        if (currentScene.Things.TryGetValue(args[1].Value, out Thing? thing) && thing is not null)
        {
            thing.InteractWith?.Invoke(thing);
        }
    },
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

    MatchCollection? commandMatches = Regexes.Command().Matches(input);

    switch (commandMatches[0].Value)
    {
    case "go":
    {
        Match goMatch = Regexes.Go().Match(input);

        Console.WriteLine(goMatch.Success);/*

        if (currentScene.Links.TryGetValue(goMatches[1].Value, out string? arg) && arg is not null)
        {
            currentScene = scenes[currentScene.Links[goMatches[1].Value]];

            string text = currentScene.InitialText;

            if (currentScene.WasVisited)
            {
                text = currentScene.Name;
            }

            Console.WriteLine(text);
        }*/
    }
    break;
    case "inspect":
    {
    }
    break;
    }

    Console.WriteLine("Come again?");
}
