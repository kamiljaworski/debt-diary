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

            SummaryCommand = new RelayCommand(() => ChangeSubpageAsync(ApplicationSubpage.SummarySubpage));

            MyAccountCommand = new RelayCommand(() => ChangeSubpageAsync(ApplicationSubpage.MyAccountSubpage));

            LogoutCommand = new RelayCommand(async () =>
            {
                ResetSelectedDebtor();
                IocContainer.Get<IClientDataStore>().LogoutUser();
                await IocContainer.Get<IApplicationViewModel>().ChangeCurrentPageAsync(ApplicationPage.LoginPage);
            });

            AddDebtorCommand = new RelayCommand(() => ChangeSubpageAsync(ApplicationSubpage.AddDebtorSubpage));

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
        }

        /// <summary>
        /// Change subpage
        /// </summary>
        private async void ChangeSubpageAsync(ApplicationSubpage subpage)
        {
            ResetSelectedDebtor();
            await IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpageAsync(subpage);
        }
        #endregion
    }
}
