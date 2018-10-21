using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for MyAccountSubpage.xaml
    /// </summary>
    public partial class MyAccountSubpage : Page, IHaveThreePasswords
    {
        public SecureString Password => CurrentPassword.SecurePassword;
        public SecureString SecondPassword => NewPassword.SecurePassword;
        public SecureString ThirdPassword => RepeatNewPassword.SecurePassword;

        public MyAccountSubpage()
        {
            DataContext = new MyAccountSubpageViewModel();
            InitializeComponent();
        }

        public void ClearPassword()
        {
            CurrentPassword.Clear();
            NewPassword.Clear();
            RepeatNewPassword.Clear();
        }
    }
}
