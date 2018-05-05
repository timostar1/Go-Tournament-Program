using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.Commands;
using Prism.Mvvm;

using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using GoTournamentProgram.Model;

namespace GoTournamentProgram
{
    class GoTournamentsModel: BindableBase
    {
        private readonly ObservableCollection<Player> _users = new ObservableCollection<Player>();
        public readonly ReadOnlyObservableCollection<Player> Users;
        public void AddUser()
        {
            Player p = new Player();
            _users.Add(p);
        }
        public void Save()
        {
            List<Type> types = new List<Type>();
            //types.Add(typeof(Game.GameResult));
            //types.Add(typeof(Game));
            //types.Add(typeof(Person));
            //types.Add(typeof(Player));

            // TODO: JSON сериализация Users
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Player>));

            using (FileStream fs = new FileStream("plaers.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, _users);
            }
        }

        public void Open()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Player>));

            using (FileStream fs = new FileStream("plaers.json", FileMode.OpenOrCreate))
            {
                ObservableCollection<Player> newpeople = (ObservableCollection<Player>)jsonFormatter.ReadObject(fs);

                foreach (Player p in newpeople)
                {
                    _users.Add(p);
                }
            }
        }

        public void Delete(int index)
        {
            try
            {
                _users.RemoveAt(index);
            }
            catch { }
        }

        public GoTournamentsModel()
        {
            Users = new ReadOnlyObservableCollection<Player>(_users);
        }
    }
}
