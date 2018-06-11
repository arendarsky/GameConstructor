using GameConstructor.Core.Interfaces;
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
    /// Логика взаимодействия для EndOfGameWindow.xaml
    /// </summary>
    public partial class EndOfGameWindow : Window
    {
        IStorage _storage;
        Game _game;
        public EndOfGameWindow()
        {
            InitializeComponent();
        }

        private void PlayToDifferentGames_Click(object sender, RoutedEventArgs e)
        {
            PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

            playingModeWindow.Show();

            Close();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            DiscriptionOfGameWindow discriptionOfGameWindow = new DiscriptionOfGameWindow(_storage, _game);

            discriptionOfGameWindow.Show();

            Close();
        }
    }
}
