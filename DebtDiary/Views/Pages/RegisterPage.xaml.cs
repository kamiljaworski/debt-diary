using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, IHaveTwoPasswords
    {
        /// <summary>
        /// First Password of IHaveTwoPasswords interface implementation
        /// </summary>
        public SecureString FirstPassword => UserPassword.SecurePassword;

        /// <summary>
        /// First Password of IHaveTwoPasswords interface implementation
        /// </summary>
        public SecureString SecondPassword => RepeatUserPassword.SecurePassword;

        public RegisterPage()
        {
            DataContext = new RegisterPageViewModel();
            InitializeComponent();
        }

    }
}
