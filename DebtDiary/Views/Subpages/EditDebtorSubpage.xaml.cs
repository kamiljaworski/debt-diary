using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for EditDebtorSubpage.xaml
    /// </summary>
    public partial class EditDebtorSubpage : Page
    {
        public EditDebtorSubpage()
        {
            DataContext = new EditDebtorSubpageViewModel();
            InitializeComponent();
        }
    }
}
