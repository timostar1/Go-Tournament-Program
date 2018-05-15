using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public static class Sortitions
    {
        private static Random rnd = new Random();
        public static List<Player> CircleSystem(List<Player> players)
        {
            int n = players.Count;
            List<int> numbers = new List<int>();
            for (int i = 1; i <= n; i++) { numbers.Add(i); }

            Dictionary<int, int> circleDict = new Dictionary<int, int>();
            circleDict.Add(0, -1);
            for (int i = 1; i <= n; i++)
            {
                int randNum = rnd.Next(0, n - i + 1);
                circleDict.Add(numbers[randNum], i);
                numbers.RemoveAt(randNum);
            }

            List<int> circle = new List<int>();
            for (int i = 2; i <= n; i++) { circle.Add(i); }
            if (n % 2 == 1) { circle.Add(0); }


            for (int round = 1; round < n + n % 2; round++)
            {
                // Сопоставль пары игроков
                circle.Insert(0, 1);
                List<int[]> pairs = new List<int[]>();
                for (int k = 0; k < (n - 1) / 2 + 1; k++)
                {
                    int[] pair = { circle[k], circle[k + (n - 1) / 2 + 1] };
                    pairs.Add(pair);
                }

                // Сдвиг круга на 1 шаг
                circle.RemoveAt(0);
                int x = circle[n/2];
                circle.RemoveAt(n/2);
                circle.Insert(0, x);
                x = circle[n / 2];
                circle.RemoveAt(n / 2);
                circle.Add(x);

                foreach (int[] pair in pairs)
                {
                    int p1 = circleDict[pair[0]];
                    int p2 = circleDict[pair[1]];
                    if (p1 == -1)
                    {
                        players[p2 - 1].Games[round - 1].Result = GameResult.Absence;
                    }
                    else if (p2 == -1)
                    {
                        players[p1 - 1].Games[round - 1].Result = GameResult.Absence;
                    }
                    else
                    {
                        players[p2 - 1].Games[round - 1].Result = GameResult.NotFinished;
                        players[p2 - 1].Games[round - 1].Opponent = players[p1 - 1].Place;
                        players[p2 - 1].Games[round - 1].OpponentId = players[p1 - 1].ID;
                        players[p1 - 1].Games[round - 1].Result = GameResult.NotFinished;
                        players[p1 - 1].Games[round - 1].Opponent = players[p2 - 1].Place;
                        players[p1 - 1].Games[round - 1].OpponentId = players[p2 - 1].ID;
                    }
                    //Console.WriteLine($"{p[0]} ({circleDict[p[0]]}) - {p[1]} ({circleDict[p[1]]})");
                }
            }

            return players;
        }

        public static List<Player> MakmagonSystem(List<Player> players)
        {
            return null;
        }

        public static List<Player> SwizSystem(List<Player> players)
        {
            return null;
        }

        public static List<Player> OlympicSystem(List<Player> players)
        {
            return null;
        }

        public static int FindPlayerIndex(List<Player> players, int id)
        {
            int index =  players.FindIndex(p => 
            {
                if (p.ID == id)
                {
                    return true;
                }
                else return false;
            });
            return index;
        }
    }
}
