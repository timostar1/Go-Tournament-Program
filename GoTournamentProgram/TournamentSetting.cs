using GoTournamentProgram.Services;
using System.Runtime.Serialization;

namespace GoTournamentProgram
{
    [DataContract]
    public class TournamentSetting<T>: Notifier
    {
        private T value;
        [DataMember]
        public T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        private bool isEnabled;
        [DataMember]
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public TournamentSetting(T value, bool isEnabled)
        {
            Value = value;
            IsEnabled = isEnabled;
        }
    }
}
