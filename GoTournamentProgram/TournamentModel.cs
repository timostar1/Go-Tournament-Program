using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTournamentProgram.Model;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GoTournamentProgram.Services;
using System.Net;

namespace GoTournamentProgram
{
    /// <summary>
    /// Варианты системы жеребьевки турнира
    /// </summary>
    public enum TournamentSystem
    {
        MakMagon,
        Circle,
    }

    public enum GameResult
    {
        NotFinished = '?',
        Win = '+',
        Defeat = '-',
        Draw = '=',
        Absence = '!',
        WinByAbsence = '*',
        DefeatByAbsence = '~',
    }

    [DataContract]
    public class TournamentModel: Notifier
    {
        // TODO: свойство
        private bool isFinished = false;

        /// <summary>
        /// Список всех игроков
        /// </summary>
        private List<Player> allPlayers;
        /// <summary>
        /// Публичный список имен всех игроков (для поиска)
        /// </summary>
        public List<string> AllPlayers { get; private set; }

        private readonly ObservableCollection<Player> players = new ObservableCollection<Player>();
        /// <summary>
        /// Список участников турнира
        /// </summary>
        [DataMember]
        public readonly ReadOnlyObservableCollection<Player> Players;

        private readonly ObservableCollection<Judge> judges = new ObservableCollection<Judge>();
        /// <summary>
        /// Список судей турнира
        /// </summary>
        // TODO: [DataMember]
        public readonly ReadOnlyObservableCollection<Judge> Judges;

        /// <summary>
        /// Название турнира
        /// </summary>
        private string name;
        /// <summary>
        /// Получает или задает название турнира
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Система проведения жеребьевки турнира
        /// </summary>
        private TournamentSystem system;
        /// <summary>
        /// Получает или задает систему проведения жеребьевки турнира
        /// </summary>
        [DataMember]
        public TournamentSystem System
        {
            get { return this.system; }
            private set
            {
                this.system = value;
                OnPropertyChanged("System");
            }
        }

        public void SetSystem(TournamentSystem system)
        {
            if (system != TournamentSystem.Circle)
            {
                this.IsNumberOfToursEnabled = true;
            }
            else
            {
                SetNumberOfTours(this.players.Count + this.players.Count % 2 - 1);
                this.IsNumberOfToursEnabled = false;
            }
            this.System = system;
        }

        /// <summary>
        /// Организатор турнира
        /// </summary>
        private string organizer;
        /// <summary>
        /// Получает или задает название организатора турнира
        /// </summary>
        [DataMember]
        public string Organizer
        {
            get { return this.organizer; }
            set
            {
                this.organizer = value;
                OnPropertyChanged("Organizer");
            }
        }

        /// <summary>
        /// Дата начала турнира
        /// </summary>
        private DateTime startDate;
        /// <summary>
        /// Получает или задает дату начала турнира
        /// </summary>
        [DataMember]
        public DateTime StartDate
        {
            get { return this.startDate; }
            set
            {
                this.startDate = value;
                OnPropertyChanged("StartDate");
            }

        }

        /// <summary>
        /// Дата окончания турнира
        /// </summary>
        private DateTime endDate;
        /// <summary>
        /// Получает или задает дату начала турнира
        /// </summary>
        [DataMember]
        public DateTime EndDate
        {
            get { return this.endDate; }
            set
            {
                if (value >= StartDate)
                {
                    this.endDate = value;
                    OnPropertyChanged("EndDate");
                }
                else throw new ArgumentException("Wrong end date!");
            }
        }

        /// <summary>
        /// Текущий тур
        /// </summary>
        private int currentTour;
        /// <summary>
        /// Получает или задает текущий тур
        /// </summary>
        [DataMember]
        public int CurrentTour
        {
            get { return this.currentTour; }
            set
            {
                this.currentTour = value;
                OnPropertyChanged("CurrentTour");
            }
        }

        /// <summary>
        /// Количество туров втурнире
        /// </summary>
        private int numberOfTours;
        /// <summary>
        /// Получает или задает количество туров в турнире
        /// </summary>
        [DataMember]  // not tested
        public int NumberOfTours
        {
            get { return this.numberOfTours; }
            private set
            {
                this.numberOfTours = value;
                OnPropertyChanged("NumberOfTours");
            }
        }

        public void SetNumberOfTours(int number)
        {
            List<Player> newPlayers = players.ToList();
            players.Clear();
            try
            {
                foreach (Player p in newPlayers)
                {
                    List<Game> games = new List<Game>();
                    for (int i = 0; i < number; i++)
                    {
                        if (i < NumberOfTours)
                        {
                            games.Add(p.Games[i]);
                        }
                        else
                        {
                            games.Add(new Game());
                        }
                    }
                    p.Games = games;
                    players.Add(p);
                }
            }
            catch { }
            
            NumberOfTours = number;
        }

        private bool isNumberOfToursEnabled;
        [DataMember]
        public bool IsNumberOfToursEnabled
        {
            get { return this.isNumberOfToursEnabled; }
            set
            {
                this.isNumberOfToursEnabled = value;
                OnPropertyChanged("IsNumberOfToursEnabled");
            }
        }

        public TournamentModel()
        {
            Players = new ReadOnlyObservableCollection<Player>(players);
            Judges = new ReadOnlyObservableCollection<Judge>(judges);
            name = "new tournament";
            system = TournamentSystem.Circle;
            organizer = "Moscow Go Federation";
            startDate = DateTime.Now;
            endDate = DateTime.Now.AddDays(1);
            numberOfTours = 0;
            isNumberOfToursEnabled = false;
            currentTour = 0;

            LoadAllPlayers();
        }

        public void Update(TournamentModel model)
        {
            this.players.Clear();
            foreach (Player p in model.Players)
            {
                this.players.Add(p);
            }

            Name = model.Name;
            System = model.System;
            Organizer = model.Organizer;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            CurrentTour = model.CurrentTour;
            NumberOfTours = model.NumberOfTours;
            IsNumberOfToursEnabled = model.IsNumberOfToursEnabled;
        }

        private void SortPlayers(List<Player> players)
        {
            players.Sort();
            this.players.Clear();
            int place = 1;
            foreach (Player p in players)
            {
                p.Place = place;
                place++;
            }
            int id;
            int index;
            foreach (Player p in players)
            {
                foreach (Game g in p.Games)
                {
                    id = g.OpponentId;
                    if (id != -1)
                    {
                        index = Sortitions.FindPlayerIndex(players, id);
                        g.Opponent = players[index].Place;
                    }
                }
                this.players.Add(p);
            }
        }

        public void NextTour()
        {
            if (CurrentTour == NumberOfTours)
            {
                if (!isFinished)
                {
                    UpdatePoints();
                    isFinished = true;
                }
                throw new ArgumentOutOfRangeException("CurrentTour", "Это уже последний тур!");
            }
            if (CurrentTour > 0)
            {
                if (System == TournamentSystem.Circle)
                {
                    UpdatePoints();
                    CurrentTour += 1;
                }
                else
                {
                    MakeSortition();
                    CurrentTour += 1;
                }
            }
            else
            {
                MakeSortition();
                CurrentTour += 1;
            }
        }

        private void UpdatePoints()
        {
            List<Player> players = this.players.ToList();

            if (currentTour > 0)
            {
                // Изменяем игровые очки
                Game game;
                foreach (Player p in players)
                {
                    game = p.Games[CurrentTour - 1];
                    if (game.Result == GameResult.Win || game.Result == GameResult.WinByAbsence)
                    {
                        p.Points += 1;
                    }
                    else if (game.Result == GameResult.Draw)
                    {
                        p.Points += 0.5;
                    }
                }

                // Изменяем коэффициенты Бухгольца и бергера
                foreach (Player p in players)
                {
                    game = p.Games[CurrentTour - 1];
                    if (game.OpponentId != -1)
                    {
                        // индекс и очки противника
                        int index = Sortitions.FindPlayerIndex(players, game.OpponentId);
                        double points = players[index].Points;

                        if (game.Result == GameResult.Win || game.Result == GameResult.WinByAbsence)
                        {
                            p.Buhgolz += points;
                            p.Berger += points;
                        }
                        else if (game.Result == GameResult.Draw)
                        {
                            p.Buhgolz += points / 2;
                            p.Berger += points / 2;
                        }
                        else if (game.Result == GameResult.Defeat || game.Result == GameResult.DefeatByAbsence)
                        {
                            p.Buhgolz += points;
                        }
                    }
                }
            }

            SortPlayers(players);
        }

        private void MakeSortition()
        {
            UpdatePoints();
            // Вызов жеребьевки
            List<Player> players = Sortitions.CircleSystem(this.players.ToList());

            this.players.Clear();
            foreach (Player p in players)
            {
                this.players.Add(p);
            }
        }

        public void AddPlayer(string name)
        {
            Player p = new Player();
            if (this.AllPlayers.Contains(name))
            {
                Player new_p = this.allPlayers[this.AllPlayers.IndexOf(name)];
                p.ID = new_p.ID;
                p.Rating = new_p.Rating;
                p.Name = new_p.Name;
                p.Surname = new_p.Surname;
            }
            else
            {
                p.Name = name;
                p.ID = 10000 + this.players.Count;
            }

            for (int i = 0; i < NumberOfTours; i++)
            {
                Game game = new Game();
                if (i < CurrentTour)
                {
                    game.Result = GameResult.Absence;
                }
                p.Games.Add(game);
            }
            players.Add(p);
        }

        public void Delete(int index)
        {
            try
            {
                players.RemoveAt(index);
            }
            catch { }
        }

        public void RemoveAll()
        {
            players.Clear();
        }


        private void LoadAllPlayers()
        {
            this.allPlayers = new List<Player>();
            this.AllPlayers = new List<string>();

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Player>));

            string api_url = "http://178.62.24.26:8888/api/players";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(api_url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader stream = new StreamReader(
                     response.GetResponseStream(), Encoding.UTF8))
                {
                    this.allPlayers = (List<Player>)jsonFormatter.ReadObject(stream.BaseStream);
                }
            }
            catch
            {
                using (FileStream fs = new FileStream("all_players.json", FileMode.OpenOrCreate))
                {
                    this.allPlayers = (List<Player>)jsonFormatter.ReadObject(fs);
                }
            }

            foreach (Player p in this.allPlayers)
            {
                this.AllPlayers.Add($"{p.Surname} {p.Name}");
            }
        }
    }
}
