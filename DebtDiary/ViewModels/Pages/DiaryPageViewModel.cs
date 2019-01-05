using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public class DiaryPageViewModel : BaseViewModel, IDiaryPageViewModel, ILoadable
    {
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IClientDataStore _clientDataStore;

        private bool _isInDesignTime;
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
        public IDebtorsListViewModel DebtorsList { get; private set; }
        public bool IsLoaded { get; private set; }

        #endregion

        #region Constructor

        public DiaryPageViewModel()
        {
            _isInDesignTime = true;
        }

        public DiaryPageViewModel(IApplicationViewModel applicationViewModel, IClientDataStore clientDataStore)
        {
            _isInDesignTime = false;

            IsLoaded = false;
            _applicationViewModel = applicationViewModel;
            _clientDataStore = clientDataStore;
            DebtorsList = new DebtorsListViewModel(_applicationViewModel, _clientDataStore);

            UpdateUsersData();

            SummaryCommand = new RelayCommand(() => ResetSelectedDebtorAndChangeSubpageAsync(ApplicationSubpage.SummarySubpage));
            MyAccountCommand = new RelayCommand(() => ResetSelectedDebtorAndChangeSubpageAsync(ApplicationSubpage.MyAccountSubpage));
            LogoutCommand = new RelayCommand(async () =>
            {
                ResetSelectedDebtor();
                _clientDataStore.LogoutUser();
                await _applicationViewModel.ChangeCurrentPageAsync(ApplicationPage.LoginPage);
            });
            AddDebtorCommand = new RelayCommand(() => ResetSelectedDebtorAndChangeSubpageAsync(ApplicationSubpage.AddDebtorSubpage));
            SortCommand = new RelayCommand(() =>
            {
                SortType = SortType == SortType.Ascending ? SortType.Descending : SortType.Ascending;
                DebtorsList.Sort(SortType);
            });

            IsLoaded = true;
        }
        #endregion

        #region Public methods


        public void UpdateUsersData()
        {
            if (_isInDesignTime == true)
                return;

            User loggedUser = _clientDataStore.LoggedUser;
            if (loggedUser == null)
                return;

            FullName = loggedUser.FullName;
            Username = loggedUser.Username;
            Initials = loggedUser.Initials;
            AvatarColor = loggedUser.AvatarColor;
        }

        public void UpdateDebtorsList() => DebtorsList.Update();
        #endregion

        #region Private methods

        private void ResetSelectedDebtor() => _applicationViewModel.SelectedDebtor = null;

        private async void ResetSelectedDebtorAndChangeSubpageAsync(ApplicationSubpage subpage)
        {
            ResetSelectedDebtor();
            await _applicationViewModel.ChangeCurrentSubpageAsync(subpage);
        }
        #endregion
    }
}
