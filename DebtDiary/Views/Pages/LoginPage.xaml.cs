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

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginPage()
        {
            DataContext = new LoginPageViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Clear the PasswordBox
        /// </summary>
        public void ClearPassword() => UserPassword.Clear();
    }
}
