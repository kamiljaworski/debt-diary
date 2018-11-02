using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public class DiaryPageViewModel : BaseViewModel, IDiaryPageViewModel, ILoadable
    {
        #region Public properties

        public string FullName { get; set; }
        public string Username { get; set; }
        public string Initials { get; set; }
        public Color AvatarColor { get; set; }

        public ICommand SummaryCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand AddDebtorCommand { get; set; }
        public ICommand SortCommand { get; set; }

        public bool IsSummarySelected { get; set; }
        public bool IsMyAccountSelected { get; set; }
        public bool IsLogoutSelected { get; set; }
        public bool IsAddDebtorSelected { get; set; }
        public SortType SortType { get; set; } = SortType.Descending;

        public IDebtorsListViewModel DebtorsList => IocContainer.Get<IDebtorsListViewModel>();

        public bool IsLoaded { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DiaryPageViewModel(bool designTime = false)
        {
            IsLoaded = false;

            if (designTime == false)
                UpdateUsersData();

            SummaryCommand = new RelayCommand(() =>
            {
                ChangeSubpageAsync(ApplicationSubpage.SummarySubpage);
                IsSummarySelected = true;
            });

            MyAccountCommand = new RelayCommand(() =>
            {
                ChangeSubpageAsync(ApplicationSubpage.MyAccountSubpage);
                IsMyAccountSelected = true;
            });

            LogoutCommand = new RelayCommand(async () =>
            {
                ResetSelectedDebtor();
                ResetSelectedButtons();
                IsLogoutSelected = true;
                IocContainer.Get<IClientDataStore>().LogoutUser();
                await IocContainer.Get<IApplicationViewModel>().ChangeCurrentPageAsync(ApplicationPage.LoginPage);
            });

            AddDebtorCommand = new RelayCommand(() =>
            {
                ChangeSubpageAsync(ApplicationSubpage.AddDebtorSubpage);
                IsAddDebtorSelected = true;
            });

            SortCommand = new RelayCommand(() =>
            {
                SortType = SortType == SortType.Ascending ? SortType.Descending : SortType.Ascending;
                IocContainer.Get<IDebtorsListViewModel>().Sort(SortType);
            });

            IsLoaded = true;
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
            IsAddDebtorSelected = false;
        }

        /// <summary>
        /// Reset users fullname, username and initials
        /// </summary>
        public void UpdateUsersData()
        {
            User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

            FullName = loggedUser.FullName;
            Username = loggedUser.Username;
            Initials = loggedUser.Initials;
            AvatarColor = loggedUser.AvatarColor;
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

        /// <summary>
        /// Change subpage
        /// </summary>
        private async void ChangeSubpageAsync(ApplicationSubpage subpage)
        {
            ResetSelectedDebtor();
            ResetSelectedButtons();
            await IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpageAsync(subpage);
        }
        #endregion
    }
}
