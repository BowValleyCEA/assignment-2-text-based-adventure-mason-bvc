[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/X_Tuc_DH)
# A2: Text-Based Adventure

This assignment will test your knowledge and skill of:

- Coding style
- Syntax
- Data types
- Operators
- Functions
- Conditions
- String Manipulation
- Object-Oriented Programming
- Searching for solutions to common problems (Google, manual, script reference, fellow students)

## Description

You are to develop your very own text-based adventure game, much like [Zork](https://textadventures.co.uk/games/play/5zyoqrsugeopel3ffhz_vq), [Spider and Web](https://eblong.com/zarf/zweb/tangle/), or [Night House](https://play2.textadventures.co.uk/Play.aspx?id=7nwoee9ope2_hvfrx8kjvw). You will be creating a game where you can navigate rooms, use objects, and try to find your way through a location of your choice. You are free to come up with whatever theme or world you would like for your adventure; the story is up to you. Classroom link: [https://classroom.github.com/a/X_Tuc_DH](https://classroom.github.com/a/X_Tuc_DH)

## Text Commands

In your text-based adventure, the player is going to enter different commands and get a response. To simplify this, you are going to have text commands of only three different forms:

- **One Word Commands:** for example, `search` `investigate`. Must always be a *verb*
- **Two Word Commands:** for example, `go north`, `open door` `use potion`. Must always be of the form `*<verb> <noun>*`
- **Three Word Commands**: for example, `turn light on` or `turn light off` . These are of the form `<verb> <noun> <state>` where you apply an action to an object to change its state.
- **Four Word Commands:** for example, “open door with key”. These must always be of the form *<[verb]((https://academicguides.waldenu.edu/writingcenter/grammar/prepositions)> <[object](https://ielts.idp.com/canada/prepare/article-grammar-101-subjects-and-objects-in-english)> <[preposition](https://academicguides.waldenu.edu/writingcenter/grammar/prepositions)> <[subject](https://ielts.idp.com/canada/prepare/article-grammar-101-subjects-and-objects-in-english)>.*

It is strongly recommended to convert all entered text commands to either lower case or upper case when doing string comparison. It will be unfair for players if they enter “Open Door” and you meant for them to write “open door”, or vice-versa.

## Rooms

It will make your text-based adventure a lot easier if you have rooms where objects exist. These do not always need to be physical spaces like in a house, but could also be grassy areas, rivers, etc. The idea is that you have rooms and you may have other rooms connected to that room. Your other rooms have to be in (at a minimum) one of four different directions from that room (North, South, East, West). 

*Minimum Requirement: to implement players moving from one room to another using the command* `go <direction>` . You may allow for short hand, as a lot of text adventure games worked through “go n”, “go e”, etc.

## Objects in Rooms

A room may have objects in the room that you can find by `investigate`  or any command of your choosing. These objects could be any thing you would like, but you must come up with a *minimum of 3 distinct object types*. Some examples could be *key, door, light.* These must all be useable in some way.

You must be able to *change the state of at least two of your objects*. For example, if you have a door, it must be able to be opened or closed. If you have a key, you must be able to open it or close it. If you have a light, you must be able to turn it on or off.

## Game Loading

Your game must load at start time. It can be loaded and be serialized using *json.* There is a JSON loader example provided in your project, and you can use it as a base for how you load your game.  If you do not do this, you will have to program all of your creation for this in a function to populate GameData when your game loads.

## Responses to Commands

Your game must respond to commands and give *relevant feedback.* For example, if someone enters `go north` you will move the player north to another room (if that is available) and then give them some sort of answer like `You have entered a new room, the living room. It smells like food has been left out too long. Wax has completely melted from a candle, and it has hardened next to a plate of turkey.`  

## Edge Cases

Your program has to [handle edge cases](https://airfocus.com/glossary/what-is-an-edge-case/). An edge case is a scenario where the user enters undesired input. For example, if someone enters `go to mars` you must be able to handle this and give them a response. For example: `I do not understand "go to mars"` .

# Professionalism

Your **coding style** should match the rules outlined at the end of this document.

The assets in your assignment should be **entirely created by you**. If you copy another person on your assignment, that is an automatic 0, no questions asked, and an Academic Misconduct report will be sent to the school.

# Coding Style

We will use the Microsoft C# coding conventions for our assignment:

[https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

# Rubrics

Testing for:

- Implementation of Game Rules
- Handling of Edge Cases (someone selects incorrect location, does not use a number, etc.)
- Rendering of Gameplay and Display
- User Interaction Functionality
- Code Quality

| Major | Excellent | Good | Okay | Poor | Very Poor |
| --- | --- | --- | --- | --- | --- |
| Processing Commands | Game processes commands, handles edge cases, and provides meaningful feedback. | Game processes commands and handles edge cases, but does not always provide meaningful feedback | Some edge cases may be missed, or some commands do not work, and not all feedback is meaningful. | A significant amount of edge cases do not work, and almost no meaningful feedback is given | Almost no commands are processed correctly and meaningful feedback is never given. |
| Navigation | You can navigate between different rooms with no issues | You can navigate between rooms but may not be able to back track, or you have some issues | Room navigation is sort of working but things like “open door” may not work | Most navigation is an issue with unreliable movement between rooms and spaces | You cannot navigate between different rooms |
| Objects | At least three objects are implemented with state logic all working | At least three objects are implemented with most state logic  working | Some state logic is not working or not all three objects are implemented | At least some objects are implemented but no state logic exists | Objects do not seem to be a part of your game |
| Game Logic | Game logic works allowing for completion of a game using interesting mechanics | Game logic is mainly working and allowing for some interesting mechanics | Game logic has issues and may have some issues with the mechanics | Most game logic is not implemented and mechanics do not work either | No game logic seems to work and no mechanics seem to work. |
| Code Quality | Code is written using C# conventions and is well commented | Code is mainly written with conventions and is well commented | Code is not following some conventions or lacks comments | Code is severely lacking in following conventions and lacks comments | Code is impossible to follow, does not follow conventions, and has no comments |
