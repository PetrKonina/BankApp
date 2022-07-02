using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для adminPage.xaml
    /// </summary>
    public partial class adminPage : Window
    {
        // Admin page, show all registrated users
        public adminPage()
        {
            InitializeComponent();

            ApplicationContext db = new ApplicationContext();
            List<User> users = db.Users.ToList();

            listOfUsers.ItemsSource = users;

        }
    }
}
