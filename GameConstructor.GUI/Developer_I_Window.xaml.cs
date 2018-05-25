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
    /// Логика взаимодействия для Developer_I_Window.xaml
    /// </summary>
    public partial class Developer_I_Window : Window
    {
        public Developer_I_Window()
        {
            InitializeComponent();

            CharacteristicsListBox.ItemsSource = new List<int> { 1, 2, 3 };
        }


        private void BackToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow mainWindow = new ProfileWindow();

            mainWindow.Show();

            Close();
        }


        private void NextWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window();

            developer_II_Window.Show();

            Close();
        }
    }
}
