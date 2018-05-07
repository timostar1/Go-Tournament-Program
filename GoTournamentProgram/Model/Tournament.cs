using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public class Tournament: ITournament, INotifyPropertyChanged
    {
        /// <summary>
        /// Реализация интерфейса INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public List<Player> Players { get; set; }
        public string Name { get; set; }
        public int CurrentTour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TournamentSettings Settings { get; set; }
        //public delegate bool PlayersComparer(Player p);
        //public PlayersComparer ComparePlayerId = delegate (Player p) { return p.TournamentSettings.Id == playerId; }
        public Tournament()
        {
            this.Settings = new TournamentSettings();
            this.Players = new List<Player>();
            this.Name = "New tournament";
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now.AddDays(1);
            this.CurrentTour = 0;
        }

        public Tournament(List<Player> players, TournamentSettings.TournamentSystem system)
        {
            this.Settings = new TournamentSettings();
            this.Settings.System = system;
            this.Players = players;
            // Задаем id участникам турнира
            for (int i = 0; i < players.Count; i++)
            {
                this.Players[i].ID = i + 1;
            }
        }

        public void NextTour()
        {
            // TODO
        }
        
        public void AddPlayer(Player player)
        {
            for (int i = 1; i <= this.CurrentTour; i++)
            {
                //player.AddGameResult(i);
            }
        }

        public void AddGameResult(int playerId, char result)
        {
            // FIXME
            int playerIndex = this.Players.FindIndex(
                delegate (Player p) { return p.ID == playerId; });
            if (playerIndex == -1)
            {
                throw new ArgumentOutOfRangeException("playerId", "Игрока с таким индексом не существует");
            }
            switch (result)
            {
                case '+':
                    //int opponentId = this.Players[playerIndex].TournamentSettings.Games.ElementAt(this.CurrentTour).Key;
                    //this.Players[playerIndex].TournamentSettings.Games[opponentId] = (PlayerTournamentSettings.GameResult)result;
                    //int opponentIndex = this.Players.FindIndex(
                    //    delegate (Player p) { return p.TournamentSettings.Id == opponentId; });
                    //this.Players[opponentIndex].TournamentSettings.Games[playerId] = PlayerTournamentSettings.GameResult.Defeat;
                    break;
                case '-': break;
                case '=': break;
                case '?': break;
                case '!': break;
                case '*': break;
                case '~': break;
            }
        }

        public void ChangeGameResult()
        {
            // TODO
        }

        public void MakeSortition()
        {
            switch (this.Settings.System)
            {
                case TournamentSettings.TournamentSystem.Circle:
                    this.Players = Sortitions.CircleSystem(this.Players);
                    break;
                case TournamentSettings.TournamentSystem.MakMagon:
                    // TODO
                    break;
            }
        }
    }
}