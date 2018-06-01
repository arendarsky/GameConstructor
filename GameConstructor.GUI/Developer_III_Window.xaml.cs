using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.SpecialMethods;
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
using GameConstructor.Core.Models;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для Developer_III_Window.xaml
    /// </summary>
    public partial class Developer_III_Window : Window
    {
        private const string emDash = " — ";

        private const string conjuctionSymbol = "&";
        private const string conjuctionName = "Конъюнкиция";
        private const string conjuctionExplanation = "логическое И";

        private const string disjuctionSymbol = "|";
        private const string disjuctionName = "Дизъюнкция";
        private const string disjuctionExplanation = "логическое ИЛИ";

        private const string bracketSymbol = ")  (";
        private const string bracketName = "Скобки";
        private const string bracketExplanation = "очередность операций";

        private const string mathOperationSymbol = "+  -  *  /";
        private const string mathOperationName = "Математические операции";

        private const string mathOperatorSymbol = "=  >  <  >=  <=  !=";
        private const string mathOperatorName = "Операторы";
        private const string mathOperatorExplanation = "символ '!=' обозначает оператор 'не равно'";


        private const string defaultNumberResultComboBoxText = "номер";

        private const string defaultConstructorBuildingComboBox = "Выберете продолжение";
        private const string constructorBuildingComboBoxNewCondition = "Новое условие";
        private const string constructorBuildingComboBoxOtherVariants = "Остальные варианты";


        IGame _game;
        bool _wereThereAlreadySomeChangings;
        User _user;
        bool _savingTheGame = false;
        bool _goingToThePreviousDeveloperWindow = false;

        IStorage _storage;
        Dictionary<string, string> _characteristicDictionary;


        private string Conjuction => conjuctionSymbol + emDash + conjuctionName + " (" + conjuctionExplanation + ")";
        private string Disjuction => disjuctionSymbol + emDash + disjuctionName + " (" + disjuctionExplanation + ")";
        private string Brackets => bracketSymbol + emDash + bracketName + " (" + bracketExplanation + ")";
        private string MathOperations => mathOperationSymbol + emDash + mathOperationName;
        private string MathOperators => mathOperatorSymbol + emDash + mathOperatorName; // + " (" + mathOperatorExplanation + ")";



        public Developer_III_Window(User user, IGame game, IStorage storage, bool wereThereAlreadySomeChangings)
        {
            _user = user;
            _game = game;
            _wereThereAlreadySomeChangings = wereThereAlreadySomeChangings;

            _storage = storage;

            FormingCharacteristicDictionary();

            InitializeComponent();
        }



        private void FormingCharacteristicDictionary()
        {
            IEnumerable<string> stringCharacteristics = _game.GetCharacteristics.Select(ch => ch.Name.ToUpperInvariant());

            _characteristicDictionary = GeneralMethods.FormingTheAbbreviationDictionary(stringCharacteristics);
        }



        private void PreviousWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamePartialSave())
            {
                _goingToThePreviousDeveloperWindow = true;

                Close();
            }
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamePartialSave())
            {
                _savingTheGame = true;

                Close();
            }
        }

        private bool GamePartialSave()
        {
            return true;
        }



        private void SaveGame()
        {
            _storage.SaveGame(_user, _game);
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_goingToThePreviousDeveloperWindow && !_savingTheGame)
            {
                if (IfThereWereAnyChangesMadeByUser())
                {
                    var messageBoxResult = MessageBox.Show("Вы уверены, что хотите покинуть окно разработки? Никакие текущие изменения не будут сохранены!",
                    "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

                    if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                    {
                        e.Cancel = true;
                    }

                    else
                    {
                        GoingBackToProfileWindow();
                    }
                }
            }

            else if (_goingToThePreviousDeveloperWindow)
            {
                GoingToThePreviousDeveloperWindow();
            }

            else
            {
                SavingTheGameAndReturningToProfile();
            }
        }

        private void GoingToThePreviousDeveloperWindow()
        {
            Developer_II_Window developer_II_Window = new Developer_II_Window(_user, _game, _storage, _wereThereAlreadySomeChangings);

            developer_II_Window.Show();
        }

        private void GoingBackToProfileWindow()
        {
            ProfileWindow profileWindow = new ProfileWindow(_storage, _user);

            profileWindow.Show();
        }

        private void SavingTheGameAndReturningToProfile()
        {
            SaveGame();

            ProfileWindow profileWindow = new ProfileWindow(_storage, _user);

            profileWindow.Show();
        }



        private bool IfThereWereAnyChangesMadeByUser()
        {
            if (_wereThereAlreadySomeChangings) { return true; }

            return false;
        }



        private void PossibleResultTextsListBox_Initialized(object sender, EventArgs e)
        {
            PossibleResultTextsListBox.ItemsSource = new List<int> { 1, 2, 3 };
        }

        private void ResultTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ResultTextBlock = sender as TextBlock;

            ResultTextBlock.Text += ((int)ResultTextBlock.DataContext).ToString();
        }



        private void CharacteristicAbbreviationsListBox_Initialized(object sender, EventArgs e)
        {
            List<string> characteristicKeys = _characteristicDictionary.Keys
                .OrderBy(key => key)
                .ToList();

            List<string> abbreviationStrings = new List<string>();

            foreach (var key in characteristicKeys)
            {
                string value = _characteristicDictionary[key];

                string originalKey = GeneralMethods.ReturnOriginalRegisterConfiguration(key, _game.GetCharacteristics.Select(ch => ch.Name));

                abbreviationStrings.Add(value + emDash + originalKey);
            }

            List<string> defaultKeys = new List<string> { MathOperations, MathOperators, Brackets, Conjuction, Disjuction };

            CharacteristicAbbreviationsListBox.ItemsSource = defaultKeys.Concat(abbreviationStrings);
        }        

        private void CharacteristicAbbreviationTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CharacteristicAbbreviationTextBlock = sender as TextBlock;

            string text = CharacteristicAbbreviationTextBlock.DataContext as string;

            CharacteristicAbbreviationTextBlock.Text = text;
        }



        private void Constructor_Initialized(object sender, EventArgs e)
        {
            Constructor.ItemsSource = new List<int> { 1, 2, 3 };
        }



        private void ResultNumberCombobox_Initialized(object sender, EventArgs e)
        {
            ComboBox ResultNumberCombobox = sender as ComboBox;

            List<string> comboBoxSource = new List<string> { defaultNumberResultComboBoxText };

            foreach (var textResult in PossibleResultTextsListBox.ItemsSource)
            {
                comboBoxSource.Add(textResult.ToString());
            }

            ResultNumberCombobox.ItemsSource = comboBoxSource;

            ResultNumberCombobox.SelectedIndex = 0;
        }

        private void ConstructorBuildingCombobox_Initialized(object sender, EventArgs e)
        {
            ComboBox ConstructorBuildingComboBox = sender as ComboBox;

            List<string> comboBoxSource = new List<string> { defaultConstructorBuildingComboBox, constructorBuildingComboBoxNewCondition, constructorBuildingComboBoxOtherVariants };

            ConstructorBuildingComboBox.ItemsSource = comboBoxSource;
                        
            ConstructorBuildingComboBox.SelectedIndex = 1;

            if ((int)ConstructorBuildingComboBox.DataContext == 3)
            {
                ConstructorBuildingComboBox.SelectedIndex = 0;
            }
        }
    }
}
