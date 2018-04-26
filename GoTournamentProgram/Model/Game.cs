using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public class Game
    {
        public Game()
        {
            this.opponentId = -1;
            this.Result = GameResult.Absence;
        }

        public Game(int opponentId)
        {
            this.opponentId = opponentId;
            this.Result = GameResult.NotFinished;
        }

        public Game(int opponentId, GameResult res)
        {
            this.OpponentId = opponentId;
            this.Result = res;
        }

        private int opponentId;
        public int OpponentId {
            get { return this.opponentId; }
            set
            {
                if (this.Result == GameResult.Absence)
                {
                    this.opponentId = -1;
                }
                else
                {
                    if (value < 1)
                    {
                        throw new ArgumentOutOfRangeException("opponentId", "Id игрока должен быть больше 0");
                    }
                    this.opponentId = value;
                }
            }
        }

        private GameResult result;
        public GameResult Result {
            get { return this.result; }
            set
            {
                if (value == GameResult.Absence)
                {
                    this.opponentId = -1;
                }
                this.result = value;
            }
        }

        public override string ToString()
        {
            string s = "";
            if (this.OpponentId == -1)
            {
                s = "!";
            }
            else
            {
                s = $"{this.OpponentId}{(char)this.Result}";
            }
            return s;
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
    }
}
