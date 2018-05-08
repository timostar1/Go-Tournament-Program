﻿using System;
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
using GoTournamentProgram.View;
using Microsoft.Win32;

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
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.FileOk += MyDialog_FileOk;
            myDialog.ShowDialog();
            //Game game = new Game();
            //game.Owner = this;
            //game.Show();
        }

        private void MyDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}