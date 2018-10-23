using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DeleteDebtorSubpage.xaml
    /// </summary>
    public partial class DeleteDebtorSubpage : Page, IHavePassword
    {
        SecureString IHavePassword.Password => Password.SecurePassword;

        public DeleteDebtorSubpage()
        {
            DataContext = new DeleteDebtorSubpageViewModel();
            InitializeComponent();
        }

        public void ClearPassword() => Password.Clear();
    }
}
