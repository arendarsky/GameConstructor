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
        private const string defaultLoginText = "Введите логин";
        private const string defaultPasswordText = "Введите пароль";
        private const string defaultRepeatPasswordText = "Повторите пароль";
        private const string defaultEmail = "Укажите Email";


        IStorage _storage;
        

        public RegisterWindow(IStorage storage)
        {
            _storage = storage;

            InitializeComponent();

            RestartTheWindow();
        }


        private void RestartTheWindow()
        {
            LoginTextBox.Text = defaultLoginText;
            LoginTextBox.Foreground = Brushes.Gray;

            PasswordTextBox.Text = defaultPasswordText;
            PasswordTextBox.Foreground = Brushes.Gray;
            PasswordTextBox.Visibility = Visibility.Visible;

            RepeatPasswordTextBox.Text = defaultRepeatPasswordText;
            RepeatPasswordTextBox.Foreground = Brushes.Gray;
            RepeatPasswordTextBox.Visibility = Visibility.Visible;

            EmailTextBox.Text = defaultEmail;
            EmailTextBox.Foreground = Brushes.Gray;

            PasswordBox.Password = "";
            PasswordBox.Visibility = Visibility.Hidden;

            RepeatPasswordBox.Password = "";
            RepeatPasswordBox.Visibility = Visibility.Hidden;
        }



        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == defaultLoginText)
            {
                LoginTextBox.Text = "";

                LoginTextBox.Foreground = Brushes.Black;
            }
        }

        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == "")
            {
                LoginTextBox.Text = defaultLoginText;

                LoginTextBox.Foreground = Brushes.Gray;
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Visibility = Visibility.Hidden;

            PasswordBox.Visibility = Visibility.Visible;

            PasswordBox.Focus();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == null || PasswordBox.Password == "")
            {
                PasswordBox.Visibility = Visibility.Hidden;

                PasswordTextBox.Visibility = Visibility.Visible;
            }
        }

        private void RepeatPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RepeatPasswordTextBox.Visibility = Visibility.Hidden;

            RepeatPasswordBox.Visibility = Visibility.Visible;

            RepeatPasswordBox.Focus();
        }

        private void RepeatPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RepeatPasswordBox.Password == null || RepeatPasswordBox.Password == "")
            {
                RepeatPasswordBox.Visibility = Visibility.Hidden;

                RepeatPasswordTextBox.Visibility = Visibility.Visible;
            }
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text == defaultEmail)
            {
                EmailTextBox.Text = "";

                EmailTextBox.Foreground = Brushes.Black;
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text == "")
            {
                EmailTextBox.Text = defaultEmail;

                EmailTextBox.Foreground = Brushes.Gray;
            }
        }



        private bool IfSomeValuesAreDefault()
        {
            if (LoginTextBox.Text == defaultLoginText || PasswordBox.Password == null || PasswordBox.Password == "" || RepeatPasswordBox.Password == null || RepeatPasswordBox.Password == "" || EmailTextBox.Text == defaultEmail)
            {
                return true;
            }

            return false;
        }



        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (IfSomeValuesAreDefault())
            {
                MessageBox.Show("Заполните, пожалуйста, все поля.", "Ошибка!");
            }

            else if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка!");

                RestartTheWindow();
            }

            else if (_storage.Users.Items.FirstOrDefault(u => u.Login.ToLower() == LoginTextBox.Text.ToLower()) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка!");

                RestartTheWindow();
            }

            else
            {
                User NewUser = new User
                {
                    Login = LoginTextBox.Text,
                    Password = User.GetHash(PasswordBox.Password),
                    Email = EmailTextBox.Text,
                    Games = new List<Game>()
                };

                _storage.SaveUser(NewUser);

                DialogResult = true;
            }
        }
    }
}
