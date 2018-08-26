namespace DebtDiary.Core
{
    public class ClientDataStore : IClientDataStore
    {
        public bool IsUserLoggedIn { get; private set; } = false;

        public User LoggedUser { get; private set; } = null;

        public void LoginUser(User user)
        {
            LoggedUser = user;
            IsUserLoggedIn = true;
        }

        public void LogoutUser()
        {
            LoggedUser = null;
            IsUserLoggedIn = false;
        }
    }
}
