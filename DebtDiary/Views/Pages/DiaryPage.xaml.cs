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
        public DiaryPage()
        {
            DataContext = IocContainer.Get<IDiaryPageViewModel>();
            InitializeComponent();
        }
    }
}
