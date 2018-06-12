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

        

        public ResultAfterQuestionWindow(IStorage storage, IGame game, int numberOfQuestionsShown, List<Characteristic> localCharacteristics, Answer answer)
        {
            _storage = storage;
            _game = game;
            _numberOfQuestionsShown = numberOfQuestionsShown;
            _localCharacteristics = localCharacteristics;
            _answer = answer;

            InitializeComponent();
        }



        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            QuestionsWindow questionsWindow = new QuestionsWindow(null, null);

            questionsWindow.Show();

            Hide();            
        }
    }
}
