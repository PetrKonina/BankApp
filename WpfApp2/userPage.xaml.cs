using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2
{
    public partial class userPage : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] Labels { get; set; }

        bool chartCreated = false;

        private void saveVariables(double balance = 0, int expenses = 0)

        {
            double percent = 0, investments = -1;
            int duration = 0;
            string tPercent = Percent.Text.Trim();
            string tSalary = Salary.Text.Trim();
            string tDuration = Duration.Text.Trim();

            Double.TryParse(tPercent, out percent);
            if(percent == 0)
            {
                MessageBox.Show("Enter percents correctly(it should only contain numbers without spaces and it should not be zero)");
            }
            percent = percent / 1200 + 1;

            Double.TryParse(tSalary, out investments);
            if (investments == 0)
            {
                MessageBox.Show("Enter investments correctly(it should only contain numbers without spaces");
                Salary.ToolTip = "Enter investments correctly(it should only contain numbers without spaces";
            }

            Int32.TryParse(tDuration, out duration);
            if (duration == 0)
            {
                MessageBox.Show("Enter duration correctly(it should only contain numbers without spaces and it should not be zero)");
                Duration.ToolTip = "Enter duration correctly(it should only contain numbers without spaces and it should not be zero)";
            }

            if (expenses > 0)
            {
                investments -= expenses;
            }

            calculateProfit(balance, investments, percent, duration);


        }

        // shows profit and chart
        private void calculateProfit(double balance, double investments, double percent, int duration)
        {
            ChartValues<int> znac = new ChartValues<int> { };
            double result = (int)balance;
            string abels = null;
            int ekstremumPoints = duration;
            if (ekstremumPoints > 10)
            {
                ekstremumPoints /= 10;
            }
            for (int i = 0; i < duration; i++)
            {
                balance = balance * percent + investments;
                if ((duration > 10) && (i % ekstremumPoints == 0))
                {
                    znac.Add((int)balance);
                    abels += i;
                }
            }
            result = balance - result;
            Changes.Text = (Math.Round(result, 1)).ToString();
            Result.Text = balance.ToString();

            // Chart creation
            if (chartCreated != true)
            {
                chartCreated = true;
                SeriesCollection = new SeriesCollection
                {

                    new LineSeries
                    {
                        Title = "Vklad",
                        Values = znac,
                        LineSmoothness = 0,
                        PointForeground = Brushes.Gray,
                        DataLabels = true,
                        LabelPoint = point => point.Y +""
                    },

                };
                YFormatter = value => "Period №" + (++value);
            }
            else
            {
                SeriesCollection[0].Values = znac;
            }
            DataContext = this;
        }
        #region event handlers
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double sum;
            string tsum = Sum.Text;
            Double.TryParse(tsum, out sum);

            saveVariables(sum);


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double balance;
            string tbalance = Result.Text;
            Double.TryParse(tbalance, out balance);

            int expenses;
            string tExpenses = Expenses.Text;
            Int32.TryParse(tExpenses, out expenses);

            saveVariables(balance, expenses);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double balance;
            string tbalance = Result.Text;
            Double.TryParse(tbalance, out balance);

            saveVariables(balance);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow autorizationWindow = new AuthorizationWindow();
            autorizationWindow.Show();
            Hide();
        }
        #endregion
        public userPage()
        {
            InitializeComponent();
        }
    }
}
