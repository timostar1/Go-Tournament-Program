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
    class GoTournamentsVM: BindableBase
    {
        //readonly GoTournamentsModel _model = new GoTournamentsModel();
        private readonly ObservableCollection<TournamentVM> tournaments = new ObservableCollection<TournamentVM>();
        //public Dictionary<string, TournamentVM> Tournaments;

        public GoTournamentsVM()
        {
            //tournaments.Add(new TournamentVM());
            AddTournament = new DelegateCommand(_AddTournament);
        }

        private void _AddTournament()
        {
        }

        public DelegateCommand AddTournament { get; }
    }
}
