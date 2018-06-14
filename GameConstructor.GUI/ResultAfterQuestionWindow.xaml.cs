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
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для ResultAfterQuestionWindow.xaml
    /// </summary>
    public partial class ResultAfterQuestionWindow : Window
    {
        private IStorage _storage;
        private IGame _game;
        private int _numberOfQuestionsShown;
        private List<Characteristic> _localCharacteristics;
        private Answer _answer;

        private Effect _reaction;


        private bool _goingToTheNextQuestionWindow = false;
        private bool _goingToTheEndOfTheGameWindow = false;
        bool _testingMode;
        bool _wereThereAlreadySomeChangings;



        public ResultAfterQuestionWindow(bool testingMode, IStorage storage, IGame game, int numberOfQuestionsShown, List<Characteristic> localCharacteristics, Answer answer, bool wereThereAlreadySomeChangings)
        {
            _wereThereAlreadySomeChangings = wereThereAlreadySomeChangings;
            _testingMode = testingMode;
            _storage = storage;
            _game = game;
            _numberOfQuestionsShown = numberOfQuestionsShown;
            _localCharacteristics = localCharacteristics;
            _answer = answer;

            ChoosingRandomlyAReactionForAnswer();

            InitializeComponent();            
        }


        private void ChoosingRandomlyAReactionForAnswer()
        {
            int amountOfReactions = _answer.Effects.Count();

            _reaction = _answer.Effects[(new Random()).Next(amountOfReactions)];
        }
        


        private void CharacteristicsListBox_Initialized(object sender, EventArgs e)
        {
            CharacteristicsListBox.ItemsSource = _localCharacteristics;
        }



        private void IntermediateResultTextBlock_Initialized(object sender, EventArgs e)
        {
            IntermediateResultTextBlock.Text = _reaction.Body;
        }



        private void CharacteristicNameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock CharacteristicNameTextBlock = sender as TextBlock;

            Characteristic characteristic = CharacteristicNameTextBlock.DataContext as Characteristic;

            CharacteristicNameTextBlock.Text = characteristic.Name + ":";
        }

        private void InfluenceValueTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock InfluenceValueTextBlock = sender as TextBlock;

            Grid CharacteristicGrid = InfluenceValueTextBlock.Parent as Grid;

            TextBlock CharacteristicValueTextBlock = CharacteristicGrid.Children[0] as TextBlock;

            Characteristic characteristic = InfluenceValueTextBlock.DataContext as Characteristic;

            Influence influence = _reaction.Influences.FirstOrDefault(inf => inf.Characteristic.Name == characteristic.Name);

            characteristic.Value += influence.Value;

            CharacteristicValueTextBlock.Text = characteristic.Value.ToString();
            
            if (influence.Value > 0)
            {
                InfluenceValueTextBlock.Foreground = Brushes.ForestGreen;

                InfluenceValueTextBlock.Text = "(+" + influence.Value + ")";
            }

            else if (influence.Value < 0)
            {
                InfluenceValueTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF2323"));

                InfluenceValueTextBlock.Text = "(" + influence.Value + ")";
            }

            else
            {
                InfluenceValueTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF64606B"));

                InfluenceValueTextBlock.Text = "(" + influence.Value + ")";
            }
        }

        

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_game.GetQuestions.Count() == _numberOfQuestionsShown + 1)
            {
                _goingToTheEndOfTheGameWindow = true;

                Close();
            }

            else
            {
                _numberOfQuestionsShown++;
                _goingToTheNextQuestionWindow = true;                

                Close();
            }
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_goingToTheEndOfTheGameWindow)
            {
                EndOfGameWindow endOfGameWindow = new EndOfGameWindow(_testingMode, _storage, _game,
                    _localCharacteristics, _wereThereAlreadySomeChangings);

                endOfGameWindow.Show();
            }

            else if (_goingToTheNextQuestionWindow)
            {
                QuestionsWindow questionsWindow = new QuestionsWindow(_testingMode, _storage, _game,
                    _numberOfQuestionsShown, _localCharacteristics, _wereThereAlreadySomeChangings);

                questionsWindow.Show();
            }

            else
            {
                if (!_testingMode)
                {
                    PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

                    _storage.CloseGame();

                    playingModeWindow.Show();
                }
                else
                {
                    Developer_III_Window developerWindow = new Developer_III_Window(
                        _storage.Users.Items.First(u => u.Id == _game.UserId), 
                        _game, _storage, _wereThereAlreadySomeChangings);
                    developerWindow.Show();
                }
            }
        }
    }
}
