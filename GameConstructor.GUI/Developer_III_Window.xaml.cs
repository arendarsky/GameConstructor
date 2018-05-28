﻿using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.SpecialMethods;
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
    /// Логика взаимодействия для Developer_III_Window.xaml
    /// </summary>
    public partial class Developer_III_Window : Window
    {
        IGame _game;
        bool _wereThereAlreadySomeChangings;

        bool _savingTheGame = false;
        bool _goingToThePreviousDeveloperWindow = false;

        Dictionary<string, string> _characteristicDictionary;

        Context _context;


        public Developer_III_Window(IGame game, Context context, bool wereThereAlreadySomeChangings)
        {
            _game = game;
            _wereThereAlreadySomeChangings = wereThereAlreadySomeChangings;

            FormingCharacteristicDictionary();

            _context = context;

            InitializeComponent();
        }



        private void FormingCharacteristicDictionary()
        {
            IEnumerable<string> stringCharacteristics = _game.GetCharacteristics.Select(ch => ch.Name);

            _characteristicDictionary = GeneralMethods.FormingTheDictionary(stringCharacteristics);
        }



        private void PreviousWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamePartialSave())
            {
                _goingToThePreviousDeveloperWindow = true;

                Close();
            }
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamePartialSave())
            {
                _savingTheGame = true;

                Close();
            }
        }

        private bool GamePartialSave()
        {
            return true;
        }



        private void SaveGame()
        {
            _game.SaveGame();
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_goingToThePreviousDeveloperWindow && !_savingTheGame)
            {
                if (IfThereWereAnyChangesMadeByUser())
                {
                    var messageBoxResult = MessageBox.Show("Вы уверены, что хотите покинуть окно разработки? Никакие текущие изменения не будут сохранены!",
                    "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

                    if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                    {
                        e.Cancel = true;
                    }
                }
            }

            else if (_goingToThePreviousDeveloperWindow)
            {
                GoingToThePreviousDeveloperWindow();
            }

            else
            {
                SavingTheGameAndReturningToProfile();
            }
        }

        private void GoingToThePreviousDeveloperWindow()
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window(_game, _context, _wereThereAlreadySomeChangings);

            developer_II_Window.Show();
        }

        private void SavingTheGameAndReturningToProfile()
        {
            SaveGame();

            ProfileWindow profileWindow = new ProfileWindow();

            profileWindow.Show();
        }



        private bool IfThereWereAnyChangesMadeByUser()
        {
            if (_wereThereAlreadySomeChangings) { return true; }

            return false;
        }



        private void PossibleResultTextsListBox_Initialized(object sender, EventArgs e)
        {
            PossibleResultTextsListBox.ItemsSource = new List<int> { 1, 2, 3 };
        }

        private void ResultTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ResultTextBlock = sender as TextBlock;

            ResultTextBlock.Text += ((int)ResultTextBlock.DataContext).ToString();
        }
    }
}
