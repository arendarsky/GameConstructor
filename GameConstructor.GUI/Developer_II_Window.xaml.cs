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

        public Developer_II_Window()
        {
            InitializeComponent();

            QuestionsListBox.ItemsSource = new List<List<List<int>>> { new List<List<int>> { new List<int> { 1 } }, new List<List<int>> { new List<int> { 2, 3, 4 }, new List<int> { 1 } }, new List<List<int>> { new List<int> { 5, 7 }, new List<int> { 6 }, new List<int> { 7, 8, 9, 10 } } };
        }


        private void AnswersListBox_Initialized(object sender, EventArgs e)
        {
            ListBox ListBox = sender as ListBox;
            ListBox.ItemsSource = ListBox.DataContext as List<List<int>>;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
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
    }
}
