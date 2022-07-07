namespace WpfApp2
{
    class User
    {
        public int Id { get; set; }

        private string Login, Password, Email;

        public string login 
        {
            get { return Login; }
            private set { Login = value; }
        }

        public string password
        {
            get { return Password; }
            private set { Password = value; }
        }

        public string email
        {
            get { return Email; }
            private set { Email = value; }
        }

        public User(string Login, string Password, string Email)
            :this()
        {
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
        }

        public User(){}
    }
}
