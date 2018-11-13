using DebtDiary.Core;
using DebtDiary.DataProvider;
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
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            IDiaryPageViewModel diaryPageViewModel = IocContainer.Get<IDiaryPageViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IClientDataStore clientDataStore = IocContainer.Get<IClientDataStore>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();
            DataContext = new LoginPageViewModel(applicationViewModel, diaryPageViewModel, dialogFacade, clientDataStore, dataAccess);

            InitializeComponent();
        }

        /// <summary>
        /// Clear the PasswordBox
        /// </summary>
        public void ClearPassword() => UserPassword.Clear();
    }
}
