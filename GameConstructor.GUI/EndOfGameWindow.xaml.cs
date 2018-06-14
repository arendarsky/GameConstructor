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
        private const char more = '>';
        private const char less = '<';
        private const char moreOrEqual = '⩾';
        private const char lessOrEqual = '⩽';
        private const char equal = '=';
        private const char notEqual = '≠';

        private const char falseChar = '^';
        private const char trueChar = '~';


        IStorage _storage;
        IGame _game;
        List<Characteristic> _localCharacteristics;


        private Result _textResult = null;
        private bool _restartingTheGame = false;


        Dictionary<string, string> _characteristicReverseDictionary;



        public EndOfGameWindow(IStorage storage, IGame game, List<Characteristic> localCharacteristics)
        {
            _storage = storage;
            _game = game;
            _localCharacteristics = localCharacteristics;
            
            FormingTheTextResult();

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



        private void RestoringOriginalCharacteristics()
        {
            var conditions = _game.GetConditions.ToList();

            var abbreviations = new List<string>();

            _characteristicReverseDictionary = new Dictionary<string, string>();            

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
                        abbreviations.Add(symbolParts[j]);
                    }
                }
            }

            _characteristicReverseDictionary = GeneralMethods.FillingTheEmpltyAbbreviationDictionary(abbreviations, 
                _game.GetCharacteristics.Select(ch => ch.Name));
        }



        private bool CheckingOneOperatorIteration(string operatorCondition)
        {
            var parts = operatorCondition.Split(' ');

            if (!int.TryParse(parts[0], out int int1))
            {
                Characteristic characteristic = _localCharacteristics.First(ch => ch.Name == _characteristicReverseDictionary[parts[0]]);

                int1 = characteristic.Value;
            }

            if (!int.TryParse(parts[2], out int int2))
            {
                Characteristic characteristic = _localCharacteristics.First(ch => ch.Name == _characteristicReverseDictionary[parts[2]]);

                int2 = characteristic.Value;
            }

            if (parts[1][0] == more)
            {
                return int1 > int2;
            }
            else if (parts[1][0] == less)
            {
                return int1 < int2;
            }
            else if (parts[1][0] == moreOrEqual)
            {
                return int1 >= int2;
            }
            else if (parts[1][0] == lessOrEqual)
            {
                return int1 <= int2;
            }
            else if (parts[1][0] == equal)
            {
                return int1 == int2;
            }
            else
            {
                return int1 != int2;
            }
        }
        

        private char GettingCharResultForAnIteration(string argument)
        {
            if (argument.Length > 1)
            {
                bool result = CheckingOneOperatorIteration(argument);

                if (result) { return trueChar; }

                else { return falseChar; }
            }

            return argument[0];            
        }



        private char SolvingOneOperatorStatement(string firstArgument, char mathOperator, string secondArgument)
        {
            char firstResult = GettingCharResultForAnIteration(firstArgument);
            char secondResult = GettingCharResultForAnIteration(secondArgument);

            if (mathOperator == MathLogicMethods.conjunction)
            {
                if (firstResult == trueChar && secondResult == trueChar)
                {
                    return trueChar;
                }
                else
                {
                    return falseChar;
                }
            }
            else
            {
                if (firstResult == falseChar && secondResult == falseChar)
                {
                    return falseChar;
                }
                else
                {
                    return trueChar;
                }
            }
        }



        private List<string> SolvingOneTypeOfLogicalOperators(List<string> partsOfCondition, char typeOfLogicalOperator)
        {
            while (true)
            {
                int indexOfLogicalOperator = partsOfCondition.IndexOf(typeOfLogicalOperator.ToString());

                if (indexOfLogicalOperator == -1) { break; }

                char mathCharResult = SolvingOneOperatorStatement(partsOfCondition[indexOfLogicalOperator - 1], partsOfCondition[indexOfLogicalOperator][0], partsOfCondition[indexOfLogicalOperator + 1]);

                partsOfCondition[indexOfLogicalOperator] = mathCharResult.ToString();

                partsOfCondition.RemoveAt(indexOfLogicalOperator + 1);
                partsOfCondition.RemoveAt(indexOfLogicalOperator - 1);
            }

            return partsOfCondition;
        }



        private char CheckingTextConditionWithoutBrackets(string condition)
        {
            var parts = MathLogicMethods.SplittingConjuctionAndDisjunctionsFromAString(condition);

            if (parts.Count() != 1)
            {
                parts = SolvingOneTypeOfLogicalOperators(parts, MathLogicMethods.conjunction);
                parts = SolvingOneTypeOfLogicalOperators(parts, MathLogicMethods.disjunction);

                return parts[0][0];
            }

            else
            {
                bool result = CheckingOneOperatorIteration(parts[0]);

                if (result)
                {
                    return trueChar;
                }
                else
                {
                    return falseChar;
                }
            }            
        }


        private void FormingTheTextResult()
        {
            RestoringOriginalCharacteristics();

            var conditions = _game.GetConditions.ToList();

            for (int i = 0; i < conditions.Count() - 1; i++)
            {
                var condition = conditions[i];

                var charResult = CheckingTextConditionWithoutBrackets(condition.Text);

                if (charResult == trueChar)
                {
                    _textResult = condition.Result;

                    break;
                }
            }

            if (_textResult == null)
            {
                _textResult = conditions.Last().Result;
            }
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
