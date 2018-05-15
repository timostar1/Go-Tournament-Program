using GoTournamentProgram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    [DataContract]
    public class Game: Notifier
    {
        private int opponentId;
        [DataMember]
        public int OpponentId
        {
            get { return this.opponentId; }
            set
            {
                this.opponentId = value;
                OnPropertyChanged("OpponentId");
            }
        }

        private int opponent;
        [DataMember]
        public int Opponent
        {
            get { return this.opponent; }
            set
            {
                this.opponent = value;
                OnPropertyChanged("Opponent");
            }
        }

        private GameResult result;
        [DataMember]
        public GameResult Result
        {
            get { return this.result; }
            set
            {
                this.result = value;
                OnPropertyChanged("Result");
            }
        }

        public Game()
        {
            this.opponent = -1;
            this.opponentId = -1;
            this.result = GameResult.NotFinished;
        }
    }
}
