using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using GoTournamentProgram.Services;

namespace GoTournamentProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext =
                new TournamentVM(new DefaultDialogService(), new JsonFileService());
        }

        private void PlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Title = "xxx";
                RoutedEventArgs ev = new RoutedEventArgs();
                ev.RoutedEvent = Button.ClickEvent;
                this.AddPlayerBtn.RaiseEvent(ev);
                //this.AddPlayerBtn.
                
            }
        }
    }
}
