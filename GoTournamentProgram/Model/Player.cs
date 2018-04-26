using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public class Player:Person
    {
        public int ID { get; set; }
        public string Club { get; set; }
        public string City { get; set; }
        public int Rating { get; set; }
        public PlayerTournamentSettings TournamentSettings { get; set; }

        /// <summary>
        /// Словарь (Номер тура: Объект игры)
        /// </summary>
        public Dictionary<int, Game> Games { get; }
        public Player()
        {
            this.ID = -1;
            this.BirthDate = DateTime.Now;
            this.City = "-";
            this.Club = "-";
            this.gender = Gender.Male;
            this.Rating = 0;
            this.Name = "-";
            this.Surname = "-";
            this.TournamentSettings = new PlayerTournamentSettings();
            this.Games = new Dictionary<int, Game>();
        }

        public void AddGameResult(int tour, Game game)
        {
            this.Games[tour] = game;
        }
        public void AddGameResult(int tour,int opponentId, Game.GameResult result)
        {
            this.Games[tour] = new Game(opponentId, result);
        }
        public void AddGameResult(int tour, int opponentId)
        {
            this.Games[tour] = new Game(opponentId);
        }
        public void AddGameResult(int tour)
        {
            this.Games[tour] = new Game();
        }
    }
}
