using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Security;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DeleteDebtorSubpage.xaml
    /// </summary>
    public partial class DeleteDebtorSubpage : Page, IHavePassword
    {
        SecureString IHavePassword.Password => Password.SecurePassword;

        public DeleteDebtorSubpage()
        {
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            IDiaryPageViewModel diaryPageViewModel = IocContainer.Get<IDiaryPageViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IClientDataStore clientDataStore = IocContainer.Get<IClientDataStore>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();
            DataContext = new DeleteDebtorSubpageViewModel(applicationViewModel, diaryPageViewModel, dialogFacade, clientDataStore, dataAccess);

            InitializeComponent();
        }

        public void ClearPassword() => Password.Clear();
    }
}
