using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public class PlayerTournamentSettings
    {
        /// <summary>
        /// Очки макмагона
        /// </summary>
        public int MMR { get; set; }

        /// <summary>
        /// Количество побед + очки макмагона
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Сумма очков всех соперников
        /// </summary>
        public int Buhgolz { get; set; }

        /// <summary>
        /// Сумма очков противников, у которых участник выиграл + 
        /// 1/2 суммы очков противников, с которыми сыграл вничью
        /// </summary>
        public int Berger { get; set; }

        public PlayerTournamentSettings()
        {
            this.Berger = 0;
            this.Buhgolz = 0;
            this.MMR = 0;
            this.Points = 0;
        }
    }
}
