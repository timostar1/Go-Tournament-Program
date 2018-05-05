using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.ObjectModel;

using GoTournamentProgram.Model;
using System.ComponentModel;

namespace GoTournamentProgram
{
    class GoTournamentsVM: BindableBase, INotifyPropertyChanged
    {
        readonly GoTournamentsModel _model = new GoTournamentsModel();


        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get { return this.currentPlayer; }
            set
            {
                this.currentPlayer = value;
                RaisePropertyChanged("CurrentPlayer"); 
            }
        }

        private string testString;
        public string TestString
        {
            get { return this.testString; }
            set
            {
                this.testString = value;
                RaisePropertyChanged("TestString");
            }
        }
        public GoTournamentsVM()
        {
            CurrentPlayer = null;
            TestString = "xxx";
            //
            AddUser = new DelegateCommand(_model.AddUser);
            Save = new DelegateCommand(_model.Save);
            Open = new DelegateCommand(_model.Open);
            Delete = new DelegateCommand<int?>(i => 
            {
                if (i.HasValue) _model.Delete(i.Value);
            });
        }

        public DelegateCommand AddUser { get; }
        public DelegateCommand Save { get; }
        public DelegateCommand Open { get; }
        public DelegateCommand<int?> Delete { get; }
        public ReadOnlyObservableCollection<Player> Users => _model.Users;
    }
}
