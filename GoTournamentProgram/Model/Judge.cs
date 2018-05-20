using System;


namespace GoTournamentProgram.Model
{
    public class Judge: Person
    {
        private string qualification;
        public string Qualification
        {
            get { return this.qualification; }
            set
            {
                this.qualification = value;
                OnPropertyChanged("Qualification");
            }
        }
        private DateTime workSince;
        public DateTime WorkSince
        {
            get { return this.workSince; }
            set
            {
                this.workSince = value;
                OnPropertyChanged("WorkSince");
            }
        }

        public Judge()
        {
            this.Name = "-";
            this.Surname = "-";
            this.Gender = "Male";
            this.BirthDate = DateTime.Now;
            this.WorkSince = DateTime.Now;
            this.Qualification = "-";
        }
    }
}
