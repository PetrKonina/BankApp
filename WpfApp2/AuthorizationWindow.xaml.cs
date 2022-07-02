using System.Linq;
using System.Windows;

namespace WpfApp2
{
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        //Autorization button
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Password.Trim();


            User authUser = null;
            using (ApplicationContext db = new ApplicationContext()) 
            {
                authUser = db.Users.Where(b => b.login == login && b.password == password).FirstOrDefault();
            }
            // check if loggin in as a admin
            if(authUser != null && authUser.login == "admin1")
            {
                adminPage adminPageWindow = new adminPage();
                adminPageWindow.Show();
                Hide();
            }
            else if (authUser !=null)
            {
                Print("correct");

                userPage userPageWindow = new userPage();
                userPageWindow.Show();
                Hide();
            }
            else
            {
                Print("incorrect");
            }

            void Print(string text)
            {
                MessageBox.Show(text);
            }
        }

        // Show registration page button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
