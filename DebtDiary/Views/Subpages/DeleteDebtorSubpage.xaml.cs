using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DeleteDebtorSubpage.xaml
    /// </summary>
    public partial class DeleteDebtorSubpage : Page
    {
        public DeleteDebtorSubpage()
        {
            DataContext = new AddDebtorSubpageViewModel();
            InitializeComponent();
        }
    }
}
