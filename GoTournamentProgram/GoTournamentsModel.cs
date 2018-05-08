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
        private readonly ObservableCollection<TournamentModel> _tournaments = new ObservableCollection<TournamentModel>();
        public readonly ReadOnlyObservableCollection<TournamentModel> Tournaments;

        public GoTournamentsModel()
        {
            Tournaments = new ReadOnlyObservableCollection<TournamentModel>(_tournaments);
        }

        public void AddTournament()
        {
            _tournaments.Add(new TournamentModel());
        }
    }
}
