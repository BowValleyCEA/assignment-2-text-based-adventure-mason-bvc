using System.Collections;
using System.Text.RegularExpressions;
using Mason.Extensions;
using TextAdventure;

Dictionary<string, Scene> scenes = new()
{
    ["StartingRoom"] = new()
    {
        Name = "Lavish Room",
        InitialText = "You appear in a lavish room, adorned with red and purple. To the west is a small corridor, to your east is the entrance to the Great Hall.",
        Links = new()
        {
            [Regexes.East()] = "GreatHall",
            [Regexes.West()] = "Corridor0",
        },
    },
    ["Corridor0"] = new()
    {
        Name = "Corridor",
        InitialText = "You venture down the corridor; wooden casks line its flanks and its end is guarded by a portcullis.",
        Links = new()
        {
            [Regexes.East()] = "StartingRoom",
        },
        Things = new()
        {
            [new Regex("portcullis", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Portcullis",
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing _, Thing _) =>
                    {
                        Console.WriteLine("The portcullis is shut. You try prying it open from the bottom, but it's no use. There is a key hole, however.");
                    },
                },
            },
            [new Regex(@"(?:wood(?:en)?\s+)?(cask(?:s)?)", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Casks",
                State = new()
                {
                    ["IsOpened"] = false,
                },
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing self, Thing activator) =>
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
                }
            },
        },
    },
    ["GreatHall"] = new()
    {
        Name = "Great Hall",
        InitialText = "You now stand in the Great Hall. A sizable room whose walls are lined with red and purple flags and striated by long tables. At the end of the hall to your north, there is a staircase leading to the salle haute.",
        Links = new()
        {
            [Regexes.North()] = "SalleHaute",
        },
    },
    ["SalleHaute"] = new()
    {
        Name = "Salle Haute",
        InitialText = "You now stand in the Salle Haute; a relatively intimate dwelling spotted with round tables. On the east side of the room is a window and a door.",
        Things = new()
        {
            [new Regex(@"table(s)?", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Tables",
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing self, Thing activator) =>
                    {
                        Console.WriteLine("Upon one of the tabletops lies an ornate-looking rock enthusiastically pinning one of the napkins to the table. It's roughly the size of your head and thrice the weight.");
                    },
                },
            },
            [new Regex("rock", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Rock",
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing self, Thing activator) =>
                    {
                        Console.WriteLine("It's a nice heavy rock. I bet it would be a lot of fun if you chucked it into something breakable.");
                    },
                    [Regexes.Take()] = (Thing self, Thing taker) =>
                    {
                    },
                },
            },
        },
    }
};

Stack<Scene> breadcrumbs = [];
Scene currentScene = scenes["StartingRoom"];
Thing player = new()
{
    Name = "Player",
};

GoInto(scenes["StartingRoom"]);
breadcrumbs.Pop();

while (true)
{
    Console.Write("> ");

    string? input = Console.ReadLine();

    if (input is null)
    {
        continue;
    }

    input = input.Trim().ToLower();

    // go back

    if (Regexes.GoBack().IsMatch(input))
    {
        if (!breadcrumbs.TryPop(out Scene? targetScene))
        {
            Console.WriteLine("There is nowhere to go back to!");
            continue;
        }

        GoInto(targetScene);

        continue;
    }

    // go

    var matchAndGroups = ProcessGroups(Regexes.Go().Match(input));

    if (matchAndGroups.Match.Success)
    {
        if (matchAndGroups.Groups.Length < 3)
        {
            Console.WriteLine("Where should I go?");
            continue;
        }

        bool success = false;

        foreach (DictionaryEntry kvp in currentScene.Links)
        {
            Regex re = (Regex)kvp.Key;
            string targetSceneReference = (string)kvp.Value!;

            if (re.IsMatch(matchAndGroups.Groups[^1]) && scenes.TryGetValue(targetSceneReference, out Scene? targetScene))
            {
                GoInto(targetScene);
                success = true;
                break;
            }
        }

        if (success)
        {
            continue;
        }

        Console.WriteLine($"\"{matchAndGroups.Groups[^1]}\" is not a place I can go into.");
        continue;
    }

    Console.WriteLine("Come again?");
}

void GoInto(Scene targetScene)
{
    breadcrumbs.Push(currentScene);
    currentScene = targetScene;

    string text = currentScene.InitialText;

    if (currentScene.WasVisited)
    {
        text = currentScene.Name;
    }

    currentScene.WasVisited = true;

    Console.WriteLine(text);
}

static (Match Match, string[] Groups) ProcessGroups(Match match)
{
    Group[] groups = new Group[match.Groups.Count];
    match.Groups.CopyTo(groups, 0);
    return (match, groups.Where(g => !string.IsNullOrWhiteSpace(g.Value)).Select(g => g.Value.Trim().ToLower()).ToArray());
}
