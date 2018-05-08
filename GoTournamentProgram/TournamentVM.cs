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
    class TournamentVM: BindableBase
    {
        public Dictionary<string, Game> TestDict = new Dictionary<string, Game>();
        //

        private IFileDialogService fileDialogService;
        private IFileService fileService;

        readonly TournamentModel _model = new TournamentModel();

        public ReadOnlyObservableCollection<Player> Players => _model.Players;
        public string Name => _model.Name;

        public TournamentVM(IFileDialogService fileDialogService, IFileService fileService)
        {
            TestDict.Add("xxx", new Game(19));
            //

            this.fileDialogService = fileDialogService;
            this.fileService = fileService;

            //таким нехитрым способом мы пробрасываем изменившиеся свойства модели во View
            _model.PropertyChanged += (sender, e) => { RaisePropertyChanged(e.PropertyName); };
            //
            AddUser = new DelegateCommand(_model.AddUser);
            Save = new DelegateCommand(SaveCommand);
            Open = new DelegateCommand(OpenCommand);
            Delete = new DelegateCommand<int?>(i =>
            {
                if (i.HasValue) _model.Delete(i.Value);
            });
            //
            ChangeName = new DelegateCommand(_model.ChangeName);
        }

        public DelegateCommand AddUser { get; }
        public DelegateCommand Save { get; }
        public DelegateCommand Open { get; }
        public DelegateCommand<int?> Delete { get; }
        //
        public DelegateCommand ChangeName { get; }

        private void OpenCommand()
        {
            try
            {
                if (fileDialogService.OpenFileDialog() == true)
                {
                    TournamentModel newTournament = fileService.Open(fileDialogService.FilePath);
                    _model.Update(newTournament);
                    fileDialogService.ShowMessage("Файл открыт");
                }
            }
            catch (Exception ex)
            {
                fileDialogService.ShowMessage(ex.Message);
            }
        }

        private void SaveCommand()
        {
            try
            {
                if (fileDialogService.SaveFileDialog() == true)
                {
                    fileService.Save(fileDialogService.FilePath, _model);
                    fileDialogService.ShowMessage("Файл сохранен");
                }
            }
            catch (Exception ex)
            {
                fileDialogService.ShowMessage(ex.Message);
            }
        }
    }
}
