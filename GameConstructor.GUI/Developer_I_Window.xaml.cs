using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
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
        private const string defaultGameName = "Введите название вашей игры";
        private const string defaultSourceName = "Укажите оригинальный источник (ссылку), если имеется";
        private const string defaultCharacteristicName = "Название характеристики";
        private const int defaultValueOfCharacteristic = 0;

        private const double defaultBorderThickness = 1.2;


        IGame _game;
        List<Characteristic> _characteristics;
        Context _context;


        public Developer_I_Window()
        {
            _game = Factory.Instance.GetGame;
            _characteristics = new List<Characteristic>();

            InitializeComponent();

            AddNewDefaultCharacteristic();
        }

        public Developer_I_Window(IGame game, Context context)
        {
            _game = game;
            _characteristics = _game.GetCharacteristics.ToList();
            _context = context;

            InitializeComponent();

            DefaultCharacteristicsListBoxSource();
        }


        private void DefaultCharacteristicsListBoxSource()
        {
            CharacteristicsListBox.ItemsSource = null;

            CharacteristicsListBox.ItemsSource = _characteristics;
        }

        private void AddNewDefaultCharacteristic()
        {
            _characteristics.Add(new Characteristic(defaultCharacteristicName, defaultValueOfCharacteristic));

            DefaultCharacteristicsListBoxSource();
        }

        private void NewCharacteristicButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDefaultCharacteristic();
        }



        private void BackToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();

            profileWindow.Show();

            Close();
        }

        private void NextWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window();

            developer_II_Window.Show();

            Close();
        }



        private void GameNameTextBox_Initialized(object sender, EventArgs e)
        {
            GameNameTextBox.Text = _game.Name;

            if (_game.Name == null || _game.Name == "")
            {
                GameNameTextBox.Text = defaultGameName;

                GameNameTextBox.Foreground = Brushes.Gray;
            }
        }

        private void SourceTextBox_Initialized(object sender, EventArgs e)
        {
            SourceTextBox.Text = _game.Source;

            if (_game.Source == null || _game.Source == "")
            {
                SourceTextBox.Text = defaultSourceName;

                SourceTextBox.Foreground = Brushes.Gray;
            }
        }

        private void EditAvatarImage_Initialized(object sender, EventArgs e)
        {
            if (_game.Picture != null)
            {
                try
                {
                    Border imageBorder = EditAvatarImage.Parent as Border;

                    EditAvatarImage.Stretch = Stretch.UniformToFill;

                    EditAvatarImage.Source = new BitmapImage(new Uri("Images/" + _game.Picture.ImageSource, UriKind.Relative));

                    if (_game.Picture.IsBorderRequired)
                    {
                        imageBorder.BorderThickness = new Thickness(defaultBorderThickness);
                    }

                    else
                    {
                        imageBorder.BorderThickness = new Thickness(0);
                    }
                }
                catch { }
            }
        }



        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("К сожалению, эта возможность ещё не реализована. Ожидайте ближайших обновлений.", "Ошибка!");
        }


        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckingIfEveryFieldIsFilledCorrectly())
            {
                string sourceText = SourceTextBox.Text;

                if (sourceText == defaultSourceName)
                {
                    sourceText = "";
                }

                _game.UpdateName(GameNameTextBox.Text);
                _game.UpdateSource(sourceText);
                _game.UpdateCharacteristics(_characteristics);

                _game.SaveGame(_context);
            }
        }



        private void CharacteristicNameTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox CharacteristicNameTextBox = sender as TextBox;

            Characteristic characteristic = CharacteristicNameTextBox.DataContext as Characteristic;

            CharacteristicNameTextBox.Text = characteristic.Name;

            if (characteristic.Name == defaultCharacteristicName)
            {
                CharacteristicNameTextBox.Foreground = Brushes.Gray;
            }
        }

        private void CharacteristicValueTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox CharacteristicValueTextBox = sender as TextBox;

            Characteristic characteristic = CharacteristicValueTextBox.DataContext as Characteristic;

            CharacteristicValueTextBox.Text = characteristic.Value.ToString();
        }

        private void CharacteristicNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicNameTextBox = sender as TextBox;

            if (CharacteristicNameTextBox.Text == defaultCharacteristicName)
            {
                CharacteristicNameTextBox.Text = "";
                CharacteristicNameTextBox.Foreground = Brushes.Black;
            }
        }

        private void CharacteristicValueTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicValueTextBox = sender as TextBox;

            if (CharacteristicValueTextBox.Text == defaultValueOfCharacteristic.ToString())
            {
                CharacteristicValueTextBox.Text = "";
            }
        }

        private void CharacteristicNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicNameTextBox = sender as TextBox;

            if (CharacteristicNameTextBox.Text == "")
            {
                CharacteristicNameTextBox.Text = defaultCharacteristicName;
                CharacteristicNameTextBox.Foreground = Brushes.Gray;
            }

            Characteristic characteristic = CharacteristicNameTextBox.DataContext as Characteristic;

            characteristic.Name = CharacteristicNameTextBox.Text;
        }

        private void CharacteristicValueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicValueTextBox = sender as TextBox;

            Characteristic characteristic = CharacteristicValueTextBox.DataContext as Characteristic;

            try { characteristic.Value = int.Parse(CharacteristicValueTextBox.Text); }
            catch { }

            CharacteristicValueTextBox.Text = characteristic.Value.ToString();
        }

        private void GameNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GameNameTextBox.Text == defaultGameName)
            {
                GameNameTextBox.Text = "";
                GameNameTextBox.Foreground = Brushes.Black;
            }
        }

        private void GameNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GameNameTextBox.Text == "")
            {
                GameNameTextBox.Text = defaultGameName;
                GameNameTextBox.Foreground = Brushes.Gray;
            }
        }

        private void SourceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SourceTextBox.Text == defaultSourceName)
            {
                SourceTextBox.Text = "";
                SourceTextBox.Foreground = Brushes.Black;
            }
        }

        private void SourceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SourceTextBox.Text == "")
            {
                SourceTextBox.Text = defaultSourceName;
                SourceTextBox.Foreground = Brushes.Gray;
            }
        }


        private bool CheckingIfEveryFieldIsFilledCorrectly()
        {
            if (GameNameTextBox.Text == defaultGameName)
            {
                MessageBox.Show("Название игры является обязательным полем!", "Ошибка!");
                GameNameTextBox.Focus();
                return false;
            }

            else
            {
                for (int i = 0; i < CharacteristicsListBox.Items.Count; i++)
                {
                    ListBoxItem itemContainer = CharacteristicsListBox.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                    Border itemContainerBorder = VisualTreeHelper.GetChild(itemContainer, 0) as Border;
                    ContentPresenter itemContainerContentPresenter = VisualTreeHelper.GetChild(itemContainerBorder, 0) as ContentPresenter;
                    Grid itemUIParent = VisualTreeHelper.GetChild(itemContainerContentPresenter, 0) as Grid;

                    TextBox CharacteristicNameTextBox = itemUIParent.Children[0] as TextBox;

                    if (CharacteristicNameTextBox.Text == defaultCharacteristicName)
                    {
                        MessageBox.Show("Название характеристики — обязательный аттрибут. Заполните все поля либо удалите ненужные характеристики");
                        CharacteristicNameTextBox.Focus();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
