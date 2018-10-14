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

        public bool IsSummaryActive { get; set; }
        public bool IsMyAccountActive { get; set; }
        public bool IsLogoutActive { get; set; }


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
                ResetActiveButtons();
                IsSummaryActive = true;
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.SummarySubpage);
            });

            MyAccountCommand = new RelayCommand(() =>
            {
                ResetActiveButtons();
                IsMyAccountActive = true;
            });

            LogoutCommand = new RelayCommand(() =>
            {
                ResetActiveButtons();
                IsLogoutActive = true;
                IocContainer.Get<IClientDataStore>().LogoutUser();
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentPage(ApplicationPage.LoginPage);
            });

            AddDebtorCommand = new RelayCommand(() =>
            {
                ResetActiveButtons();
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.AddDebtorSubpage);
            });
        }

        public void ResetActiveButtons()
        {
            IsSummaryActive = false;
            IsMyAccountActive = false;
            IsLogoutActive = false;
        }

    }
}
