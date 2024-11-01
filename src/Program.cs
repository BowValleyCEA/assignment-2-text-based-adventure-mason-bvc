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
            [new Regex(@"key\s+hole", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Key Hole",
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing _, Thing _) =>
                    {
                        Console.WriteLine("The portcullis' key hole. Now if you could just find the key...");
                    },
                }
            },
            [new Regex("portcullis", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Portcullis",
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing.ActionArgs _) =>
                    {
                        Console.WriteLine("The portcullis is shut. You try prying it open from the bottom, but it's no use. There is a key hole, however.");
                    },
                    [new Regex(@"\bopen\s+(the)?", RegexOptions.IgnoreCase)] = (Thing.ActionArgs args) =>
                    {
                        if (args.Activator.State.TryGetValue("HasPortcullisKey", out object? o) && o is true)
                        {
                            Console.WriteLine("You escaped! Thanks for playing!");
                            args.Activator.State["Done"] = true;
                            return;
                        }

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
                    [Regexes.Inspect()] = (Thing.ActionArgs args) =>
                    {
                        // needed to start a new scope because i repeatedly use the
                        // identifier "value" for out args

                        {
                            if (args.Activator.State.TryGetValue("Inventory", out object? value) && value is Dictionary<string, Thing> inventory && inventory.ContainsKey("Crowbar"))
                            {
                                Console.WriteLine("You descend the flanks, prying open the caskets one-by-one with your crowbar. In one of them, you find a key.");
                                return;
                            }
                        }

                        if (args.Activator.State.TryGetValue("HasCrowbar", out object? o) && o is true)
                        {
                            Console.WriteLine("You use the crowbar you acquired from the Salle Haute to pry open the casks one-by-one. In one you find the portcullis key!");
                            args.Activator.State["HasPortcullisKey"] = true;
                            return;
                        }

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
            [new Regex(@"table((\s+)?top)?(s)?", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Tables",
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing.ActionArgs _) =>
                    {
                        Console.WriteLine("Upon one of the tabletops lies an ornate-looking rock enthusiastically pinning one of the napkins to the table. It's roughly the size of your head and thrice the weight.");
                    },
                },
            },
            [new Regex("window", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Window",
                State = new()
                {
                    ["Shattered"] = false,
                },
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing.ActionArgs args) =>
                    {
                        Console.WriteLine("A heavily-tinted window. You can just make out a crowbar-looking shape in the room beyond it.");
                    },
                    [new Regex(@"\b(go|jump)\s+through")] = (Thing.ActionArgs args) =>
                    {
                        if (args.Self.State.TryGetValue("Shattered", out object? o) && o is bool shattered)
                        {
                            Console.WriteLine("You quickly hop into the room behind what used to be the window. You grab the crowbar off a workbench and promptly come back out.");
                            args.Activator.State["HasCrowbar"] = true;

                            return;
                        }

                        Console.WriteLine("You feel apprehensive about barreling through the window. You hate cuts and scrapes!");
                    },
                },
            },
            [new Regex("rock", RegexOptions.IgnoreCase)] = new Thing()
            {
                Name = "Rock",
                IsHidden = true,
                CanPickUp = true,
                Actions = new()
                {
                    [Regexes.Inspect()] = (Thing.ActionArgs args) =>
                    {
                        args.Self.IsHidden = false;
                        Console.WriteLine("It's a nice heavy rock. You bet it would be a lot of fun if you chucked it into something breakable.");
                    },
                    [new Regex(@"\bthrow\s+(?:the\s+)?rock\s+(at)?\s+(?:the\s+)?(\w+)")] = (Thing.ActionArgs args) =>
                    {
                        string victim = args.Command[^1];

                        if (victim.Equals("window", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (args.Self.Query is not null)
                            {
                                if (args.Self.Query.TryGetValue("Window", out Thing? window) && window is not null)
                                {
                                    window.State["Shattered"] = true;
                                    Console.WriteLine($"You threw the rock at the window, shattering it into countless, brilliant shards.");
                                }
                            }

                            return;
                        }
                    },
                },
            },
        },
    }
};

// ugh
foreach (Scene scene in scenes.Values)
{
    Dictionary<string, Thing> q = [];

    foreach (DictionaryEntry kvp in scene.Things)
    {
        Thing? thing = (Thing?)kvp.Value;

        if (thing is not null)
        {
            q[thing.Name] = thing;
            thing.Query = q;
        }
    }
}

Stack<Scene> breadcrumbs = [];
Scene currentScene = scenes["StartingRoom"];
Thing player = new()
{
    Name = "Player",
};

player.State["Inventory"] = new Dictionary<string, Thing>();
GoInto(scenes["StartingRoom"]);
breadcrumbs.Pop();

while (!(player.State.TryGetValue("Done", out object? o) && o is true))
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

        GoInto(targetScene, false);

        continue;
    }

    // go

    var matchAndGroups = ProcessGroups(Regexes.Go().Match(input));

    if (matchAndGroups.Match.Success)
    {
        if (matchAndGroups.Groups.Length < 3)
        {
            Console.WriteLine("Where should you go?");
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

        Console.WriteLine($"\"{matchAndGroups.Groups[^1]}\" is not a place you can go to.");

        continue;
    }

    // where

    matchAndGroups = ProcessGroups(Regexes.Where().Match(input));

    if (matchAndGroups.Match.Success)
    {
        Console.WriteLine(currentScene.InitialText);
        continue;
    }

    // what

    matchAndGroups = ProcessGroups(Regexes.What().Match(input));

    if (matchAndGroups.Match.Success)
    {
        if (currentScene.Things.Count <= 0)
        {
            Console.WriteLine("There's nothing of note here.");
            continue;
        }

        Console.WriteLine("What you can immediately notice in this room:");

        foreach (DictionaryEntry kvp in currentScene.Things)
        {
            Thing? t = (Thing?)kvp.Value;

            if (t is not null && !t.IsHidden)
            {
                Console.WriteLine($"    {t.Name}");
            }
        }

        continue;
    }

    // other actions

    MatchCollection splits = new Regex(@"(\w+)").Matches(input);
    Match? thingMatch = splits[^1];
    Thing? thing = null;

    foreach (DictionaryEntry kvp in currentScene.Things)
    {
        Regex re = (Regex)kvp.Key;

        if (re.IsMatch(input))
        {
            thing = (Thing?)kvp.Value;
        }
    }

    if (thing is not null)
    {
        Action<Thing.ActionArgs>? action = null;

        foreach (DictionaryEntry kvp in thing.Actions)
        {
            Regex re = (Regex)kvp.Key;

            if (re.IsMatch(input))
            {
                action = (Action<Thing.ActionArgs>?)kvp.Value;
                break;
            }
        }

        if (action is not null)
        {
            var actionArgs = new Thing.ActionArgs
            {
                Self = thing,
                Activator = player,
                Command = ProcessGroups(thingMatch).Groups,
            };

            action.Invoke(actionArgs);
            continue;
        }

        Console.WriteLine($"You cannot \"{splits[0].Value}\" the {thing.Name}.");

        continue;
    }

    Console.WriteLine("Come again?");
}

void GoInto(Scene targetScene, bool shouldPushIntoBreadcrumbs = true)
{
    if (shouldPushIntoBreadcrumbs)
    {
        breadcrumbs.Push(currentScene);
    }

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
