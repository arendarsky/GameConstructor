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
        IGame _game;
        List<Characteristic> _localCharacteristics;


        public EndOfGameWindow(IStorage storage, IGame game, List<Characteristic> localCharacteristics)
        {
            _storage = storage;
            _game = game;
            _localCharacteristics = localCharacteristics;

            InitializeComponent();

            UpdatePopularity();
        }



        private void UpdatePopularity()
        {
            int popularity = _game.Popularity;

            popularity++;

            _game.UpdatePopularity(popularity);

            //User user = _storage.Users.Items.FirstOrDefault(u => u.Id == _game.UserId);

            //_storage.SaveGame(user, _game);
        }



        private void PlayToDifferentGames_Click(object sender, RoutedEventArgs e)
        {
            PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

            playingModeWindow.Show();

            Close();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            DescriptionOfGameWindow discriptionOfGameWindow = new DescriptionOfGameWindow(_storage, _game);

            discriptionOfGameWindow.Show();

            Close();
        }
    }
}
