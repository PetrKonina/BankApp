﻿using System;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Registration page
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();

            DoubleAnimation regAnimation = new DoubleAnimation();

            regAnimation.From = 0;
            regAnimation.To = 300;
            regAnimation.Duration = TimeSpan.FromSeconds(3);
            regButton.BeginAnimation(Button.WidthProperty, regAnimation);
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            bool loginCorrect = false, passwordCorrect = false, passwordRepeatCorrect = false, emailCorrect = false;
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Password.Trim();
            string passwordRepeat = textBoxPasswordRepeat.Password.Trim();
            string email = textBoxEmail.Text.ToLower().Trim();

            if ((login.Length > 5) && (login.Length < 15))
            {
                loginCorrect = true;
                textBoxLogin.Background = Brushes.White;
            }
            else
            {
                textBoxLogin.ToolTip = "login should be more than 5 characters and less than 15";
                textBoxLogin.Background = Brushes.DarkRed;
            }

            if ((password.Length > 7) && (password.Length < 20))
            {
                passwordCorrect = true;
                textBoxPassword.Background = Brushes.White;
            }
            else
            {
                textBoxPassword.ToolTip = "password should be more than 7 characters and less than 20";
                textBoxPassword.Background = Brushes.DarkRed;
            }

            if (passwordRepeat == password)
            {
                passwordRepeatCorrect = true;
                textBoxPasswordRepeat.Background = Brushes.White;
            }
            else
            {
                textBoxPasswordRepeat.ToolTip = "passwords do not match";
                textBoxPasswordRepeat.Background = Brushes.DarkRed;
            }

            if (CheckingEmail(email))
            {
                emailCorrect = true;
                textBoxEmail.Background = Brushes.White;
            }
            else
            {
                textBoxEmail.ToolTip = "Email is incorrect";
                textBoxEmail.Background = Brushes.DarkRed;
            }

            if (loginCorrect && passwordCorrect && passwordRepeatCorrect && emailCorrect)
            {
                clearData();
                Print("Everithing is alright");

                createNewUser();

                changeActiveWindow();
            }

            bool CheckingEmail(string emailAddress)
            {
                try
                {
                    MailAddress m = new MailAddress(emailAddress);

                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }

            void changeActiveWindow()
            {
                userPage userPageShow = new userPage();
                userPageShow.Show();
                Hide();
            }

            void clearData()
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                textBoxPassword.ToolTip = "";
                textBoxPassword.Background = Brushes.Transparent;
                textBoxPasswordRepeat.ToolTip = "";
                textBoxPasswordRepeat.Background = Brushes.Transparent;
                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;
            }

            void createNewUser() 
            {
                User user = new User(login, password, email);
                db.Users.Add(user);
                db.SaveChanges();
            }

            void Print(string text)
            {
                MessageBox.Show(text);
            }
        }

        private void Button_sign_in_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Hide();
        }
    }
}
