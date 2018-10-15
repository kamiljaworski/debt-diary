using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public class DiaryPageViewModel : BaseViewModel, IDiaryPageViewModel
    {
        #region Public properties

        public string FullName { get; set; }
        public string Username { get; set; }
        public string Initials { get; set; }

        public ICommand SummaryCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand AddDebtorCommand { get; set; }
        public ICommand SortCommand { get; set; }

        public bool IsSummarySelected { get; set; }
        public bool IsMyAccountSelected { get; set; }
        public bool IsLogoutSelected { get; set; }
        public SortType SortType { get; set; } = SortType.Descending;
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiaryPageViewModel(bool designTime = false)
        {
            if (designTime == false)
            {
                User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

                FullName = loggedUser.FullName;
                Username = loggedUser.Username;
                Initials = loggedUser.Initials;
            }

            SummaryCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetSelectedButtons();
                IsSummarySelected = true;
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.SummarySubpage);
            });

            MyAccountCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetSelectedButtons();
                IsMyAccountSelected = true;
            });

            LogoutCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetSelectedButtons();
                IsLogoutSelected = true;
                IocContainer.Get<IClientDataStore>().LogoutUser();
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentPage(ApplicationPage.LoginPage);
            });

            AddDebtorCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetSelectedButtons();
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.AddDebtorSubpage);
            });

            SortCommand = new RelayCommand(() =>
            {
                SortType = SortType == SortType.Ascending ? SortType.Descending : SortType.Ascending;
                IocContainer.Get<IDebtorsListViewModel>().Sort(SortType);
            });
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Reset all the selected buttons properties to false
        /// </summary>
        public void ResetSelectedButtons()
        {
            IsSummarySelected = false;
            IsMyAccountSelected = false;
            IsLogoutSelected = false;
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Reset selected debtor in the application and in side menu view
        /// </summary>
        private void ResetSelectedDebtor()
        {
            IocContainer.Get<IApplicationViewModel>().SelectedDebtor = null;
            IocContainer.Get<IDebtorsListViewModel>().ResetSelectedDebtor();
        }
        #endregion
    }
}
