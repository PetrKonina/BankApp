using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

        #region event handlers
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            bool loginCorrect = false, passwordCorrect = false, passwordRepeatCorrect = false, emailCorrect = false;
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Password.Trim();
            string passwordRepeat = textBoxPasswordRepeat.Password.Trim();
            string email = textBoxEmail.Text.ToLower().Trim();

            dataValidation(ref loginCorrect, ref passwordCorrect, ref passwordRepeatCorrect, ref emailCorrect, login, password, email, passwordRepeat);

            if (IsRegistrationDataCorrect(loginCorrect, passwordCorrect, passwordRepeatCorrect, emailCorrect))
            {
                clearData();
                Print("Everithing is alright, you have been registrated");
                createNewUser(login, password, email);
                changeActiveWindow();
            }
        }

        void dataValidation(ref bool loginCorrect , ref bool passwordCorrect, ref bool passwordRepeatCorrect,ref bool emailCorrect, string login, string password, string email, string passwordRepeat)
        {
            if (loginValidation(login))
            {
                loginCorrect = true;
                textBoxLogin.Background = Brushes.White;
            }
            else
            {
                textBoxLogin.ToolTip = "login should be at least 5 and no more than 16 symbols and starts with a symbol";
                textBoxLogin.Background = Brushes.DarkRed;
            }

            if (passwordValidation(password))
            {
                passwordCorrect = true;
                textBoxPassword.Background = Brushes.White;
            }
            else
            {
                textBoxPassword.ToolTip = "password should be more than 7 characters and less than 16, contain at least 1 uppercase and 1 lowercase symbol";
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
        }

        bool IsRegistrationDataCorrect(bool loginCorrect, bool passwordCorrect, bool passwordRepeatCorrect, bool emailCorrect)
        {
            return (loginCorrect && passwordCorrect && passwordRepeatCorrect && emailCorrect);
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

        bool loginValidation(string login)
        {
            Regex startsWithCharacter = new Regex(@"^[a-zA-Z]");
            Regex hasBeetween8And15Chars = new Regex(@".{5,15}");

            return startsWithCharacter.IsMatch(login) && hasBeetween8And15Chars.IsMatch(login);
        }

        bool passwordValidation(string password)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasBeetween8And15Chars = new Regex(@".{8,15}");

            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasBeetween8And15Chars.IsMatch(password);
        }

        void changeActiveWindow()
        {
            userPage userPageShow = new userPage();
            userPageShow.Show();
            Hide();
        }

        void clearData()
        {
            textBoxLogin.ToolTip = null;
            textBoxLogin.Background = Brushes.Transparent;
            textBoxPassword.ToolTip = null;
            textBoxPassword.Background = Brushes.Transparent;
            textBoxPasswordRepeat.ToolTip = null;
            textBoxPasswordRepeat.Background = Brushes.Transparent;
            textBoxEmail.ToolTip = null;
            textBoxEmail.Background = Brushes.Transparent;
        }

        void createNewUser(string login, string password, string email)
        {
            User user = new User(login, password, email);
            db.Users.Add(user);
            db.SaveChanges();
        }

        void Print(string text)
        {
            MessageBox.Show(text);
        }
        private void Button_sign_in_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Hide();
        }
        #endregion
    }
}
