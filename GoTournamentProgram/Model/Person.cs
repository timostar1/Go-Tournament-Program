using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTournamentProgram.Model
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Gender gender { get; set; }
        public DateTime BirthDate { get; set; }

        public enum Gender
        {
            Male,
            Female
        }
    }
}
