﻿using System.Linq;
using System.Windows;

namespace WpfApp2
{
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

            if(authUser != null && authUser.login == "admin1")
            {
                adminPage adminPageWindow = new adminPage();
                adminPageWindow.Show();
                Hide();
            }
            else if (authUser !=null)
            {
                MessageBox.Show("correct");

                userPage userPageWindow = new userPage();
                userPageWindow.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("incorrect");
            }


            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
