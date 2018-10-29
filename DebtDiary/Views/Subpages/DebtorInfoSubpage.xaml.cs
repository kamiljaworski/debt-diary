using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DebtorInfoSubpage.xaml
    /// </summary>
    public partial class DebtorInfoSubpage : Page
    {
        public DebtorInfoSubpage()
        {
            DataContext = IocContainer.Get<IDebtorInfoSubpageViewModel>();
            InitializeComponent();
        }
    }
}
