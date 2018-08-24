using DebtDiary.Core;

namespace DebtDiary
{
    public class SideMenuViewModel : BaseViewModel
    {
        private User _loggedUser = null;

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Initials { get; set; }

        public SideMenuViewModel()
        {
            _loggedUser = IocContainer.Get<IClientDataStore>().CurrentUser;

            FullName = _loggedUser.FullName;
            Username = _loggedUser.Username;
            Initials = _loggedUser.Initials;
        }

    }
}
