using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
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

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для EndOfGameWindow.xaml
    /// </summary>
    public partial class EndOfGameWindow : Window
    {
        IStorage _storage;
        IGame _game;
        List<Characteristic> _localCharacteristics;


        private Result _textResult = null;
        private bool _restartingTheGame = false;



        public EndOfGameWindow(IStorage storage, IGame game, List<Characteristic> localCharacteristics)
        {
            _storage = storage;
            _game = game;
            _localCharacteristics = localCharacteristics;
            
            FormingTheResult();

            InitializeComponent();            
        }

        

        private void UpdatePopularity()
        {
            int popularity = _game.Popularity;

            popularity++;

            _game.UpdatePopularity(popularity);

            User user = new User { Id = _game.UserId };

            _storage.SaveGame(user, _game);
        }



        private void FormingTheResult()
        {
            var conditions = _game.GetConditions.ToList();
            var results = _game.GetResults.ToList();

            var characteristicReverseDictionary = new Dictionary<string, string>();

            for (int i = 0; i < conditions.Count - 1; i++)
            {
                string textOfConditionInAbbreviations = conditions[i].Text;

                var symbolParts = textOfConditionInAbbreviations
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(' ');

                for (int j = 0; j < symbolParts.Length; j+=2)
                {
                    if (!int.TryParse(symbolParts[j], out int t))
                    {
                        characteristicReverseDictionary[symbolParts[j]] = "";
                    }
                }
            }

            characteristicReverseDictionary = GeneralMethods.FillingTheEmpltyAbbreviationDictionary(characteristicReverseDictionary, 
                _game.GetCharacteristics.Select(ch => ch.Name));

            //for (int i = 0; i < conditions.Count() - 1; i++)
            //{
                
            //}

            _textResult = conditions.Last().Result;
        }



        private void PlayToDifferentGames_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            _restartingTheGame = true;

            Close();
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_restartingTheGame)
            {
                IGame game = _game;
                game.UpdatePicture(_game.Picture);

                UpdatePopularity();

                DescriptionOfGameWindow discriptionOfGameWindow = new DescriptionOfGameWindow(_storage, game);

                discriptionOfGameWindow.Show();
            }

            else
            {
                UpdatePopularity();

                PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

                playingModeWindow.Show();
            }
        }



        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ResultTextBlock = sender as TextBlock;

            ResultTextBlock.Text = _textResult.Body;
        }
    }
}
