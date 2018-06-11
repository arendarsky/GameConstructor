﻿using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
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
using System.Windows.Shapes;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для DiscriptionOfGameWindow.xaml
    /// </summary>
    public partial class DiscriptionOfGameWindow : Window
    {
        IStorage _storage;
        Game _game;
        public DiscriptionOfGameWindow(IStorage storage, Game game)
        {
            InitializeComponent();
            _storage = storage;
            _game = game;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionsWindow questionsWindow = new QuestionsWindow();

            questionsWindow.Show();

            Close();
        }

        private void BackToPlayModeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

            playingModeWindow.Show();

            Close();
              
        }
    }
}
