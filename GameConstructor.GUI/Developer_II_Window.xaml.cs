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
    /// Логика взаимодействия для DeveloperModeWindow.xaml
    /// </summary>
    public partial class Developer_II_Window : Window
    {
        int _questionNumber = 1;
        int _answerNumber = 1;


        IGame _game;
        bool _wereThereAlreadySomeChangings;

        bool _goingToTheNextDeveloperWindow = false;
        bool _goingToThePreviousDeveloperWindow = false;

        Context _context;
        


        public Developer_II_Window(IGame game, Context context, bool wereThereAlreadySomeChangings)
        {
            _game = game;
            _wereThereAlreadySomeChangings = wereThereAlreadySomeChangings;

            _context = context;

            InitializeComponent();

            QuestionsListBox.ItemsSource = new List<List<List<int>>> { new List<List<int>> { new List<int> { 1 } }, new List<List<int>> { new List<int> { 2, 3, 4 }, new List<int> { 1 } }, new List<List<int>> { new List<int> { 5, 7 }, new List<int> { 6 }, new List<int> { 7, 8, 9, 10 } } };
        }


        private void AnswersListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ListBox = sender as ListBox;
            ListBox.ItemsSource = ListBox.DataContext as List<List<int>>;
        }


        private void AnswerTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox TextBox = sender as TextBox;
            TextBox.Text = "Вариант ответа " + _answerNumber;

            _answerNumber++;
        }


        private void QuestionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TextBlock = sender as TextBlock;
            TextBlock.Text = "Вопрос " + _questionNumber;

            _questionNumber++;

            _answerNumber = 1;
        }


        private void ReactionsListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ListBox = sender as ListBox;
            ListBox.ItemsSource = ListBox.DataContext as List<int>;
        }


        private void ChangesOfCharacteristicListBox_Initialized(object sender, EventArgs e)
        {
            (sender as ListBox).ItemsSource = new int[3] { 0, 0, 0 };
        }


        private void PreviousWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamePartialSave())
            {
                _goingToThePreviousDeveloperWindow = true;

                Close();
            }
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
            return true;
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_goingToTheNextDeveloperWindow && !_goingToThePreviousDeveloperWindow)
            {
                if (IfThereWereAnyChangesMadeByUser())
                {
                    var messageBoxResult = MessageBox.Show("Вы уверены, что хотите покинуть окно разработки? Никакие текущие изменения не будут сохранены!",
                    "Предупреждение!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);

                    if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                    {
                        e.Cancel = true;
                    }
                }
            }

            else if (_goingToTheNextDeveloperWindow)
            {
                GoingToTheNextDeveloperWindow();
            }

            else
            {
                GoingToThePreviousDeveloperWindow();
            }
        }

        private void GoingToThePreviousDeveloperWindow()
        {
            Developer_I_Window developer_I_Window = new Developer_I_Window(_game, _context, _wereThereAlreadySomeChangings);

            developer_I_Window.Show();
        }

        private void GoingToTheNextDeveloperWindow()
        {
            Developer_III_Window developer_III_Window = new Developer_III_Window(_game, _context, _wereThereAlreadySomeChangings);

            developer_III_Window.Show();
        }



        private bool IfThereWereAnyChangesMadeByUser()
        {
            if (_wereThereAlreadySomeChangings) { return true; }

            return false;
        }
    }
}
