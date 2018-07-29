using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DiaryPage.xaml
    /// </summary>
    public partial class DiaryPage : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public DiaryPage()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Test",
                    Values = new ChartValues<double> { -1000, -700, 0, -200 , 0, 200, 500, 100, 0, 290 }
                }
            };

            YFormatter = value => value.ToString("C");


            DataContext = this;
        }
    }
}
