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
            Game game = new Game();
            Console.WriteLine(game);
            game.OpponentId = -1;
            Console.WriteLine(game);
            game.Result = Game.GameResult.Win;
            Console.WriteLine(game);
            game.OpponentId = 2;
            Console.WriteLine(game);
            game.Result = Game.GameResult.NotFinished;
            Console.WriteLine(game);
        }
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < 7; i++)
            {
                players.Add(new Player());
            }
            for (int i = 0; i < 10; i++)
            {
                Tournament tournament = new Tournament(players, TournamentSettings.TournamentSystem.Circle);
                //Console.WriteLine(players[0].TournamentSettings.Games.Count);
                tournament.MakeSortition();
                List<Player> pl = tournament.Players;
                //tournament.AddGameResult(1, '+');
                foreach (Player player in pl)
                {
                    Console.Write($"{player.ID}  ");
                    for (int tour = 1; tour <= player.Games.Count; tour++)
                    {

                        Game game = player.Games[tour];
                        Console.Write($"{game} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
