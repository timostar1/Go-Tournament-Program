using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTournamentProgram.Model;
using GoTournamentProgram.Services;

namespace GoTournamentProgram
{
    class TournamentVM : BindableBase
    {
        private IDialogService dialogService;
        private IFileService fileService;

        readonly TournamentModel _model = new TournamentModel();

        public ReadOnlyObservableCollection<Player> Players => _model.Players;
        public string Name { get => _model.Name; set { _model.Name = value; } }
        public string Organizer { get => _model.Organizer; set { _model.Organizer = value; } }
        public TournamentSystem System { get => _model.System; set { _model.SetSystem(value); } }
        public DateTime StartDate { get => _model.StartDate; set { _model.StartDate = value; } }
        public DateTime EndDate
        {
            get => _model.EndDate;
            set
            {
                try
                {
                    _model.EndDate = value;
                }
                catch (ArgumentException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            }
        }

        public int NumberOfTours {
            get => _model.NumberOfTours;
            set { _model.SetNumberOfTours(value); }
        }

        public bool IsNumberOfToursEnabled {
            get => _model.IsNumberOfToursEnabled;
            set { _model.IsNumberOfToursEnabled = value; }
        }

        public int CurrentTour => _model.CurrentTour;

        //
        public List<string> AllPlayers => _model.AllPlayers;
        //

        public TournamentVM(IDialogService dialogService, IFileService fileService)
        {

            this.dialogService = dialogService;
            this.fileService = fileService;

            //таким нехитрым способом мы пробрасываем изменившиеся свойства модели во View
            _model.PropertyChanged += (sender, e) => { RaisePropertyChanged(e.PropertyName); };
            //
            AddPlayer = new DelegateCommand<string>(name => { _model.AddPlayer(name); });
            Save = new DelegateCommand(SaveCommand);    
            Open = new DelegateCommand(OpenCommand);
            Delete = new DelegateCommand<int?>(i =>
            {
                if (i.HasValue) _model.Delete(i.Value);
            });
            RemoveAll = new DelegateCommand(_model.RemoveAll);
            NextTour = new DelegateCommand(() => 
            {
                try { _model.NextTour(); }
                catch (ArgumentOutOfRangeException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    dialogService.ShowMessage(ex.Message);
                }
            });
        }

        public DelegateCommand<string> AddPlayer { get; }
        public DelegateCommand Save { get; }
        public DelegateCommand Open { get; }
        public DelegateCommand<int?> Delete { get; }
        public DelegateCommand RemoveAll { get; }
        public DelegateCommand NextTour { get; }

        private void OpenCommand()
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    TournamentModel newTournament = fileService.Open(dialogService.FilePath);
                    _model.Update(newTournament);
                    dialogService.ShowMessage("Файл открыт");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }

        private void SaveCommand()
        {
            try
            {
                if (dialogService.SaveFileDialog() == true)
                {
                    fileService.Save(dialogService.FilePath, _model);
                    dialogService.ShowMessage("Файл сохранен");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }
    }
}
