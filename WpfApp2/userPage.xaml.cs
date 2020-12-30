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
            double percent, investments;
            int duration;
            string tPercent = Percent.Text;
            string tSalary = Salary.Text;
            string tDuration = Duration.Text;

            Double.TryParse(tPercent, out percent);
            percent = percent / 1200 + 1;
            Double.TryParse(tSalary, out investments);
            Int32.TryParse(tDuration, out duration);

            if (expenses > 0)
            {
                investments = investments - (investments + expenses);
            }

            calculateProfit(balance, investments, percent, duration);


        }

        private void calculateProfit(double balance, double investments, double percent, int duration)
        {
            ChartValues<int> znac = new ChartValues<int> { };
            int result = (int)balance;
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
            balance = (int)balance;
            result = (int)balance - result;
            Changes.Text = result.ToString();
            Result.Text = balance.ToString();

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


                YFormatter = value => "Month №" + (++value);
            }
            else
            {
                SeriesCollection[0].Values = znac;
            }
            DataContext = this;
        }

        public userPage()
        {
            InitializeComponent();
        }

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


    }
}
