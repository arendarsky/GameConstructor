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
using GameConstructor.Core.SpecialMethods;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private const double defaultBorderThickness = 1.1;


        IStorage _storage;

        User _user;


        public ProfileWindow(IStorage storage, User user)
        {
            _storage = storage;
            _user = storage.LoadUsersGames(user);
        
            InitializeComponent();

            DefaultGameListBoxItemsSource();
        }


        private void DefaultGameListBoxItemsSource()
        {
            UserGamesListBox.ItemsSource = null;

            UserGamesListBox.ItemsSource = _user.Games;
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

            if (NameTextBlock.Text.Length > 35)
            {
                NameTextBlock.Text = NameTextBlock.Text.Substring(0, 32) + "...";
            }
        }

        private void SourceOfTheGameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock SourceTextBlock = sender as TextBlock;

            SourceTextBlock.Text = (SourceTextBlock.DataContext as Game).Source;

            if (SourceTextBlock.Text.Length > 50)
            {
                SourceTextBlock.Text = SourceTextBlock.Text.Substring(0, 47) + "...";
            }
        }



        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_I_Window developer_I_Window = new Developer_I_Window(_storage, _user);

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
            if (UserGamesListBox.SelectedItem is Game game)
            {
                Developer_I_Window developer_I_Window = new Developer_I_Window(_user, game, _storage);

                developer_I_Window.Show();

                Close();
            }
        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить данную игру? Это действие нельзя будет отменить и все изменения будут утеряны!",
                    "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

            if (messageBoxResult != MessageBoxResult.No && messageBoxResult != MessageBoxResult.Cancel && messageBoxResult != MessageBoxResult.None)
            {
                Button deleteButton = sender as Button;

                Game currentGame = deleteButton.DataContext as Game;

                _storage.RemoveGame(currentGame);
                _user.Games.RemoveAll(g => g.Name == currentGame.Name);
                DefaultGameListBoxItemsSource();
            }
        }



        private void BrowseGamesFromJsonTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Factory.Instance.LoadFromFileToDatabase(_user);

            MessageBox.Show("Данные успешно загружены!");

            _storage.LoadNewUsers();

            _user = _storage.LoadUsersGames(_user);

            DefaultGameListBoxItemsSource();
        }

        private void SaveGamesToJsonTextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Factory.Instance.SynchronizeFileStorage(_user);

            MessageBox.Show("Данные успешно сохранены!");
        }



        private void BrowseGamesFromJsonTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            BrowseGamesFromJsonTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5F391D"));
        }

        private void BrowseGamesFromJsonTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            BrowseGamesFromJsonTextBox.Foreground = Brushes.Black;
        }

        private void SaveGamesToJsonTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveGamesToJsonTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5F391D"));
        }

        private void SaveGamesToJsonTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveGamesToJsonTextBox.Foreground = Brushes.Black;
        }
    }
}
