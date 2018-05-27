using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
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
        Context _context;


        public Developer_III_Window(IGame game, Context context)
        {
            _game = game;
            _context = context;

            InitializeComponent();
        }


        private void PreviousWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window(_game, _context);

            developer_II_Window.Show();

            Close();
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();

            LoginWindow mainWindow = new LoginWindow();

            mainWindow.Show();

            Close();
        }

        private void SaveGame()
        {

        }
    }
}
