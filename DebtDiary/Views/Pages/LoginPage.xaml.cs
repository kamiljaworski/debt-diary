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
        public SecureString Password => UserPassword.SecurePassword;

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

        public void ClearPassword() => UserPassword.Clear();
    }
}
