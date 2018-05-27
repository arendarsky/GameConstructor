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
    /// Логика взаимодействия для DeveloperModeWindow.xaml
    /// </summary>
    public partial class Developer_II_Window : Window
    {
        private const string defaultQuestionText = "Текст вопроса";
        private const string defaultAnswerText = "Вариант ответа";
        private const string defaultEffectText = "Текст возможной реакции";
        private const string defaultInfluenceText = "Изменение характеристики";


        IGame _game;
        List<Question> _questions;
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

            InitializingQuestions();
        }



        private void InitializingQuestions()
        {
            try
            {
                _questions = _game.GetQuestions.ToList();
            }
            catch
            {
                _questions = new List<Question>();

                AddNewDefaultQuestion();
            }
        }



        private void DefaultQuestionListBoxSource()
        {
            QuestionsListBox.ItemsSource = null;

            QuestionsListBox.ItemsSource = _questions;
        }

        private void DefaultAnswersListBoxSource(ListBox AnswerListBox, Question question)
        {
            AnswerListBox.ItemsSource = null;

            AnswerListBox.ItemsSource = question.Answers;
        }



        private void AddNewDefaultQuestion()
        {
            _questions.Add(DefaultQuestion());

            DefaultQuestionListBoxSource();
        }

        private void AddNewDefaultAnswer(ListBox AnswerListBox, Question question)
        {
            question.Answers.Add(DefaultAnswer());

            DefaultAnswersListBoxSource(AnswerListBox, question);
        }



        private List<Influence> DefaultInfluences()
        {
            List<Influence> defaultInfluences = new List<Influence>();

            foreach (var characteristic in _game.GetCharacteristics)
            {
                defaultInfluences.Add(new Influence()
                {
                    Characteristic = characteristic
                });
            }
            
            return defaultInfluences;
        }

        private Effect DefaultEffect()
        {
            return new Effect
            {
                Body = defaultEffectText,
                Influences = DefaultInfluences()
            };
        }

        private Answer DefaultAnswer()
        {
            return new Answer
            {
                Body = defaultAnswerText,
                Effects = new List<Effect> { DefaultEffect() }
            };
        }

        private Question DefaultQuestion()
        {
            return new Question
            {
                Body = defaultQuestionText,
                Answers = new List<Answer> { DefaultAnswer() }
            };
        }



        private void QuestionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock QuestionTextBlock = sender as TextBlock;

            Question currentQuestion = QuestionTextBlock.DataContext as Question;

            int questionIndex = _questions.IndexOf(currentQuestion);

            QuestionTextBlock.Text = "Вопрос " + (questionIndex + 1).ToString();
        }

        private void AnswersListBox_Initialized(object sender, EventArgs e)
        {
            ListBox AnswersListBox = sender as ListBox;

            Question currentQuestion = AnswersListBox.DataContext as Question;

            DefaultAnswersListBoxSource(AnswersListBox, currentQuestion);
        }

        private void AnswerTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox AnswerTextBox = sender as TextBox;

            Answer answer = AnswerTextBox.DataContext as Answer;

            AnswerTextBox.Text = answer.Body;
        }

        private void ReactionsListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ReactionsListBox = sender as ListBox;

            Answer answer = ReactionsListBox.DataContext as Answer;

            ReactionsListBox.ItemsSource = answer.Effects;
        }

        private void ChangesOfCharacteristicListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ChangingsOfCharacteristicsListBox = sender as ListBox;

            Effect effect = ChangingsOfCharacteristicsListBox.DataContext as Effect;

            ChangingsOfCharacteristicsListBox.ItemsSource = effect.Influences;
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



        private void NewQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDefaultQuestion();
        }

        private void NewAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button NewAnswerButton = sender as Button;

            Grid QuestionGrid = NewAnswerButton.Parent as Grid;

            ListBox AnswerListBox = QuestionGrid.Children[3] as ListBox;

            Question question = NewAnswerButton.DataContext as Question;

            AddNewDefaultAnswer(AnswerListBox, question);
        }
    }
}
