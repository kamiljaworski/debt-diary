using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for AddDebtorSubpage.xaml
    /// </summary>
    public partial class AddDebtorSubpage : Page
    {
        public AddDebtorSubpage()
        {
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            IDiaryPageViewModel diaryPageViewModel = IocContainer.Get<IDiaryPageViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IClientDataStore clientDataStore = IocContainer.Get<IClientDataStore>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();
            DataContext = new AddDebtorSubpageViewModel(applicationViewModel, diaryPageViewModel, dialogFacade, clientDataStore, dataAccess);

            InitializeComponent();
        }
    }
}
