using DebtDiary.Core;
using DebtDiary.DataProvider;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DebtorInfoSubpage.xaml
    /// </summary>
    public partial class DebtorInfoSubpage : Page
    {
        public DebtorInfoSubpage()
        {
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            IDiaryPageViewModel diaryPageViewModel = IocContainer.Get<IDiaryPageViewModel>();
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            IClientDataStore clientDataStore = IocContainer.Get<IClientDataStore>();
            IDataAccess dataAccess = IocContainer.Get<IDataAccess>();
            DataContext = new DebtorInfoSubpageViewModel(applicationViewModel, diaryPageViewModel, dialogFacade, clientDataStore, dataAccess);

            InitializeComponent();
        }
    }
}
