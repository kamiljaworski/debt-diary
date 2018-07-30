using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IHavePassword
    {
        public LoginPage()
        {
            DataContext = new LoginPageViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// IHavePassword interface implementation
        /// </summary>
        public SecureString Password => UserPassword.SecurePassword;
    }
}
