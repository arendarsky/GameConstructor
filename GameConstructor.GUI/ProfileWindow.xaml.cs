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
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();

            UserGamesListBox.ItemsSource = new List<int> { 1, 2, 3 };
        }

        private void Image_Initialized(object sender, EventArgs e)
        {
            Image image = sender as Image;

            int number = (int)image.DataContext;

            if (number == 2)
            {
                
            }
        }
    }
}
