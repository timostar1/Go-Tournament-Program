using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public class TournamentSettings
    {
        public TournamentSystem System { get; set; }
        public string Organizer { get; set; }
        public List<Judge> Judges { get; set; }

        public TournamentSettings()
        {
            this.System = TournamentSystem.Circle;
            this.Organizer = "Moscow Go Federation";
            this.Judges = new List<Judge>();
        }

        public enum TournamentSystem
        {
            MakMagon,
            Circle,
        }
    }
}
