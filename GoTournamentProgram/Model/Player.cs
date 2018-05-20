using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace GoTournamentProgram.Model
{
    [DataContract]
    public class Player: Person, IComparable<Player>
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
        /// Место в турнире
        /// </summary>
        private int place;
        /// <summary>
        /// Получает или задает место в турнире
        /// </summary>
        [DataMember]
        public int Place
        {
            get { return this.place; }
            set
            {
                this.place = value;
                OnPropertyChanged("Place");
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
                this.Points = this.Points + value;
                OnPropertyChanged("MMR");
            }
        }

        /// <summary>
        /// Количество побед + очки макмагона
        /// </summary>
        private double points;
        /// <summary>
        /// Получает или задает турнирные очки участника турнира
        /// </summary>
        [DataMember]
        public double Points
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
        private double buhgolz;
        /// <summary>
        /// Получает или задает сумму очков всех соперников участника турнира
        /// </summary>
        [DataMember]
        public double Buhgolz
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
        private double berger;
        /// <summary>
        /// Получает или задает коэффициент Бергера участника турнира
        /// </summary>
        [DataMember]
        public double Berger
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
            this.Gender = "Male";

            // TournamentSettings:
            this.Place = 0;
            this.Points = 0.0;
            this.MMR = 0;
            this.Buhgolz = 0;
            this.Berger = 0;

            //
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

        /// <summary>
        /// Реализация интерфейса IComparable
        /// </summary>
        /// <param name="other">Объект для сравнения</param>
        /// <returns>Результат сравнения двух участников турнира</returns>
        public int CompareTo(Player other)
        {
            if (this.Points > other.Points)
            {
                return -1;
            }
            else if (this.Points < other.Points)
            {
                return 1;
            }
            else
            {
                if (this.Buhgolz > other.Buhgolz)
                {
                    return -1;
                }
                else if (this.Buhgolz < other.Buhgolz)
                {
                    return 1;
                }
                else
                {
                    if (this.Berger > other.Berger)
                    {
                        return -1;
                    }
                    else if (this.Berger < other.Berger)
                    {
                        return 1;
                    }
                    else
                    {
                        if (this.Rating > other.Rating)
                        {
                            return -1;
                        }
                        else if (this.Rating < other.Rating)
                        {
                            return 1;
                        }
                        else
                        {
                            // Личная встреча
                            return 0;
                        }
                    }
                }
            }
        }


        //public void AddGameResult(int tour)
        //{
        //    this.Games[tour] = new Game();
        //}

        public override string ToString()
        {
            string games = "";
            foreach (Game g in Games)
            {
                games += $"| {g.Opponent}{(char)g.Result} ";
            }
            return $"{Name} {Surname} {Rating} {MMR} {games}| {Points} {Place}";
        }
    }
}
