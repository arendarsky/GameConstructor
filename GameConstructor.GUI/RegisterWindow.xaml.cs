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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IStorage _storage;
        public RegisterWindow(IStorage storage)
        {
            _storage = storage;
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = User.GetHash(PasswordTextBox.Password);
            string passwordConf = User.GetHash(RepeatPasswordTextBox.Password);
            if (string.IsNullOrWhiteSpace(login))
            {
                LoginTextBox.Focus();
                return;
            }
            if (_storage.Users.Items.FirstOrDefault(u => u.Login.ToLower() == login.ToLower()) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                PasswordTextBox.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(RepeatPasswordTextBox.Password))
            {
                RepeatPasswordTextBox.Focus();
                return;
            }
            if (password != passwordConf)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка");
                return;
            }
            User NewUser = new User
            {
                Login = login,
                Password = password,
                Email = EmailTextBox.Text,
                Games = new List<Game>()
            };
            _storage.SaveUser(NewUser);
            DialogResult = true;
        }
    }
}
