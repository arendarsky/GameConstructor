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
using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        Context _context = new Context();

        List<IGame> _games;
        int _userId = 1;


        public ProfileWindow()
        {
            InitializeComponent();

            FormingTheGamesListBoxSource();
        }


        private void FormingTheGamesCollection()
        {
            var games = _context.Games
                .Where(g => g.User.Id == _userId);

            //Type gameType = games.FirstOrDefault().GetType();

            _games = GeneralMethods.GettingAbstractCollectionFromNormalCollection<IGame, Game>(games).ToList();
        }

        private void FormingTheGamesListBoxSource()
        {
            FormingTheGamesCollection();

            UserGamesListBox.ItemsSource = _games;
        }



        private void GameAvatarImage_Initialized(object sender, EventArgs e)
        {
            Image image = sender as Image;

            Game game = image.DataContext as Game;
            Border border = image.Parent as Border;

            image.Source = new BitmapImage(new Uri("Images/gamepad.png", UriKind.Relative));
            border.BorderThickness = new Thickness(1.1);

            if (game.Name == "Новая тестовая игра")
            {
                image.Source = new BitmapImage(new Uri("Images/img_199922.png", UriKind.Relative));
                border.BorderThickness = new Thickness(0);
            }

            else if (game.Name == "Yura's game")
            {
                image.Source = new BitmapImage(new Uri("Images/книга.png", UriKind.Relative));
                border.BorderThickness = new Thickness(0);
            }
        }

        private void NameOfTheGameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock NameTextBlock = sender as TextBlock;

            NameTextBlock.Text = (NameTextBlock.DataContext as Game).Name;
        }

        private void SourceOfTheGameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock SourceTextBlock = sender as TextBlock;

            SourceTextBlock.Text = (SourceTextBlock.DataContext as Game).Source;
        }



        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_I_Window developer_I_Window = new Developer_I_Window();

            developer_I_Window.Show();

            Close();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

            Close();
        }


        private void UserGamesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Game game = UserGamesListBox.SelectedItem as Game;

            Developer_I_Window developer_I_Window = new Developer_I_Window(game, _context);

            developer_I_Window.Show();

            Close();
        }
    }
}
