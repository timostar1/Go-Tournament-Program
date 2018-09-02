using System;
using System.Runtime.Serialization;

namespace GoTournamentProgram.Model
{
    [DataContract]
    public class Judge: Person
    {
        private string qualification;
        [DataMember]
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
        [DataMember]
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
