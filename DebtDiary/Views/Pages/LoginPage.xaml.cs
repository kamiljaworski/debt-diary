using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IHavePassword
    {
        /// <summary>
        /// Password of IHavePassword interface implementation
        /// </summary>
        public SecureString Password => UserPassword.SecurePassword;

        public LoginPage()
        {
            DataContext = new LoginPageViewModel();
            InitializeComponent();
        }
    }
}
