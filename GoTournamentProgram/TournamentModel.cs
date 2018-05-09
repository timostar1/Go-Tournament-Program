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

    [DataContract]
    public class TournamentModel: INotifyPropertyChanged
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
            set
            {
                this.system = value;
                OnPropertyChanged("System");
            }
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

        public TournamentModel()
        {
            Players = new ReadOnlyObservableCollection<Player>(players);
            Judges = new ReadOnlyObservableCollection<Judge>(judges);
            name = "new tournament";
            system = TournamentSystem.Circle;
            organizer = "Moscow Go Federation";
            startDate = DateTime.Now;
            endDate = DateTime.Now.AddDays(1);
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
        }

        public void AddUser()
        {
            Player p = new Player();
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
    }
}
