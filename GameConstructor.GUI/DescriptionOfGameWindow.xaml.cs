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
    /// Логика взаимодействия для DiscriptionOfGameWindow.xaml
    /// </summary>
    public partial class DescriptionOfGameWindow : Window
    {
        private const double defaultBorderThickness = 1.2;

        private const string defaultImageSource = "gamepad.png";
        private const bool defaultStateOfBorder = true;


        IStorage _storage;
        Game _game;
        User _user;

        bool _goingToPlayGame = false;


        public DescriptionOfGameWindow(IStorage storage, Game game)
        {
            _storage = storage;
            _game = game;
            _user = _storage.Users.Items.FirstOrDefault(user => user.Id == _game.UserId);

            InitializeComponent();
        }



        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            _goingToPlayGame = true;

            Close();
        }

        private void BackToPlayModeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();              
        }



        private void NameOfTheGameTextBlock_Initialized(object sender, EventArgs e)
        {
            NameOfTheGameTextBlock.Text = _game.Name;
        }

        private void SourceTextBlock_Initialized(object sender, EventArgs e)
        {
            SourceTextBlock.Text = _game.Source;

            if (SourceTextBlock.Text == "" || SourceTextBlock.Text == null)
            {
                SourceTextBlock.Height = 0;
            }
        }

        private void DeveloperTextBlock_Initialized(object sender, EventArgs e)
        {
            DeveloperTextBlock.Text = _user.Login;
        }

        private void PopularityTextBlock_Initialized(object sender, EventArgs e)
        {
            PopularityTextBlock.Text = _game.Popularity.ToString();
        }
        
        private void DescriptionTextBlock_Initialized(object sender, EventArgs e)
        {
            DescriptionTextBlock.Text = _game.Description;
        }


        private void GameAvatarImage_Initialized(object sender, EventArgs e)
        {
            var picture = _game.Picture;

            Border imageBorder = GameAvatarImage.Parent as Border;

            GameAvatarImage.Stretch = Stretch.UniformToFill;

            try
            {
                GameAvatarImage.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(picture.ImageSource, "../GameConstructor.Core/Images"))); ;

                if (picture.IsBorderRequired)
                {
                    imageBorder.BorderThickness = new Thickness(defaultBorderThickness);
                }

                else
                {
                    imageBorder.BorderThickness = new Thickness(0);
                }
            }

            catch
            {
                GameAvatarImage.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(defaultImageSource, "../GameConstructor.Core/Images"))); ;

                imageBorder.BorderThickness = new Thickness(defaultBorderThickness);
            }
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_goingToPlayGame)
            {
                PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

                playingModeWindow.Show();
            }

            else
            {
                QuestionsWindow questionsWindow = new QuestionsWindow(_storage, _game);

                questionsWindow.Show();
            }
        }
    }
}
