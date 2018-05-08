using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTournamentProgram.Model;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GoTournamentProgram
{
    [DataContract]
    public class TournamentModel: INotifyPropertyChanged
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

        private string fileName;

        private readonly ObservableCollection<Player> _players = new ObservableCollection<Player>();
        [DataMember]
        public readonly ReadOnlyObservableCollection<Player> Players;

        private string name;
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

        public TournamentModel()
        {
            Players = new ReadOnlyObservableCollection<Player>(_players);
            name = "new tournament";
        }

        public void Update(TournamentModel model)
        {
            this._players.Clear();
            foreach (Player p in model.Players)
            {
                this._players.Add(p);
            }

            Name = model.Name;
        }

        public void ChangeName()
        {
            Name = "EYGC 2018";
        }

        public void AddUser()
        {
            Player p = new Player();
            _players.Add(p);
        }

        public void Delete(int index)
        {
            try
            {
                _players.RemoveAt(index);
            }
            catch { }
        }
    }
}
