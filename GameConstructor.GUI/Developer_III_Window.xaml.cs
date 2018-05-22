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
        public Developer_III_Window()
        {
            InitializeComponent();
        }


        private void PreviousWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window();

            developer_II_Window.Show();

            Close();
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            Close();
        }
    }
}
