using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
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
    /// Логика взаимодействия для PlayingModeWindow.xaml
    /// </summary>
    public partial class PlayingModeWindow : Window
    {
        private const double defaultBorderThickness = 1.1;


        IStorage _storage;



        public PlayingModeWindow(IStorage storage)
        {
            _storage = storage;

            InitializeComponent();
            
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



        private void GameAvatarImage_Initialized(object sender, EventArgs e)
        {
            Image image = sender as Image;

            Game game = image.DataContext as Game;
            Border border = image.Parent as Border;

            image.Stretch = Stretch.UniformToFill;

            try
            {
                image.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(game.Picture.ImageSource, "../GameConstructor.Core/Images"))); ;

                if (game.Picture.IsBorderRequired)
                {
                    border.BorderThickness = new Thickness(defaultBorderThickness);
                }

                else
                {
                    border.BorderThickness = new Thickness(0);
                }
            }

            catch
            {
                image.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(
                    "gamepad.png", "../GameConstructor.Core/Images")));
                border.BorderThickness = new Thickness(defaultBorderThickness);
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

        private void RaitingOfTheGameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock RatingTextBlock = sender as TextBlock;

            RatingTextBlock.Text = "Популярность: " + (RatingTextBlock.DataContext as Game).Popularity.ToString();
        }
    }
}
