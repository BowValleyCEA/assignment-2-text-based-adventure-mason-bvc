using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace game1402_a2_starter
{
    public class GameData
    {
        public string GameName { get; set; } //This is an example of a property; for whatever reason your serializable data objects will need to be written this way
        public string Description { get; set; }
        public List<Room> Rooms { get; set; } //this is only an example. You do not ha
        
      

    }

    public class Game(GameData data)
    {
        private GameData _gameData = data;

        public void ProcessString(string enteredString)
        {
            enteredString =
                enteredString.Trim()
                    .ToLower(); //trim any white space from the beginning or end of string and convert to lower case
            string[]
                commands = enteredString
                    .Split(" "); //split based on spaces. The length of this array will tell you whether you have 1, 2, 3, 4, or more commands.
            //modify these functions however you want, but this is where the beginning of calling functions to handle where you are
            string
                response =
                    "Default response"; //you will always do something when processing the string and then give a response
            Console.WriteLine(response); //what you tell the person after what they entered has been processed
        }

    }

}
