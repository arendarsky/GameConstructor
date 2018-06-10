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
        public QuestionsWindow()
        {
            InitializeComponent();
        }

        private void AnswersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //надо добавить вопросы, так ругается
            //if (AnswersListBox.SelectedItem is Answer answer)
            {
                ResultAfterQuestionWindow resultAfterQuestionWindow = new ResultAfterQuestionWindow();

                resultAfterQuestionWindow.Show();

                Hide();

            }

        }
    }
}
