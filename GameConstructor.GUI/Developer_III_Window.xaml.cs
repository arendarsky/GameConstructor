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
using GameConstructor.GUI.UICore;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для Developer_III_Window.xaml
    /// </summary>
    public partial class Developer_III_Window : Window
    {
        private const string defaultConditionTextBoxText = "Кликните на позицию в конструкторе ниже, чтобы редактировать условия результата в данном поле";
        private const string constructorConditionText = "логическое условие";

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


        private const string defaultResultTextBlockText = "Результат №";
        private const string defaultTextResult = "Введите здесь текст возможного результата";

        private const string defaultNumberResultComboBoxText = "номер";

        private const string defaultConstructorBuildingComboBox = "Выберите продолжение";
        private const string constructorBuildingComboBoxNewCondition = "Новое условие";
        private const string constructorBuildingComboBoxOtherVariants = "Остальные варианты";

        private const string ifCondition = "ЕСЛИ";
        private const string elseIfCondition = "ИНАЧЕ ЕСЛИ";
        private const string elseCondition = "ВО ВСЕХ ОСТАЛЬНЫХ СЛУЧАЯХ";

        private const string conditionEnterpretationError = "Возникла ошибка при попытке интерпретировать Ваше условие как математическую комбинацию из логических высказываний. Проверьте, что вы используете верные обозначения, а синтаксис не нарушен, и после этого повторите попытку.";


        private const string defaultConditionTextBoxBorderBrush = "#CC443830";


        IGame _game;
        bool _wereThereAlreadySomeChangings;
        User _user;

        bool _savingTheGame = false;
        bool _goingToThePreviousDeveloperWindow = false;
        bool _lastConstructorConditionContinuingIsElse = false;
        bool _userSelectionChanging = false;

        int _indexOfConstructorTextBoxEdittedByTheMoment = -1;

        IStorage _storage;
        Dictionary<string, string> _characteristicDictionary;
        List<Result> _textResults;
        List<Core.Models.Condition> _conditions;

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

            InitializingTextResults();
            InitializingConditions();
        }



        private void InitializingTextResults()
        {
            try
            {
                _textResults = _game.GetResults.ToList();
            }

            catch
            {
                _textResults = new List<Result>();
            }

            DefaultPossibleTextResultsItemsSource();

            if (_textResults.Count == 0)
            {
                AddNewPossibleTextResult();
            }
        }

        private void InitializingConditions()
        {
            try
            {
                _conditions = _game.GetConditions.ToList();
            }

            catch
            {
                _conditions = new List<Core.Models.Condition>();

                _lastConstructorConditionContinuingIsElse = false;
            }

            DefaultConstructorItemsSource();

            if (_conditions.Count == 0)
            {
                AddNewCondition();
            }

            else if (_conditions.Count != 1)
            {
                _lastConstructorConditionContinuingIsElse = true;
            }
        }



        private void AddNewCondition()
        {
            _conditions.Add(new Core.Models.Condition(constructorConditionText));

            DefaultConstructorItemsSource();
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

        private void SaveGame()
        {
            _storage.SaveGame(_user, _game);
        }



        private bool GamePartialSave()
        {
            if (CheckingIfEveryFieldIsFilledCorrectly())
            {
                IfThereWereAnyChangesMadeByUser();

                _game.UpdateResults(_textResults);
                _game.UpdateConditions(_conditions);

                return true;
            }

            return false;
        }



        private bool CheckingIfEveryFieldIsFilledCorrectly()
        {
            if (!AllTextConditionsAreValid())
            {
                return false;
            }

            else if (!NonNullTextResultsForConditions())
            {
                return false;
            }

            else if (!_lastConstructorConditionContinuingIsElse)
            {
                MessageBox.Show("Сетка условий конструктора не завершена! Пожалуйста, добавьте в конец карточку с полем 'во всех остальных случаях', иначе программа может некорректно отображать пользователю текстовый результат игры после ее прохождения.", "Ошибка!");

                return false;
            }

            else if (!AreThereAtLeastOneTextField())
            {
                return false;
            }

            else if (!AreTheObligatoryTextFieldsFilled())
            {
                return false;
            }

            else if (!AreTheTextFieldsFilledDifferently())
            {
                return false;
            }

            return true;
        }

        
        private bool AreThereAtLeastOneTextField()
        {
            return true;
        }

        private bool AreTheObligatoryTextFieldsFilled()
        {
            if (UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(PossibleResultTextsListBox, 1, defaultTextResult, "В списке возможных текстовых результатов остались пустые поля. Пожалуйста, заполните их либо удалите ненужные."))
            {
                return false;
            }

            return true;
        }

        private bool AreTheTextFieldsFilledDifferently()
        {
            var textResultsBodies = _textResults.Select(textR => textR.Body.ToUpperInvariant());

            if (GeneralMethods.AreThereSameElementsInTheStringCollection(textResultsBodies, out string textResultElement))
            {
                UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(PossibleResultTextsListBox, 1, textResultElement, "По крайней мере два текстовых результата совпадают. Удалите ненужные либо переименуйте.");

                return false;
            }

            return true;
        }


        private bool NonNullTextResultsForConditions()
        {
            for (int i = 0; i < _conditions.Count; i++)
            {
                Grid ConditionGrid = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(Constructor, i, 1) as Grid;

                ComboBox TextResultNumberComboBox = ConditionGrid.Children[1] as ComboBox;

                if (TextResultNumberComboBox.SelectedIndex == 0)
                {
                    MessageBox.Show("Каждое условие в конструкторе должно быть связано с одним текстовым результатом. Настройте все ссылки либо измените сетку условий, удалив ненужные.", "Ошибка!");

                    TextResultNumberComboBox.Focus();

                    return false;
                }
            }

            return true;
        }


        private bool AllTextConditionsAreValid()
        {
            for (int i = 0; i < _conditions.Count; i++)
            {
                Grid ConditionGrid = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(Constructor, i, 0) as Grid;

                TextBox TextConditionTextBox = ConditionGrid.Children[1] as TextBox;

                var condition = TextConditionTextBox.DataContext as Core.Models.Condition;

                int index = _conditions.IndexOf(condition);

                if (!(index == _conditions.Count - 1 && _lastConstructorConditionContinuingIsElse))
                {
                    if (TextConditionTextBox.Text == constructorConditionText)
                    {
                        MessageBox.Show("Поле с условием не может оставаться пустым. Заполните его или переформатируйте конструктор, чтобы избавиться от ненужных карточек.", "Ошибка!");

                        TextConditionTextBox.Focus();

                        return false;
                    }

                    else
                    {
                        TextConditionTextBox.Text = (GeneralMethods.MathConditionWithValidSpaces(TextConditionTextBox.Text)).ToUpperInvariant();
                        condition.Text = TextConditionTextBox.Text;

                        var stringCopyWithoutBrackets = TextConditionTextBox.Text
                            .Replace("(", "")
                            .Replace(")", "");

                        var elements = stringCopyWithoutBrackets.Split(' ');

                        if (elements.Count() % 4 != 3)
                        {
                            MessageBox.Show(conditionEnterpretationError, "Ошибка!");

                            TextConditionTextBox.Focus();

                            return false;
                        }

                        else
                        {
                            for (int j = 1; j < elements.Length; j += 4)
                            {
                                if (GeneralMethods.MathOperatorsContains(elements[j]) == false)
                                {
                                    MessageBox.Show(conditionEnterpretationError, "Ошибка!");

                                    TextConditionTextBox.Focus();

                                    return false;
                                }
                            }

                            for (int j = 3; j < elements.Length; j += 4)
                            {
                                if (GeneralMethods.LogicalOperatorsContains(elements[j]) == false)
                                {
                                    MessageBox.Show(conditionEnterpretationError, "Ошибка!");

                                    TextConditionTextBox.Focus();

                                    return false;
                                }
                            }

                            for (int j = 0; j < elements.Length; j += 4)
                            {
                                if (int.TryParse(elements[j], out int t) && int.TryParse(elements[j + 2], out t))
                                {
                                    MessageBox.Show("Оператор не может связывать два числа. Пожалуйста, внесите параметры характеристик во все условия, в которых они отсутствуют.", "Ошибка!");

                                    TextConditionTextBox.Focus();

                                    return false;
                                }                               
                            }

                            for (int j = 0; j < elements.Length; j+=4)
                            {
                                for (int k = 0; k <= 2; k += 2)
                                {
                                    if (!int.TryParse(elements[j + k], out int t) && !_characteristicDictionary.Values.Contains(elements[j + k].ToUpperInvariant()))
                                    {
                                        MessageBox.Show($"Название переменной '{elements[j + k]}' в выражении — невалидное. Пожалуйста, проверьте, что это и все другие названия переменных согласуются с приведенными в верхней части окна программными сокращениями и вслед за этим повторите попытку.", "Ошибка!");

                                        TextConditionTextBox.Focus();

                                        return false;
                                    }
                                }                                
                            }

                            for (int j = 0; j < elements.Length; j+=4)
                            {
                                if (elements[j] == elements[j + 2])
                                {
                                    MessageBox.Show($"Переменная '{elements[j]}' сравнивается сама с собой. Подобные сравнения не несут смысловой нагрузки, поскольку возвращают всегда одно и то же значение (либо true, либо false — зависит от оператора). Пожалуйста, избегайте таких конструкций.", "Ошибка!");

                                    TextConditionTextBox.Focus();

                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;            
        }



        private void LoosingFocusAtTheEnd()
        {
            if (FocusManager.GetFocusedElement(this) is TextBox FocusedTextBox)
            {
                if (FocusedTextBox.DataContext is Result result)
                {
                    ResultTextBox_LostFocus(FocusedTextBox, null);
                }

                else
                {
                    ConditionsTextBox_LostFocus(FocusedTextBox, null);
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoosingFocusAtTheEnd();

            bool cancelation = false;

            if (!_goingToThePreviousDeveloperWindow && !_savingTheGame)
            {
                if (IfThereWereAnyChangesMadeByUser())
                {
                    var messageBoxResult = MessageBox.Show("Вы уверены, что хотите покинуть окно разработки? Никакие текущие изменения не будут сохранены!",
                    "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

                    if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                    {
                        e.Cancel = true;

                        cancelation = true;
                    }
                }

                if (!cancelation)
                {
                    GoingBackToProfileWindow();
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
            Developer_II_Window developer_II_Window = new Developer_II_Window(_user, _game, _storage, _wereThereAlreadySomeChangings, 0);

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
            DefaultPossibleTextResultsItemsSource();
        }

        private void ResultTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ResultTextBlock = sender as TextBlock;

            Result result = ResultTextBlock.DataContext as Result;

            int index = _textResults.IndexOf(result) + 1;

            ResultTextBlock.Text = defaultResultTextBlockText + index.ToString();
        }

        private void ResultTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox ResultTextBox = sender as TextBox;

            Result result = ResultTextBox.DataContext as Result;

            ResultTextBox.Text = result.Body;

            if (result.Body == defaultTextResult)
            {
                ResultTextBox.Foreground = Brushes.Gray;
            }
        }

        private void ResultTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox ResultTextBox = sender as TextBox;

            if (ResultTextBox.Text == defaultTextResult)
            {
                ResultTextBox.Text = "";

                ResultTextBox.Foreground = Brushes.Black;
            }
        }

        private void ResultTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox ResultTextBox = sender as TextBox;

            if (ResultTextBox.Text == "")
            {
                ResultTextBox.Text = defaultTextResult;

                ResultTextBox.Foreground = Brushes.Gray;
            }

            Result result = ResultTextBox.DataContext as Result;

            if (result.Body != ResultTextBox.Text)
            {
                result.Body = ResultTextBox.Text;

                _wereThereAlreadySomeChangings = true;
            }
        }


        private void NewTextResultButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewPossibleTextResult();

            _wereThereAlreadySomeChangings = true;
        }


        private void DefaultPossibleTextResultsItemsSource()
        {
            PossibleResultTextsListBox.ItemsSource = null;

            PossibleResultTextsListBox.ItemsSource = _textResults;
        }

        private void AddNewPossibleTextResult()
        {
            _textResults.Add(new Result(defaultTextResult));

            DefaultPossibleTextResultsItemsSource();

            if (Constructor != null) { DefaultConstructorItemsSource(); }
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

            List<string> defaultKeys = new List<string> { MathOperators, Brackets, Conjuction, Disjuction };

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
            DefaultConstructorItemsSource();
        }


        private void ConstructorConditionTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox ConstructorConditionTextBox = sender as TextBox;

            var condition = ConstructorConditionTextBox.DataContext as Core.Models.Condition;

            int index = _conditions.IndexOf(condition);

            if (index == _conditions.Count - 1 && _lastConstructorConditionContinuingIsElse)
            {
                ConstructorConditionTextBox.Visibility = Visibility.Hidden;
            }

            else
            {
                ConstructorConditionTextBox.Text = condition.Text;
            }
        }

        private void ConstructorConditionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ConstructorConditionTextBlock = sender as TextBlock;

            var condition = ConstructorConditionTextBlock.DataContext as Core.Models.Condition;

            int index = _conditions.IndexOf(condition);

            if (index == 0)
            {
                ConstructorConditionTextBlock.Text = ifCondition;
            }

            else if (index == _conditions.Count - 1 && _lastConstructorConditionContinuingIsElse)
            {
                ConstructorConditionTextBlock.Text = elseCondition;
            }

            else
            {
                ConstructorConditionTextBlock.Text = elseIfCondition;
            }
        }

        private void ComaTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ComaTextBlock = sender as TextBlock;

            var condition = ComaTextBlock.DataContext as Core.Models.Condition;

            int index = _conditions.IndexOf(condition);

            if (index == _conditions.Count - 1 && _lastConstructorConditionContinuingIsElse)
            {
                ComaTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void ResultNumberCombobox_Initialized(object sender, EventArgs e)
        {
            _userSelectionChanging = false;

            ComboBox ResultNumberCombobox = sender as ComboBox;

            List<string> comboBoxSource = new List<string> { defaultNumberResultComboBoxText };

            foreach (var result in _textResults)
            {
                int index = _textResults.IndexOf(result) + 1;

                comboBoxSource.Add(index.ToString());
            }

            ResultNumberCombobox.ItemsSource = comboBoxSource;

            var condition = ResultNumberCombobox.DataContext as Core.Models.Condition;

            if (condition.Result == null)
            {
                ResultNumberCombobox.SelectedIndex = 0;
            }

            else
            {
                int index = _textResults.IndexOf(condition.Result) + 1;

                ResultNumberCombobox.SelectedIndex = index;
            }

            _userSelectionChanging = true;
        }

        private void ConstructorBuildingCombobox_Initialized(object sender, EventArgs e)
        {
            _userSelectionChanging = false;

            ComboBox ConstructorBuildingComboBox = sender as ComboBox;

            List<string> comboBoxSource = new List<string> { defaultConstructorBuildingComboBox, constructorBuildingComboBoxNewCondition, constructorBuildingComboBoxOtherVariants };

            ConstructorBuildingComboBox.ItemsSource = comboBoxSource;

            var condition = ConstructorBuildingComboBox.DataContext as Core.Models.Condition;

            int index = _conditions.IndexOf(condition);

            if (!_lastConstructorConditionContinuingIsElse)
            {
                if (index == _conditions.Count - 1)
                {
                    ConstructorBuildingComboBox.SelectedIndex = 0;
                }

                else
                {
                    ConstructorBuildingComboBox.SelectedIndex = 1;
                }
            }

            else
            {
                if (index == _conditions.Count - 1)
                {
                    ConstructorBuildingComboBox.Visibility = Visibility.Hidden;

                    Grid parentGrid = ConstructorBuildingComboBox.Parent as Grid;

                    parentGrid.RowDefinitions[2].Height = new GridLength(0);
                }

                else if (index == _conditions.Count - 2)
                {
                    ConstructorBuildingComboBox.SelectedIndex = 2;
                }

                else
                {
                    ConstructorBuildingComboBox.SelectedIndex = 1;
                }
            }

            _userSelectionChanging = true;
        }
        

        private void DefaultConstructorItemsSource()
        {
            Constructor.ItemsSource = null;

            Constructor.ItemsSource = _conditions;
        }


        private void ResultNumberCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_userSelectionChanging)
            {
                ComboBox ResultNumberComboBox = sender as ComboBox;

                var condition = ResultNumberComboBox.DataContext as Core.Models.Condition;

                if (ResultNumberComboBox.SelectedIndex == 0)
                {
                    condition.Result = null;
                    condition.ResultId = 0;
                }

                else
                {
                    var index = int.Parse(ResultNumberComboBox.SelectedItem as string) - 1;

                    var textResult = _textResults[index];

                    condition.Result = textResult;
                    condition.ResultId = textResult.Id;
                }

                _wereThereAlreadySomeChangings = true;
            }
        }

        private void ConstructorBuildingCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_userSelectionChanging)
            {
                _lastConstructorConditionContinuingIsElse = false;

                ComboBox ConstructorBuildingCombobox = sender as ComboBox;

                var condition = ConstructorBuildingCombobox.DataContext as Core.Models.Condition;

                int index = _conditions.IndexOf(condition);

                foreach (var c in _conditions.Skip(index + 1).ToList())
                {
                    _conditions.Remove(c);
                    _storage.RemoveCondition(c);
                }
                //_conditions = _conditions.Take(index + 1).ToList();

                if (ConstructorBuildingCombobox.SelectedIndex == 0)
                {
                    DefaultConstructorItemsSource();
                }

                else if (ConstructorBuildingCombobox.SelectedIndex == 1)
                {
                    AddNewCondition();
                }

                else
                {
                    _lastConstructorConditionContinuingIsElse = true;

                    AddNewCondition();
                }

                _wereThereAlreadySomeChangings = true;
            }
        }


        private void ConditionsTextBox_Initialized(object sender, EventArgs e)
        {
            DefaultConditionTextBoxState();
        }

        private void DefaultConditionTextBoxState()
        {
            ConditionsTextBox.Text = defaultConditionTextBoxText;

            ConditionsTextBox.Foreground = Brushes.Gray;

            ConditionsTextBox.IsReadOnly = true;
        }



        private void ConstructorConditionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox ConstructorConditionTextBox = sender as TextBox;

            var dataContext = ConstructorConditionTextBox.DataContext as Core.Models.Condition;

            if (ConstructorConditionTextBox.Text == constructorConditionText)
            {
                ConstructorConditionTextBox.Text = "";
            }

            _indexOfConstructorTextBoxEdittedByTheMoment = _conditions.IndexOf(dataContext);

            ConditionsTextBox.Text = ConstructorConditionTextBox.Text;
            ConditionsTextBox.IsReadOnly = false;
            ConditionsTextBox.Focus();

            ConstructorConditionTextBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC3797F8"));
        }

        private void ConditionsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_indexOfConstructorTextBoxEdittedByTheMoment != -1)
            {
                Grid ConstructorConditionGrid =
                    UIMethods.GetUIElementChildByNumberFromTemplatedListBox(Constructor, _indexOfConstructorTextBoxEdittedByTheMoment, 0) as Grid;

                TextBox ConstructorConditionTextBox = ConstructorConditionGrid.Children[1] as TextBox;

                ConstructorConditionTextBox.Text = ConditionsTextBox.Text;
            }
        }

        private void ConditionsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!ConditionsTextBox.IsReadOnly)
            {
                Grid ConstructorConditionGrid =
                        UIMethods.GetUIElementChildByNumberFromTemplatedListBox(Constructor, _indexOfConstructorTextBoxEdittedByTheMoment, 0) as Grid;

                TextBox ConstructorConditionTextBox = ConstructorConditionGrid.Children[1] as TextBox;
                
                ConstructorConditionTextBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(defaultConditionTextBoxBorderBrush));
                
                if (ConstructorConditionTextBox.Text == "")
                {
                    ConstructorConditionTextBox.Text = constructorConditionText;
                }

                else
                {
                    var condition = ConstructorConditionTextBox.DataContext as Core.Models.Condition;

                    if (condition.Text != ConstructorConditionTextBox.Text)
                    {
                        condition.Text = ConstructorConditionTextBox.Text;

                        _wereThereAlreadySomeChangings = true;
                    }
                }

                _indexOfConstructorTextBoxEdittedByTheMoment = -1;

                DefaultConditionTextBoxState();
            }
        }




        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button DeleteButton = sender as Button;

            Result result = DeleteButton.DataContext as Result;

            for (int i = 0; i < _conditions.Count; i++)
            {
                Grid ConditionResultGrid = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(Constructor, i, 1) as Grid;

                ComboBox ConditionTextResultComboBox = ConditionResultGrid.Children[1] as ComboBox;

                ConditionTextResultComboBox.SelectedIndex = 0;
            }

            _textResults.Remove(result);
            _storage.RemoveResult(result);

            DefaultPossibleTextResultsItemsSource();
            DefaultConstructorItemsSource();

            _wereThereAlreadySomeChangings = true;
        }
    }
}
