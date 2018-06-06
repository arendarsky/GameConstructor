using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
using GameConstructor.Core.SpecialMethods;
using GameConstructor.GUI.UICore;
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
        private const string defaultInfluenceTextPlural = " изменяются?";
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

            DefaultQuestionListBoxSource();

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
                var characeristicName = influence.Characteristic.Name;

                if (characeristicName[characeristicName.Length - 1] == 'ы')
                {
                    ChangeOfCharacteristicTextBox.Text = characeristicName + defaultInfluenceTextPlural;
                }

                else
                {
                    ChangeOfCharacteristicTextBox.Text = characeristicName + defaultInfluenceText;
                }

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

            Question question = QuestionTextBox.DataContext as Question;

            question.Body = QuestionTextBox.Text;
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
            
            Answer answer = AnswerTextBox.DataContext as Answer;

            answer.Body = AnswerTextBox.Text;
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

            Effect reaction = ReactionTextBox.DataContext as Effect;

            reaction.Body = ReactionTextBox.Text;
        }

        private void ChangeOfCharacteristicLTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox ChangeOfCharacteristicLTextBox = sender as TextBox;

            Influence influence = ChangeOfCharacteristicLTextBox.DataContext as Influence;

            if (influence.Value == defaultInfluenceValue)
            {
                ChangeOfCharacteristicLTextBox.Text = "";
            }           

            ChangeOfCharacteristicLTextBox.Foreground = Brushes.Black;
        }

        private void ChangeOfCharacteristicLTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox ChangeOfCharacteristicLTextBox = sender as TextBox;

            Influence influence = ChangeOfCharacteristicLTextBox.DataContext as Influence;

            try { influence.Value = int.Parse(ChangeOfCharacteristicLTextBox.Text); }
            catch { }

            ChangeOfCharacteristicLTextBox.Text = influence.Value.ToString();
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
            if (CheckingIfEveryFieldIsFilledCorrectly())
            {
                IfThereWereAnyChangesMadeByUser();

                _game.UpdateQuestions(_questions);

                return true;
            }

            return false;
        }



        private bool CheckingIfEveryFieldIsFilledCorrectly()
        {
            if (!AreThereAtLeastOneFieldOfEveryType())
            {
                return false;
            }

            else if (!AreTheObligatoryFieldsFilled())
            {
                return false;
            }

            else if (!AreTheFieldsFilledDifferently())
            {
                return false;
            }

            return true;
        }



        private bool AreThereAtLeastOneFieldOfEveryType()
        {
            if (_questions == null || _questions.Count == 0)
            {
                MessageBox.Show("Вопросов не может вообще не быть. Добавьте хотя бы один.", "Ошибка!");

                return false;
            }
            
            foreach (var question in _questions)
            {
                if (question.Answers == null || question.Answers.Count == 0)
                {
                    MessageBox.Show($"В вопросе {_questions.IndexOf(question) + 1} нет вариантов ответа. Так нельзя, добавьте хотя бы один.", "Ошибка!");

                    return false;
                }

                foreach (var answer in question.Answers)
                {
                    if (answer.Effects == null || answer.Effects.Count == 0)
                    {
                        MessageBox.Show($"В вопросе {_questions.IndexOf(question) + 1} есть вариант ответа с нулевым числом реакций. Исправьте это. По крайней мере одна реакция должна быть в каждом ответе.", "Ошибка!");

                        return false;
                    }
                }
            }

            return true;            
        }



        private bool AreTheObligatoryFieldsFilled()
        {
            if (UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(QuestionsListBox, 1, defaultQuestionText, "Текст вопроса — обязательный аттрибут. Заполните все пустые поля либо удалите ненужные вопросы."))
            {
                return false;
            }

            else
            {
                foreach (var question in _questions)
                {
                    int questionIndex = _questions.IndexOf(question);

                    ListBox AnswerListBox = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(QuestionsListBox, questionIndex, 4) as ListBox;

                    if (UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(AnswerListBox, 0, defaultAnswerText, "Текст ответа — обязательный аттрибут. Заполните все пустые поля либо удалите ненужные ответы."))
                    {
                        return false;
                    }

                    else
                    {
                        foreach (var answer in question.Answers)
                        {
                            int answerIndex = question.Answers.IndexOf(answer);

                            ListBox ReactionListBox = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(AnswerListBox, answerIndex, 3) as ListBox;

                            if (UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(ReactionListBox, 0, defaultEffectText, "Текст возможной реакции — обязательный аттрибут. Заполните все пустые поля либо удалите ненужные реакции."))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }



        private bool AreTheFieldsFilledDifferently()
        {
            var questionsBodies = _questions.Select(qu => qu.Body.ToUpperInvariant());

            if (GeneralMethods.AreThereSameElementsInTheStringCollection(questionsBodies, out string questionElement))
            {
                UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(QuestionsListBox, 1, questionElement, "По крайней мере два Ваших вопроса совпадают. Так нельзя — как же игроки будут их различать?");

                return false;
            }

            foreach (var question in _questions)
            {
                int questionIndex = _questions.IndexOf(question);

                var answerBodies = question.Answers.Select(an => an.Body.ToUpperInvariant());

                ListBox AnswerListBox = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(QuestionsListBox, questionIndex, 4) as ListBox;

                if (GeneralMethods.AreThereSameElementsInTheStringCollection(answerBodies, out string answerElement))
                {
                    UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(AnswerListBox, 0, answerElement, $"В вопросе {(questionIndex + 1).ToString()} есть одинаковые ответы. Измените их либо удалите ненужные.");

                    return false;
                }

                foreach (var answer in question.Answers)
                {
                    int answerIndex = question.Answers.IndexOf(answer);

                    var reactionBodies = answer.Effects.Select(re => re.Body.ToUpperInvariant());

                    if (GeneralMethods.AreThereSameElementsInTheStringCollection(reactionBodies, out string reactionElement))
                    {
                        ListBox ReactionListBox = UIMethods.GetUIElementChildByNumberFromTemplatedListBox(AnswerListBox, answerIndex, 3) as ListBox;

                        UIMethods.FindCurrentTextInTextBoxesOfTheTemplatedListBox(ReactionListBox, 0, reactionElement, $"В вопросе {(questionIndex + 1).ToString()} есть реакции с совпадающим текстом. Измените их либо удалите ненужные.");

                        return false;
                    }
                }
            }

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

            List<Question> questions = _questions;

            if (questions.Count == 1 && questions[0] == DefaultQuestion()) { questions = null; }

            if (GeneralMethods.CheckingWhetherCollectionsHaveTheSameValues<Question>(questions, _game.GetQuestions))
            {
                return false;
            }

            _wereThereAlreadySomeChangings = true;

            return true;
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



        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            Button DeleteQuestionButton = sender as Button;

            Question question = DeleteQuestionButton.DataContext as Question;

            _questions.Remove(question);
            _storage.RemoveQuestion(question);

            DefaultQuestionListBoxSource();
        }

        private void DeleteAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button DeleteAnswerButton = sender as Button;

            Answer answer = DeleteAnswerButton.DataContext as Answer;

            Grid AnswerUIGrid = DeleteAnswerButton.Parent as Grid;

            ListBox AnswersListBox = UIMethods.GettingTemplatedListBoxByListBoxItemsGrid(AnswerUIGrid);

            Question question = AnswersListBox.DataContext as Question;

            question.Answers.Remove(answer);

            _storage.RemoveAnswer(answer);

            DefaultAnswersListBoxSource(AnswersListBox, question);
        }

        private void DeleteReactionButton_Click(object sender, RoutedEventArgs e)
        {
            Button DeleteReactionButton = sender as Button;

            Effect effect = DeleteReactionButton.DataContext as Effect;

            Grid ReactionUIGrid = DeleteReactionButton.Parent as Grid;

            ListBox ReactionsListBox = UIMethods.GettingTemplatedListBoxByListBoxItemsGrid(ReactionUIGrid);

            Answer answer = ReactionsListBox.DataContext as Answer;

            answer.Effects.Remove(effect);

            _storage.RemoveEffect(effect);

            DefaultReactionsListBoxSource(ReactionsListBox, answer);
        }
    }
}
