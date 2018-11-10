using DebtDiary.DataProvider;
using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, IHaveTwoPasswords
    {
        public SecureString Password => UserPassword.SecurePassword;
        public SecureString SecondPassword => RepeatUserPassword.SecurePassword;

        public RegisterPage()
        {
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();

            DataContext = new RegisterPageViewModel(applicationViewModel, dialogFacade, dataAccess);
            InitializeComponent();
        }

        public void ClearPassword()
        {
            UserPassword.Clear();
            RepeatUserPassword.Clear();
        }
    }
}
