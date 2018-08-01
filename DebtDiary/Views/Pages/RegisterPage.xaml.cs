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
        public SecureString Password => UserPassword.SecurePassword;

        /// <summary>
        /// Second Password of IHaveTwoPasswords interface implementation
        /// </summary>
        public SecureString SecondPassword => RepeatUserPassword.SecurePassword;

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterPage()
        {
            DataContext = new RegisterPageViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Clears both PasswordBoxes in the view
        /// </summary>
        public void ClearPassword()
        {
            UserPassword.Clear();
            RepeatUserPassword.Clear();
        }
    }
}
