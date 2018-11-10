using DebtDiary.Core;
using DebtDiary.DataProvider;
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
            IDiaryPageViewModel diaryPageViewModel = IocContainer.Get<IDiaryPageViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IClientDataStore clientDataStore = IocContainer.Get<IClientDataStore>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();
            DataContext = new MyAccountSubpageViewModel(diaryPageViewModel, dialogFacade, clientDataStore, dataAccess);

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
