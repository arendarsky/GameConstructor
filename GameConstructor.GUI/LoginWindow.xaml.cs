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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameConstructor.Core;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;

namespace GameConstructor.GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string defaultLoginText = "Введите логин";
        private const string defaultPasswordText = "Введите пароль";

        IStorage _storage;


        public LoginWindow()
        {
            _storage = Factory.Instance.GetDatabaseStorage();

            InitializeComponent();

            RestartTheWindow();
        }



        private void PlayingModeButton_Click(object sender, RoutedEventArgs e)
        {
            PlayingModeWindow playingModeWindow = new PlayingModeWindow(_storage);

            playingModeWindow.Show();

            Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (IfSomeValuesAreDefault())
            {
                MessageBox.Show("Заполните, пожалуйста, все поля.", "Ошибка!");
            }

            else
            {
                string login = LoginTextBox.Text;

                string password = User.GetHash(PasswordBox.Password);

                User user = _storage.Users.Items.FirstOrDefault(u => (
                    u.Login.ToLower() == login.ToLower() & u.Password == password));

                if (user != null)
                {
                    ProfileWindow profileWindow = new ProfileWindow(_storage, user);

                    profileWindow.Show();

                    Close();
                }

                else
                {
                    MessageBox.Show("Неправильный логин или пароль!", "Ошибка!");

                    RestartTheWindow();
                }
            }
        }

        private void RegisterTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(_storage);

            registerWindow.ShowDialog();
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



        private bool IfSomeValuesAreDefault()
        {
            if (LoginTextBox.Text == defaultLoginText || PasswordBox.Password == null || PasswordBox.Password == "")
            {
                return true;
            }

            return false;
        }

        private void RestartTheWindow()
        {
            LoginTextBox.Text = defaultLoginText;
            LoginTextBox.Foreground = Brushes.Gray;

            PasswordTextBox.Text = defaultPasswordText;
            PasswordTextBox.Foreground = Brushes.Gray;
            PasswordTextBox.Visibility = Visibility.Visible;

            PasswordBox.Password = "";
            PasswordBox.Visibility = Visibility.Hidden;
        }



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(null, null);
            }
            
            else if (!LoginTextBox.IsFocused && e.Key == Key.Tab)
            {
                LoginTextBox.Focus();

                e.Handled = true;
            }
        }
    }
}
