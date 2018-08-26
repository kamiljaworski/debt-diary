using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public class DiaryPageViewModel : BaseViewModel
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string Initials { get; set; }

        public ICommand AddDebtorCommand { get; set; }

        public DiaryPageViewModel(bool designTime = false)
        {
            if (designTime == false)
            {
                User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

                FullName = loggedUser.FullName;
                Username = loggedUser.Username;
                Initials = loggedUser.Initials;
            }

            AddDebtorCommand = new RelayCommand(() => IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.AddDebtorSubpage));
        }

    }
}
