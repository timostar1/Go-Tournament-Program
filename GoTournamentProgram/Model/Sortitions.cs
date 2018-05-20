using System;
using System.Collections.Generic;
using System.Linq;

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
                }
            }

            return players;
        }

        private static bool CanPairPlay(Player p1, Player p2, int tour)
        {
            if (p1.Games[tour - 1].OpponentId != -1 || p2.Games[tour - 1].OpponentId != -1)
            {
                return false;
            }

            int id1 = p1.ID;
            foreach (Game g in p2.Games)
            {
                if (g.OpponentId == id1) return false;
            }
            int id2 = p2.ID;
            foreach (Game g in p1.Games)
            {
                if (g.OpponentId == id2) return false;
            }
            
            return true;
        }

        private static bool TrySortition(ref List<Player> group, int tour)
        {
            int n = group.Count;
            if (n == 0)
            {
                return false;
            }

            List<Player> subgroup1 = new List<Player>();
            List<Player> subgroup2 = new List<Player>();

            // распределяем участников на две подгруппы
            for (int i = 0; i < n; i++)
            {
                if (i < n / 2) subgroup1.Add(group[i]);
                else subgroup2.Add(group[i]);
            }

            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    if (CanPairPlay(subgroup1[i], subgroup2[j], tour))
                    {
                        subgroup1[i].Games[tour - 1].OpponentId = subgroup2[j].ID;
                        subgroup1[i].Games[tour - 1].Opponent = subgroup2[j].Place;
                        subgroup1[i].Games[tour - 1].Result = GameResult.NotFinished;

                        subgroup2[j].Games[tour - 1].OpponentId = subgroup1[i].ID;
                        subgroup2[j].Games[tour - 1].Opponent = subgroup1[i].Place;
                        subgroup2[j].Games[tour - 1].Result = GameResult.NotFinished;
                        break;
                    }
                }
            }

            // Проверяем, все ли игроки разбиты на пары
            foreach (Player p in subgroup1)
            {
                if (p.Games[tour - 1].OpponentId == -1) return false;
            }

            group.Clear();
            group.AddRange(subgroup1);
            group.AddRange(subgroup2);
            return true;
        }

        public static List<Player> MakmagonSystem(List<Player> allPlayers, int tour)
        {
            // список игроков, участвующих в туре
            List<Player> players = new List<Player>();
            List<Player> skippers = new List<Player>();
            foreach (Player p in allPlayers)
            {
                if (p.Games[tour - 1].Result != GameResult.Absence)
                {
                    players.Add(p);
                }
                else skippers.Add(p);
            }

            List<List<Player>> groups = new List<List<Player>>();
            double points = -1;
            while (true)
            {
                points = -1;
                List<Player> group = new List<Player>();
                // Выделяем отдельную группу (по турнирным очкам)
                foreach (Player p in players)
                {
                    if (points == -1)
                    {
                        points = p.Points;
                    }
                    if (p.Points == points)
                    {
                        group.Add(p);
                    }
                    else break;
                }
                foreach (Player p in group)
                {
                    players.Remove(p);
                }

                // корректируем четность количества участников в группе
                if (group.Count % 2 == 1)
                {
                    players.Insert(0, group.Last());
                    group.Remove(group.Last());
                }

                // пытаемся распределить игроков по парам в группе
                while (!TrySortition(ref group, tour))
                {
                    try
                    {
                        group.Add(players.First());
                        players.Remove(players.First());
                        group.Add(players.First());
                        players.Remove(players.First());
                    }
                    catch
                    {
                        // Случай, когда неозможн провести жеребьевку
                        return null;
                    }
                }

                groups.Add(group);
                if (players.Count == 0) break;
                else if (players.Count == 1)
                {
                    players[0].Games[tour - 1].Result = GameResult.Absence;
                    players[0].Points += 1;
                    groups.Add(players);
                    break;
                }
            }

            players = new List<Player>();

            // Тестовый вывод (потом удалить)
            //Console.WriteLine(groups.Count);
            foreach (List<Player> group in groups)
            {
                //Console.WriteLine("group:");
                foreach (Player p in group)
                {
                    //Console.WriteLine(p);
                    players.Add(p);
                }
                //Console.WriteLine();
            }

            foreach (Player p in skippers)
            {
                players.Add(p);
            }

            players.Sort();
            return players;
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
