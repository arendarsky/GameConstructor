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
            FormingTheGamesCollection();

            InitializeComponent();            
        }


        private void FormingTheGamesCollection()
        {
            var games = _context.Games
                .Where(g => g.User.Id == _userId);

            //Type gameType = games.FirstOrDefault().GetType();

            _games = GeneralMethods.GettingAbstractCollectionFromNormalCollection<IGame, Game>(games).ToList();
        }


        private void Image_Initialized(object sender, EventArgs e)
        {
            Image image = sender as Image;

            int number = (int)image.DataContext;

            if (number == 2)
            {
                image.Source = new BitmapImage(new Uri("Images/gamepad.png", UriKind.Relative));

                Border border = image.Parent as Border;

                border.BorderThickness = new Thickness(1.1);
            }

            else if (number == 3)
            {
                image.Source = new BitmapImage(new Uri("Images/книга.png", UriKind.Relative));
            }
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


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
