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
        if (args.Count < 2)
        {
            Console.WriteLine("Where should I go?");
            return;
        }

        if (currentScene.Links.TryGetValue(args[1].Value, out string? arg) && arg is not null)
        {
            currentScene = scenes[currentScene.Links[args[1].Value]];

            string text = currentScene.InitialText;

            if (currentScene.WasVisited)
            {
                text = currentScene.Name;
            }

            Console.WriteLine(text);

            return;
        }
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

    MatchCollection? matches = Regexes.Command().Matches(input);

    if (actions.TryGetValue(matches[0].Value, out Action<MatchCollection>? action) && action is not null)
    {
        actions[matches[0].Value]?.Invoke(matches);
        continue;
    }

    Console.WriteLine("Come again?");
}
