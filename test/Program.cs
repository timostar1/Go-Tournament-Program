using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTournamentProgram;
using System.Web;
using System.Net;
using System.IO;
using GoTournamentProgram.Model;
using System.Reflection;
using System.ComponentModel;

namespace test
{
    public enum TestEnum
    {
        [Description("!+")]
        X,
        [Description("?")]
        Y,
        [Description("-!")]
        Z
    }
    class Program
    {
        static void Api()
        {
            WebClient Client = new WebClient();
            using (Stream str = Client.OpenRead("http://159.65.182.26/api/xxx"))
            {
                using (StreamReader reader = new StreamReader(str))
                {
                    //using (JsonTextReader json = new JsonTextReader(reader))
                    //{
                    //    JObject jObject = (JObject)JToken.ReadFrom(json);
                    //    Console.WriteLine(jObject["a"]);
                    //}
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
            Sortitions.MakmagonSystem(players, 1);

            TestEnum en = TestEnum.X;
            foreach(var val in Enum.GetNames(typeof(TestEnum)))
            {
                Console.WriteLine(val);
            }
            Type enumType = typeof(TestEnum);
            foreach(string enumName in enumType.GetEnumNames())
            {
                Console.WriteLine(enumName);
                Console.WriteLine(enumType.GetMember(enumName)[0].GetCustomAttribute<DescriptionAttribute>().Description);
            }
        }
    }
}
