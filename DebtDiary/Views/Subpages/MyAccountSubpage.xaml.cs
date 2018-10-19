using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for MyAccountSubpage.xaml
    /// </summary>
    public partial class MyAccountSubpage : Page
    {
        public MyAccountSubpage()
        {
            DataContext = new AddDebtorSubpageViewModel();
            InitializeComponent();
        }
    }
}
