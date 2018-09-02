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
using System.IO;

namespace GoTournamentProgram
{
    class TournamentVM : BindableBase
    {
        private IDialogService dialogService;
        private IFileService fileService;
        private IEnvironmentService environmentService;

        readonly TournamentModel _model = new TournamentModel();

        public ReadOnlyObservableCollection<Player> Players => _model.Players;
        public ReadOnlyObservableCollection<Judge> Judges => _model.Judges;
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

        public TournamentVM(IDialogService dialogService, 
            IFileService fileService, IEnvironmentService environmentService)
        {

            this.dialogService = dialogService;
            this.fileService = fileService;
            this.environmentService = environmentService;

            // пробрасываем изменившиеся свойства модели во View
            _model.PropertyChanged += (sender, e) => { RaisePropertyChanged(e.PropertyName); };
            //
            PrintPlayers = new DelegateCommand(PrintTableCommand);
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

            AddJudge = new DelegateCommand(_model.AddJudge);
            RemoveJudge = new DelegateCommand<int?>(i => {
                if (i.HasValue) { _model.RemoveJudgeAt(i.Value); }
            });

            //
            AddPlayerByKey = new DelegateCommand<string>(name => {
                if (name != "")
                {
                    _model.AddPlayer(name);
                }
            });


            // Открытие файла при запуске
            List<string> args = this.environmentService.GetCommandLineArguments().ToList();
            try
            {
                TournamentModel newTournament = fileService.Open(args[1]);
                _model.Update(newTournament);
            }
            catch { }
        }

        public DelegateCommand<string> AddPlayer { get; }
        public DelegateCommand Save { get; }
        public DelegateCommand Open { get; }
        public DelegateCommand<int?> Delete { get; }
        public DelegateCommand RemoveAll { get; }
        public DelegateCommand NextTour { get; }
        public DelegateCommand PrintPlayers { get; }

        public DelegateCommand AddJudge { get; }
        public DelegateCommand<int?> RemoveJudge { get; } 

        //
        public DelegateCommand<string> AddPlayerByKey { get; }

        //
        
        private void OpenCommand()
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    TournamentModel newTournament = fileService.Open(dialogService.FilePath);
                    _model.Update(newTournament);
                    dialogService.ShowMessage("File opened");
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
                if (dialogService.SaveFileDialog($"{Name}.trn") == true)
                {
                    fileService.Save(dialogService.FilePath, _model);
                    dialogService.ShowMessage("File saved");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }

        private void PrintTableCommand()
        {
            try
            {
                if (dialogService.SaveFileDialog($"{Name}-{CurrentTour}_tour.txt") == true)
                {
                    List<string> games = _model.PrintGames();
                    using (FileStream fs = new FileStream(dialogService.FilePath, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                        {
                            foreach (string game in games)
                            {
                                sw.WriteLine(game);
                            }
                        }
                    }
                    dialogService.ShowMessage($"File \"{dialogService.FilePath}\" saved");
                }
            }
            catch (ArgumentException ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
        }
    }
}
