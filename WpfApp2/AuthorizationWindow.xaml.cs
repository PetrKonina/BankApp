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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Password.Trim();


            User authUser = null;
            using (ApplicationContext db = new ApplicationContext()) 
            {
                authUser = db.Users.Where(b => b.login == login && b.password == password).FirstOrDefault();
            }

            if(authUser != null)
            {
                MessageBox.Show("correct");
            }
            else
            {
                MessageBox.Show("incorrect");
            }


            }
    }
}
