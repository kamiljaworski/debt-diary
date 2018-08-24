namespace DebtDiary.Core
{
    public class ClientDataStore : IClientDataStore
    {
        public bool IsUserLoggedIn { get; private set; } = false;

        public User CurrentUser { get; private set; } = null;

        public void LoginUser(User user)
        {
            CurrentUser = user;
            IsUserLoggedIn = true;
        }

        public void LogoutUser()
        {
            CurrentUser = null;
            IsUserLoggedIn = false;
        }
    }
}
