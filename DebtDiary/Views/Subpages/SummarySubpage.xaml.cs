using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for SummarySubpage.xaml
    /// </summary>
    public partial class SummarySubpage : Page
    {
        public SummarySubpage()
        {
            DataContext = new SummarySubpageViewModel();
            InitializeComponent();
        }
    }
}
