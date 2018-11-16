namespace DebtDiary.Core
{
    public class ClientDataStore : IClientDataStore
    {
        public User LoggedUser { get; private set; } = null;

        public void LoginUser(User user)
        {
            LoggedUser = user;
        }

        public void LogoutUser()
        {
            LoggedUser = null;
        }
    }
}
