using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace GoTournamentProgram.Model
{
    [DataContract]
    public class Person: INotifyPropertyChanged
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

        protected string name;
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

        protected string surname;
        [DataMember]
        public string Surname
        {
            get { return this.surname; }
            set
            {
                this.surname = value;
                OnPropertyChanged("Surname");
            }
        }

        protected string gender;
        [DataMember]
        public string Gender
        {
            get { return this.gender; }
            set
            {
                this.gender = value;
                OnPropertyChanged("Gender");
            }
        }

        protected DateTime birthDate;
        [DataMember]
        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set
            {
                this.birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
    }
}
