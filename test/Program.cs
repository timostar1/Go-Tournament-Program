using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTournamentProgram;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GoTournamentProgram.Model;

namespace test
{
    class Program
    {
        static void Api()
        {
            WebClient Client = new WebClient();
            using (Stream str = Client.OpenRead("http://159.65.182.26/api/xxx"))
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    using (JsonTextReader json = new JsonTextReader(reader))
                    {
                        JObject jObject = (JObject)JToken.ReadFrom(json);
                        Console.WriteLine(jObject["a"]);
                    }
                }
            }
        }

        static void TestGame()
        {
        //    Game game = new Game();
        //    Console.WriteLine(game);
        //    game.OpponentId = -1;
        //    Console.WriteLine(game);
        //    game.Result = Game.GameResult.Win;
        //    Console.WriteLine(game);
        //    game.OpponentId = 2;
        //    Console.WriteLine(game);
        //    game.Result = Game.GameResult.NotFinished;
        //    Console.WriteLine(game);
        }
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            Player p;
            p = new Player();
            p.Name = "A";
            p.Surname = "AA";
            p.Rating = 2100;
            p.MMR = 10;
            p.Place = 1;
            p.ID = p.Place;
            p.Games = new List<Game>();
            p.Games.Add(new Game());
            players.Add(p);

            p = new Player();
            p.Name = "B";
            p.Surname = "BB";
            p.Rating = 2000;
            p.MMR = 5;
            p.Place = 2;
            p.ID = p.Place;
            p.Games = new List<Game>();
            p.Games.Add(new Game());
            players.Add(p);

            p = new Player();
            p.Name = "C";
            p.Surname = "CC";
            p.Rating = 1900;
            p.MMR = 5;
            p.Place = 3;
            p.ID = p.Place;
            p.Games = new List<Game>();
            p.Games.Add(new Game());
            players.Add(p);

            p = new Player();
            p.Name = "D";
            p.Surname = "DD";
            p.Rating = 1000;
            p.MMR = 2;
            p.Place = 4;
            p.ID = p.Place;
            p.Games = new List<Game>();
            p.Games.Add(new Game());
            players.Add(p);

            p = new Player();
            p.Name = "E";
            p.Surname = "EE";
            p.Rating = 1000;
            p.MMR = 2;
            p.Place = 5;
            p.ID = p.Place;
            p.Games = new List<Game>();
            p.Games.Add(new Game());
            p.Games[0].Result = GameResult.Absence;
            players.Add(p);

            p = new Player();
            p.Name = "F";
            p.Surname = "FF";
            p.Rating = 1000;
            p.MMR = 2;
            p.Place = 6;
            p.ID = p.Place;
            p.Games = new List<Game>();
            p.Games.Add(new Game());
            players.Add(p);



            Sortitions.MakmagonSystem(players, 1);
        }
    }
}
