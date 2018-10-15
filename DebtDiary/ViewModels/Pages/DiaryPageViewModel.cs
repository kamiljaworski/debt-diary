using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public class DiaryPageViewModel : BaseViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Initials { get; set; }

        public ICommand SummaryCommand { get; set; }
        public ICommand MyAccountCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand AddDebtorCommand { get; set; }
        public ICommand SortCommand { get; set; }

        public bool IsSummaryActive { get; set; }
        public bool IsMyAccountActive { get; set; }
        public bool IsLogoutActive { get; set; }
        public SortType SortType { get; set; } = SortType.Descending;


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
                ResetActiveButtons();
                IsSummaryActive = true;
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.SummarySubpage);
            });

            MyAccountCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetActiveButtons();
                IsMyAccountActive = true;
            });

            LogoutCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetActiveButtons();
                IsLogoutActive = true;
                IocContainer.Get<IClientDataStore>().LogoutUser();
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentPage(ApplicationPage.LoginPage);
            });

            AddDebtorCommand = new RelayCommand(() =>
            {
                ResetSelectedDebtor();
                ResetActiveButtons();
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.AddDebtorSubpage);
            });

            SortCommand = new RelayCommand(() =>
            {
                SortType = SortType == SortType.Ascending ? SortType.Descending : SortType.Ascending;
                IocContainer.Get<DebtorsListViewModel>().Sort(SortType);
            });
        }

        public void ResetActiveButtons()
        {
            IsSummaryActive = false;
            IsMyAccountActive = false;
            IsLogoutActive = false;
        }

        private void ResetSelectedDebtor()
        {
            IocContainer.Get<IApplicationViewModel>().SelectedDebtor = null;
            IocContainer.Get<DebtorsListViewModel>().ResetSelectedDebtor();
        }

    }
}
