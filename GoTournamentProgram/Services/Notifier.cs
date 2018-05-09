using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace GoTournamentProgram.Services
{
    /// <summary>
    /// Реализует интерфейс INotifyPropertyChanged
    /// </summary>
    [DataContract]
    public class Notifier : INotifyPropertyChanged
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
    }
}
