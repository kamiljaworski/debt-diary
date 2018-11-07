using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for EditDebtorSubpage.xaml
    /// </summary>
    public partial class EditDebtorSubpage : Page
    {
        public EditDebtorSubpage()
        {
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            IDiaryPageViewModel diaryPageViewModel = IocContainer.Get<IDiaryPageViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IClientDataStore clientDataStore = IocContainer.Get<IClientDataStore>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();
            DataContext = new EditDebtorSubpageViewModel(applicationViewModel, diaryPageViewModel, dialogFacade, clientDataStore, dataAccess);

            InitializeComponent();
        }
    }
}
