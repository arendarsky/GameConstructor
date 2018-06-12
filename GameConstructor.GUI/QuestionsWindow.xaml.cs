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
    /// Логика взаимодействия для QuestionsWindow.xaml
    /// </summary>
    public partial class QuestionsWindow : Window
    {
        IStorage _storage;
        IGame _game;
        Question _question;
        List<Characteristic> _localCharacteristics;
        int _numberOfQuestionsShown;

        public QuestionsWindow(IStorage storage, IGame game, int numberOfQuestionsShown, List<Characteristic> localCharacteristics)
        {
            _storage = storage;
            _game = game;
            _question = _game.GetQuestions.ToList()[numberOfQuestionsShown];
            _localCharacteristics = localCharacteristics;
            _numberOfQuestionsShown = numberOfQuestionsShown;

            InitializeComponent();
        }

        public QuestionsWindow(IStorage storage, IGame game)
        {
            _storage = storage;
            _game = _storage.OpenGame(game);
            _question = _game.GetQuestions.ToList()[0];
            _localCharacteristics = CopyCharacteristics(_game.GetCharacteristics).ToList();
            _numberOfQuestionsShown = 0;

            InitializeComponent();
        }



        private IEnumerable<Characteristic> CopyCharacteristics(IEnumerable<Characteristic> originCharacteristics)
        {
            return originCharacteristics
                .Select(ch => new Characteristic
                {
                    Name = ch.Name,
                    Value = ch.Value
                });
        }



        private void QuestionNumberTextBlock_Initialized(object sender, EventArgs e)
        {
            QuestionNumberTextBlock.Text += (_numberOfQuestionsShown + 1).ToString();
        }

        private void QuestionBodyTextBlock_Initialized(object sender, EventArgs e)
        {
            QuestionBodyTextBlock.Text = _question.Body;
        }



        private void AnswersListBox_Initialized(object sender, EventArgs e)
        {
            AnswersListBox.ItemsSource = _question.Answers;
        }

        private void CharacteristicsListBox_Initialized(object sender, EventArgs e)
        {
            CharacteristicsListBox.ItemsSource = _localCharacteristics;
        }



        private void CharacteristicValueTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CharacteristicValueTextBlock = sender as TextBlock;

            Characteristic characteristic = CharacteristicValueTextBlock.DataContext as Characteristic;

            CharacteristicValueTextBlock.Text = characteristic.Value.ToString();
        }

        private void CharacteristicNameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CharacteristicNameTextBlock = sender as TextBlock;

            Characteristic characteristic = CharacteristicNameTextBlock.DataContext as Characteristic;

            CharacteristicNameTextBlock.Text = characteristic.Name.ToString();
        }


        private void AnswerBodyTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock AnswerBodyTextBlock = sender as TextBlock;

            Answer answer = AnswerBodyTextBlock.DataContext as Answer;

            AnswerBodyTextBlock.Text = answer.Body;
        }



        private void AnswersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AnswersListBox.SelectedItem is Answer answer)
            {
                ResultAfterQuestionWindow resultAfterQuestionWindow = new ResultAfterQuestionWindow(_storage, _game, _numberOfQuestionsShown, _localCharacteristics, answer);

                resultAfterQuestionWindow.Show();

                Close();
            }
        }
    }
}
