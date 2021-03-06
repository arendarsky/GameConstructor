﻿using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
using GameConstructor.Core.SpecialMethods;
using Microsoft.Win32;
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
using GameConstructor.GUI.UICore;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для Developer_I_Window.xaml
    /// </summary>
    public partial class Developer_I_Window : Window
    {
        private const string defaultGameName = "Введите название Вашей игры";
        private const string defaultSource = "Укажите оригинальный источник (ссылку), если имеется";
        private const string defaultDescription = "Здесь приведите краткое описание — пользователям оно будет показано, когда они захотят опробовать Вашу игру";

        private const string defaultCharacteristicName = "Название характеристики";
        private const int defaultValueOfCharacteristic = 0;

        private const string defaultImageSource = "gamepad.png";
        private const bool defaultStateOfBorder = true;

        private const double defaultBorderThickness = 1.2;


        IStorage _storage;

        IGame _game;
        Picture _picture;
        User _user;
        List<Characteristic> _characteristics;
        bool _wereThereAlreadySomeChangings;

        bool _goingToTheNextDeveloperWindow = false;
        bool _characteristicNameTextBoxShouldBeFocused = false;
        bool _theSameCharacteristicsNamesErrorWasShown = false;
        bool _creatingTheGameForTheFirstTime = false;
        int _newCharacteristicsAdded = 0;

        

        public Developer_I_Window(IStorage storage, User user)
        {
            _user = user;
            _storage = storage;
            _game = Factory.Instance.GetGame();
            _picture = new Picture(defaultImageSource, defaultStateOfBorder);
            _characteristics = new List<Characteristic>();
            _wereThereAlreadySomeChangings = false;
            _creatingTheGameForTheFirstTime = true;

            InitializeComponent();

            AddNewDefaultCharacteristic();
        }

        public Developer_I_Window(User user, IGame game, IStorage storage, bool wereThereAlreadySomeChangings)
        {
            _game = game;
            _storage = storage;
            _user = user;
            _wereThereAlreadySomeChangings = wereThereAlreadySomeChangings;

            Constructor();
        }

        public Developer_I_Window(User user, IGame game, IStorage storage)
        {
            _game = storage.OpenGame(game);
            _storage = storage;
            _user = user;
            _wereThereAlreadySomeChangings = false;

            Constructor();
        }


        private void Constructor()
        {
            _characteristics = _game.GetCharacteristics.ToList();
            _picture = _game.Picture;

            if (_picture == null)
            {
                _picture = new Picture(defaultImageSource, defaultStateOfBorder);
            }

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

            _newCharacteristicsAdded++;

            _wereThereAlreadySomeChangings = true;
        }



        private void BackToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NextWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamePartialSave())
            {
                _goingToTheNextDeveloperWindow = true;

                Close();
            }
        }



        private bool GamePartialSave()
        {
            if (CheckingIfEveryFieldIsFilledCorrectly())
            {
                IfThereWereAnyChangesMadeByUser();

                string sourceText = SourceTextBox.Text;
                Picture picture = _picture;

                if (sourceText == defaultSource)
                {
                    sourceText = null;
                }
                if (picture.ImageSource == defaultImageSource && picture.IsBorderRequired == defaultStateOfBorder)
                {
                    picture = null;
                }

                _game.UpdateName(GameNameTextBox.Text);
                _game.UpdateSource(sourceText);
                _game.UpdateDescription(GameDescriptionTextBox.Text);
                _game.UpdatePicture(picture);
                _game.UpdateCharacteristics(_characteristics);

                return true;
            }

            return false;
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
                SourceTextBox.Text = defaultSource;

                SourceTextBox.Foreground = Brushes.Gray;
            }
        }

        private void GameDescriptionTextBox_Initialized(object sender, EventArgs e)
        {
            GameDescriptionTextBox.Text = _game.Description;

            if (_game.Description == null || _game.Description == "")
            {
                GameDescriptionTextBox.Text = defaultDescription;

                GameDescriptionTextBox.Foreground = Brushes.Gray;
            }            
        }



        private void EditAvatarImage_Initialized(object sender, EventArgs e)
        {
            Border imageBorder = EditAvatarImage.Parent as Border;

            EditAvatarImage.Stretch = Stretch.UniformToFill;

            try
            {                        
                EditAvatarImage.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(_picture.ImageSource, "../GameConstructor.Core/Images"))); ;

                if (_picture.IsBorderRequired)
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
                EditAvatarImage.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(defaultImageSource, "../GameConstructor.Core/Images"))); ;

                imageBorder.BorderThickness = new Thickness(defaultBorderThickness);
            }            
        }



        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageUploaded imageUploadingProcess = new ImageUploaded();

                imageUploadingProcess.UploadImageAndSave();

                _picture = imageUploadingProcess.Picture;

                EditAvatarImage.Source = new BitmapImage(new Uri(ImageUploaded.GetDestinationPath(_picture.ImageSource, "../GameConstructor.Core/Images")));

                if (_picture.IsBorderRequired)
                {
                    (EditAvatarImage.Parent as Border).BorderThickness = new Thickness(defaultBorderThickness);
                }

                else
                {
                    (EditAvatarImage.Parent as Border).BorderThickness = new Thickness(0);
                }
            }

            catch { }
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

            if (_characteristicNameTextBoxShouldBeFocused)
            {
                CharacteristicNameTextBox.Focus();

                _characteristicNameTextBoxShouldBeFocused = false;
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

            if (characteristic.Name != CharacteristicNameTextBox.Text)
            {
                characteristic.Name = CharacteristicNameTextBox.Text;

                _wereThereAlreadySomeChangings = true;
            }            
        }

        private void CharacteristicValueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox CharacteristicValueTextBox = sender as TextBox;

            Characteristic characteristic = CharacteristicValueTextBox.DataContext as Characteristic;

            bool intParsing = int.TryParse(CharacteristicValueTextBox.Text, out int val);

            if (intParsing && characteristic.Value != val)
            {
                characteristic.Value = val;

                _wereThereAlreadySomeChangings = true;
            }

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
            if (SourceTextBox.Text == defaultSource)
            {
                SourceTextBox.Text = "";
                SourceTextBox.Foreground = Brushes.Black;
            }
        }

        private void SourceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SourceTextBox.Text == "")
            {
                SourceTextBox.Text = defaultSource;
                SourceTextBox.Foreground = Brushes.Gray;
            }
        }

        private void GameDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GameDescriptionTextBox.Text == defaultDescription)
            {
                GameDescriptionTextBox.Text = "";
                GameDescriptionTextBox.Foreground = Brushes.Black;
            }
        }

        private void GameDescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (GameDescriptionTextBox.Text == "")
            {
                GameDescriptionTextBox.Text = defaultDescription;
                GameDescriptionTextBox.Foreground = Brushes.Gray;
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

            else if (GameDescriptionTextBox.Text == defaultDescription)
            {
                MessageBox.Show("Описание должно присутствовать у каждой игры. Не оставляйте это поле пустым.", "Ошибка!");
                GameDescriptionTextBox.Focus();
                return false;
            }

            else if (CharacteristicsListBox.Items.Count == 0)
            {
                MessageBox.Show("В игре не может не быть характеристик вообще — добавьте, пожалуйста, хотя бы одну!", "Ошибка!");

                AddNewDefaultCharacteristic();
                _characteristicNameTextBoxShouldBeFocused = true;

                return false;
            }

            else if (UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(CharacteristicsListBox, 0, defaultCharacteristicName, "Название характеристики — обязательный аттрибут. Заполните все поля либо удалите ненужные характеристики."))
            {
                return false;
            }            

            var namesOfCharacteristics = _characteristics.Select(ch => ch.Name);
            var namesOfCharacteristicsWithOneRegister = _characteristics.Select(ch => ch.Name.ToUpperInvariant());

            if (GeneralMethods.AreThereSameElementsInTheStringCollection(namesOfCharacteristics, out string element))
            {
                UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(CharacteristicsListBox, 0, element, "По крайней мере две ваши характеристики имеют одинаковое название. Так нельзя — как же игроки будут их различать?");

                _theSameCharacteristicsNamesErrorWasShown = true;

                return false;
            }

            else if (GeneralMethods.AreThereSameElementsInTheStringCollection(namesOfCharacteristicsWithOneRegister, out element))
            {
                if (_theSameCharacteristicsNamesErrorWasShown)
                {
                    UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(CharacteristicsListBox, 0, element, "Одинаковые названия в разном регистре  — не вариант, уж простите. Пожалуйста, измените названия.");
                }

                else
                {
                    UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(CharacteristicsListBox, 0, element, "По крайней мере две ваши характеристики имеют одинаковое название. Без учета регистра. Пожалуйста, измените названия.");
                }

                return false;
            }

            int amountOfGamesWithSuchName = _user.Games.Where(game => game.Name == GameNameTextBox.Text).Count();

            if ((amountOfGamesWithSuchName > 0 && _creatingTheGameForTheFirstTime) || (amountOfGamesWithSuchName > 1 && (!_creatingTheGameForTheFirstTime)))
            {
                MessageBox.Show("У Вас уже есть игра с таким названием. Пожалуйста, придумайте для этой игры другое!", "Ошибка!");

                GameNameTextBox.Focus();

                return false;
            }

            return true;
        }



        private bool IfThereWereAnyChangesMadeByUser()
        {
            if (_wereThereAlreadySomeChangings) { return true; }

            string name = GameNameTextBox.Text;
            string source = SourceTextBox.Text;
            string description = GameDescriptionTextBox.Text;
            Picture picture = _picture;
            List<Characteristic> characteristics = _characteristics;

            if (name == defaultGameName)
            {
                name = null;
            }
            if (source == defaultSource)
            {
                source = null;
            }
            if (description == defaultDescription)
            {
                description = null;
            }
            if (picture.ImageSource == defaultImageSource && picture.IsBorderRequired == defaultStateOfBorder)
            {
                picture = null;
            }
            if (characteristics.Count == 1 && characteristics[0].Name == defaultCharacteristicName && characteristics[0].Value == defaultValueOfCharacteristic)
            {
                characteristics = null;
            }

            if (name == _game.Name && source == _game.Source && description == _game.Description && picture == _game.Picture)
            {
                return false;
            }

            _wereThereAlreadySomeChangings = true;

            return true;
        }



        private void LoosingFocusAtTheEnd()
        {
            if (FocusManager.GetFocusedElement(this) is TextBox FocusedTextBox)
            {
                if (FocusedTextBox.DataContext is Characteristic characteristic)
                {
                    Grid CharacteristicGrid = FocusedTextBox.Parent as Grid;

                    if (FocusedTextBox == CharacteristicGrid.Children[0] as TextBox)
                    {
                        CharacteristicNameTextBox_LostFocus(FocusedTextBox, null);
                    }

                    else
                    {
                        CharacteristicValueTextBox_LostFocus(FocusedTextBox, null);
                    }
                }

                else if (FocusedTextBox == GameNameTextBox)
                {
                    GameNameTextBox_LostFocus(FocusedTextBox, null);
                }

                else if (FocusedTextBox == SourceTextBox)
                {
                    SourceTextBox_LostFocus(FocusedTextBox, null);
                }

                else
                {
                    GameDescriptionTextBox_LostFocus(FocusedTextBox, null);
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoosingFocusAtTheEnd();

            bool cancellation = false;

            if (!_goingToTheNextDeveloperWindow)
            {
                if (IfThereWereAnyChangesMadeByUser())
                {
                    var messageBoxResult = MessageBox.Show("Вы уверены, что хотите покинуть окно разработки? Никакие текущие изменения не будут сохранены!",
                    "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

                    if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                    {
                        e.Cancel = true;

                        cancellation = true;
                    }
                }

                if (!cancellation)
                {
                    GoingBackToProfileWindow();
                }
            }

            else
            {
                GoingToTheNextDeveloperWindow();
            }
        }

        private void GoingToTheNextDeveloperWindow()
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window(_user, _game, _storage, _wereThereAlreadySomeChangings, _newCharacteristicsAdded);

            developer_II_Window.Show();
        }

        private void GoingBackToProfileWindow()
        {
            ProfileWindow profileWindow = new ProfileWindow(_storage, _user);
            _storage.CloseGame();

            profileWindow.Show();
        }



        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult;

            if (_game.GetQuestions != null)
            {
                messageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить данную характеристику? Все связанные с ней влияния из следующего окна будут также удалены!",
                       "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);
            }
            else
            {
                messageBoxResult = MessageBoxResult.Yes;
            }

            if (messageBoxResult != MessageBoxResult.No && messageBoxResult != MessageBoxResult.Cancel && messageBoxResult != MessageBoxResult.None)
            {
                Button DeleteButton = sender as Button;

                Characteristic characteristic = DeleteButton.DataContext as Characteristic;

                int indexPosition = _characteristics.IndexOf(characteristic);

                if (indexPosition >= _characteristics.Count() - _newCharacteristicsAdded)
                {
                    _newCharacteristicsAdded--;
                }

                _storage.RemoveCharacteristic(characteristic);

                _characteristics.Remove(characteristic);

                DefaultCharacteristicsListBoxSource();

                _wereThereAlreadySomeChangings = true;
            }
        }
    }
}
