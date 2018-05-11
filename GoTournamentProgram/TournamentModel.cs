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
                SetNumberOfTours(this.players.Count - 1);
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
            players.Clear();
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

        private void SortPlayers()
        {
            List<Player> list_players =  players.ToList();
            list_players.Sort();
            players.Clear();
            int place = 1;
            foreach (Player p in list_players)
            {
                p.Place = place;
                players.Add(p);
                place++;
            }
        }

        public void NextTour()
        {
            if (CurrentTour == NumberOfTours)
            {
                throw new ArgumentOutOfRangeException("CurrentTour", "Это уже последний тур!");
            }
            if (CurrentTour > 0)
            {
                if (System == TournamentSystem.Circle)
                {
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
            // Изменяем игровые очки
            foreach (Player p in players)
            {
                foreach (Game game in p.Games)
                {
                    if (game.Result == GameResult.Win || game.Result == GameResult.WinByAbsence)
                    {
                        p.Points += 1;
                    }
                    else if (game.Result == GameResult.Draw)
                    {
                        p.Points += 0.5;
                    }
                }
            }

            // Изменяем коэффициенты Бухгольца и бергера
            foreach (Player p in players)
            {
                foreach (Game game in p.Games)
                {
                    if (game.Result == GameResult.Win || game.Result == GameResult.WinByAbsence)
                    {

                    }
                    else if (game.Result == GameResult.Draw)
                    {
                        
                    }
                    else if(game.Result == GameResult.Defeat || game.Result == GameResult.DefeatByAbsence)
                    {

                    }
                }
            }
        }

        private void MakeSortition()
        {
            UpdatePoints();
            // Вызов жеребьевки
            SortPlayers();
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
                    // Ставить пропуск игры
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

            using (FileStream fs = new FileStream("all_players.json", FileMode.OpenOrCreate))
            {
                this.allPlayers = (List<Player>)jsonFormatter.ReadObject(fs);
            }

            foreach (Player p in this.allPlayers)
            {
                this.AllPlayers.Add($"{p.Name} {p.Surname}");
            }
        }
    }
}
