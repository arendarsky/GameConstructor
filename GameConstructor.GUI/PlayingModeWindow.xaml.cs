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
    /// Логика взаимодействия для PlayingModeWindow.xaml
    /// </summary>
    public partial class PlayingModeWindow : Window
    {
        IStorage _storage;
        public PlayingModeWindow(IStorage storage)
        {
            InitializeComponent();
            _storage = storage;
            OurGamesListBox.ItemsSource = _storage.PlayableGames.Items;
        }

        private void OurGamesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OurGamesListBox.SelectedItem is Game game)
            {
                DiscriptionOfGameWindow discriptionOfGameWindow = new DiscriptionOfGameWindow(_storage, game);

                discriptionOfGameWindow.Show();

                Close();

            }
        }

        private void BackToCreateButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

            Close();

        }

    }
}
