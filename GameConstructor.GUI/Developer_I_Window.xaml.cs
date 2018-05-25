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
        private const string defaultCharacteristicName = "Название характеристики";
        private const int defaultValueOfCharacteristic = 0;

        IGame _game;
        List<Characteristic> _characteristics = new List<Characteristic>();


        public Developer_I_Window(bool creatingANewGame)
        {
            InitializeComponent();

            if (creatingANewGame)
            {
                _game = Factory.Instance.GetGame;

                AddNewDefaultCharacteristic();
            }

            else
            {
                CharacteristicsListBox.ItemsSource = new List<int> { 1, 2, 3 };
            }
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


        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("К сожалению, эта возможность ещё не реализована. Ожидайте ближайших обновлений.", "Ошибка!");
        }


        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            _game.NewCharacteristics(_characteristics);
        }



        private void CharacteristicNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicNameTextBox = sender as TextBox;

            CharacteristicNameTextBox.Text = (CharacteristicNameTextBox.DataContext as Characteristic).Name;
        }

        private void CharacteristicNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicNameTextBox = sender as TextBox;

            Characteristic characteristic = CharacteristicNameTextBox.DataContext as Characteristic;

            characteristic.Name = CharacteristicNameTextBox.Text;
        }

        private void CharacteristicValueTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicValueTextBox = sender as TextBox;

            CharacteristicValueTextBox.Text = (CharacteristicValueTextBox.DataContext as Characteristic).Value.ToString();
        }

        private void CharacteristicValueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicValueTextBox = sender as TextBox;

            Characteristic characteristic = CharacteristicValueTextBox.DataContext as Characteristic;

            try
            {
                characteristic.Value = int.Parse(CharacteristicValueTextBox.Text);
            }
            catch
            {
                CharacteristicValueTextBox.Text = characteristic.Value.ToString();
            }
        }
    }
}
