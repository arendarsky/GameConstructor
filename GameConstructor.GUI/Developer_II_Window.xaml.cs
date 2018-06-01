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
        private const string defaultInfluenceText = " изменяется?";
        private const int defaultInfluenceValue = 0;


        IGame _game;
        List<Question> _questions;
        bool _wereThereAlreadySomeChangings;
        User _user;
        bool _goingToTheNextDeveloperWindow = false;
        bool _goingToThePreviousDeveloperWindow = false;

        IStorage _storage;



        public Developer_II_Window(User user, IGame game, IStorage storage, bool wereThereAlreadySomeChangings)
        {
            _user = user;
            _storage = storage;
            _game = game;
            _wereThereAlreadySomeChangings = wereThereAlreadySomeChangings;

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
            }

            if (_questions.Count == 0)
            {
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

        private void DefaultReactionsListBoxSource(ListBox ReactionsListBox, Answer answer)
        {
            ReactionsListBox.ItemsSource = null;

            ReactionsListBox.ItemsSource = answer.Effects;
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

        private void AddNewDefaultReaction(ListBox ReactionsListBox, Answer answer)
        {
            answer.Effects.Add(DefaultEffect());

            DefaultReactionsListBoxSource(ReactionsListBox, answer);
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

        private void QuestionTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox QuestionTextBox = sender as TextBox;

            Question currentQuestion = QuestionTextBox.DataContext as Question;

            QuestionTextBox.Text = currentQuestion.Body;

            if (currentQuestion.Body == defaultQuestionText)
            {
                QuestionTextBox.Foreground = Brushes.Gray;
            }
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

            if (answer.Body == defaultAnswerText)
            {
                AnswerTextBox.Foreground = Brushes.Gray;
            }
        }

        private void ReactionsListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ReactionsListBox = sender as ListBox;

            Answer answer = ReactionsListBox.DataContext as Answer;

            DefaultReactionsListBoxSource(ReactionsListBox, answer);
        }

        private void ReactionTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox ReactionTextBox = sender as TextBox;

            Effect reaction = ReactionTextBox.DataContext as Effect;

            ReactionTextBox.Text = reaction.Body;

            if (reaction.Body == defaultEffectText)
            {
                ReactionTextBox.Foreground = Brushes.Gray;
            }
        }

        private void ChangesOfCharacteristicListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ChangingsOfCharacteristicsListBox = sender as ListBox;

            Effect effect = ChangingsOfCharacteristicsListBox.DataContext as Effect;

            ChangingsOfCharacteristicsListBox.ItemsSource = effect.Influences;
        }

        private void ChangeOfCharacteristicLTextBox_Initialized(object sender, EventArgs e)
        {
            TextBox ChangeOfCharacteristicTextBox = sender as TextBox;

            Influence influence = ChangeOfCharacteristicTextBox.DataContext as Influence;

            if (influence.Value == defaultInfluenceValue)
            {
                ChangeOfCharacteristicTextBox.Text = 
                    influence.Characteristic.Name + defaultInfluenceText;

                ChangeOfCharacteristicTextBox.Foreground = Brushes.Gray;
            }

            else
            {
                ChangeOfCharacteristicTextBox.Text = influence.Value.ToString();
            }
        }



        private void QuestionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox QuestionTextBox = sender as TextBox;

            if (QuestionTextBox.Text == defaultQuestionText)
            {
                QuestionTextBox.Text = "";

                QuestionTextBox.Foreground = Brushes.Black;
            }
        }

        private void QuestionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox QuestionTextBox = sender as TextBox;

            if (QuestionTextBox.Text == "")
            {
                QuestionTextBox.Text = defaultQuestionText;

                QuestionTextBox.Foreground = Brushes.Gray;
            }
        }

        private void AnswerTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox AnswerTextBox = sender as TextBox;

            if (AnswerTextBox.Text == defaultAnswerText)
            {
                AnswerTextBox.Text = "";

                AnswerTextBox.Foreground = Brushes.Black;
            }
        }

        private void AnswerTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox AnswerTextBox = sender as TextBox;

            if (AnswerTextBox.Text == "")
            {
                AnswerTextBox.Text = defaultAnswerText;

                AnswerTextBox.Foreground = Brushes.Gray;
            }
        }

        private void ReactionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox ReactionTextBox = sender as TextBox;

            if (ReactionTextBox.Text == defaultEffectText)
            {
                ReactionTextBox.Text = "";

                ReactionTextBox.Foreground = Brushes.Black;
            }
        }

        private void ReactionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox ReactionTextBox = sender as TextBox;

            if (ReactionTextBox.Text == "")
            {
                ReactionTextBox.Text = defaultEffectText;

                ReactionTextBox.Foreground = Brushes.Gray;
            }
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
            bool cancelation = false;

            if (!_goingToTheNextDeveloperWindow && !_goingToThePreviousDeveloperWindow)
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
            Developer_I_Window developer_I_Window = new Developer_I_Window(_user, _game, _storage, _wereThereAlreadySomeChangings);

            developer_I_Window.Show();
        }

        private void GoingToTheNextDeveloperWindow()
        {
            Developer_III_Window developer_III_Window = new Developer_III_Window(_user, _game, _storage, _wereThereAlreadySomeChangings);

            developer_III_Window.Show();
        }

        private void GoingBackToProfileWindow()
        {
            ProfileWindow profileWindow = new ProfileWindow(_storage, _user);

            profileWindow.Show();
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

            ListBox AnswerListBox = QuestionGrid.Children[4] as ListBox;

            Question question = NewAnswerButton.DataContext as Question;

            AddNewDefaultAnswer(AnswerListBox, question);
        }

        private void NewReactionButton_Click(object sender, RoutedEventArgs e)
        {
            Button NewReactionButton = sender as Button;

            Grid AnswerGrid = NewReactionButton.Parent as Grid;

            ListBox ReactionsListBox = AnswerGrid.Children[3] as ListBox;

            Answer answer = NewReactionButton.DataContext as Answer;

            AddNewDefaultReaction(ReactionsListBox, answer);
        }        
    }
}
