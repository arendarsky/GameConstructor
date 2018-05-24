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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double _defaultWidthOfTheWindow;
        double _defaultHeightOfTheWindow;
        double _defaultFontSize;
        Thickness _defaultPadding;

        double _minimalChangingOfTheSizeParameters;

        double _fontSize;
        Thickness _padding;

        bool _windowIsMaximized = false;


        public MainWindow()
        {
            InitializeComponent();

            //_defaultWidthOfTheWindow = Width;
            //_defaultHeightOfTheWindow = Height;
            //_defaultPadding = PlayingModeButton.Padding;
            //_defaultFontSize = PlayingModeButton.FontSize;
        }


        private void ChangingTheParametersOfTheWindowAndItsContent()
        {
            _fontSize = _defaultFontSize * Math.Pow(_minimalChangingOfTheSizeParameters, 1.2);

            _padding.Left = _defaultPadding.Left * Math.Pow(_minimalChangingOfTheSizeParameters, 1.8);
            _padding.Right = _defaultPadding.Right * Math.Pow(_minimalChangingOfTheSizeParameters, 1.8);
            _padding.Top = _defaultPadding.Top * Math.Pow(_minimalChangingOfTheSizeParameters, 1.2);
            _padding.Bottom = _defaultPadding.Bottom * Math.Pow(_minimalChangingOfTheSizeParameters, 1.2);

            //PlayingModeButton.FontSize = _fontSize;
            //DeveloperModeButton.FontSize = _fontSize;

            //PlayingModeButton.Padding = _padding;
            //DeveloperModeButton.Padding = _padding;
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_windowIsMaximized)
            {
                _minimalChangingOfTheSizeParameters = Math.Min(Height / _defaultHeightOfTheWindow, Width / _defaultWidthOfTheWindow);

                ChangingTheParametersOfTheWindowAndItsContent();
            }
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                _minimalChangingOfTheSizeParameters = MaxWidth / _defaultWidthOfTheWindow;

                _windowIsMaximized = true;

                ChangingTheParametersOfTheWindowAndItsContent();                
            }

            else if (WindowState == WindowState.Normal)
            {
                _windowIsMaximized = false;
            }
        }


        private void DeveloperModeButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_I_Window developerModeWindow = new Developer_I_Window();

            developerModeWindow.Show();

            Close();
        }
    }
}
