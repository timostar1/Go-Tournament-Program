using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace GoTournamentProgram.Model
{
    [DataContract]
    public class Player:Person
    {
        /// <summary>
        /// Id участника турнира
        /// </summary>
        private int id;
        /// <summary>
        /// Получает или задает id участника турнира
        /// </summary>
        [DataMember]
        public int ID
        {
            get { return this.id; }
            set
            {
                this.id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Клуб участника турнира
        /// </summary>
        private string club;
        /// <summary>
        /// Получает или задает клуб участника турнира
        /// </summary>
        [DataMember]
        public string Club
        {
            get { return this.club; }
            set
            {
                this.club = value;
                OnPropertyChanged("Club");
            }
        }

        /// <summary>
        /// Город участника турнира
        /// </summary>
        private string city;
        /// <summary>
        /// Получает или задает город участника турнира
        /// </summary>
        [DataMember]
        public string City
        {
            get { return this.city; }
            set
            {
                this.city = value;
                OnPropertyChanged("City");
            }
        }

        /// <summary>
        /// Рейтинг участника турнира
        /// </summary>
        private int rating;
        /// <summary>
        /// Получает или задает рейтинг участника турнира
        /// </summary>
        [DataMember]
        public int Rating
        {
            get { return this.rating; }
            set
            {
                this.rating = value;
                OnPropertyChanged("Rating");
            }
        }

        /// <summary>
        /// Очки макмагона
        /// </summary>
        private int mmr;
        /// <summary>
        /// Получает или задает очки макмагона участника турнира
        /// </summary>
        [DataMember]
        public int MMR
        {
            get { return this.mmr; }
            set
            {
                this.mmr = value;
                OnPropertyChanged("MMR");
            }
        }

        /// <summary>
        /// Количество побед + очки макмагона
        /// </summary>
        private int points;
        /// <summary>
        /// Получает или задает турнирные очки участника турнира
        /// </summary>
        [DataMember]
        public int Points
        {
            get { return this.points; }
            set
            {
                this.points = value;
                OnPropertyChanged("Points");
            }
        }

        /// <summary>
        /// Сумма очков всех соперников
        /// </summary>
        private int buhgolz;
        /// <summary>
        /// Получает или задает сумму очков всех соперников участника турнира
        /// </summary>
        [DataMember]
        public int Buhgolz
        {
            get { return this.buhgolz; }
            set
            {
                this.buhgolz = value;
                OnPropertyChanged("Buhgolz");
            }
        }

        /// <summary>
        /// Сумма очков противников, у которых участник выиграл + 
        /// 1/2 суммы очков противников, с которыми сыграл вничью
        /// </summary>
        private int berger;
        /// <summary>
        /// Получает или задает коэффициент Бергера участника турнира
        /// </summary>
        [DataMember]
        public int Berger
        {
            get { return this.berger; }
            set
            {
                this.berger = value;
                OnPropertyChanged("Berger");
            }
        }

        /// <summary>
        /// Словарь (Номер тура: Объект игры)
        /// </summary>
        private readonly ObservableCollection<Game> games = new ObservableCollection<Game>();
        [DataMember]
        public List<Game> Games { get; set; }
        public Player()
        {
            this.ID = -1;
            this.BirthDate = DateTime.Now;
            this.City = "-";
            this.Club = "-";
            this.Rating = 0;
            this.Name = "-";
            this.Surname = "-";

            // TournamentSettings:
            this.Points = 0;
            this.MMR = 0;
            this.Buhgolz = 0;
            this.Berger = 0;

            //
            this.games.Add(new Game(1));
            this.games.Add(new Game(2));
            this.games.Add(new Game(3));
            this.Games = new List<Game>(games);

        }

        public void AddGameResult(int tour, Game game)
        {
            //this.Games[tour] = game;
        }
        //public void AddGameResult(int tour,int opponentId, Game.GameResult result)
        //{
        //    this.Games[tour] = new Game(opponentId, result);
        //}
        public void AddGameResult(int tour, int opponentId)
        {
            //this.Games[tour] = new Game(opponentId);
        }
        //public void AddGameResult(int tour)
        //{
        //    this.Games[tour] = new Game();
        //}
    }
}
