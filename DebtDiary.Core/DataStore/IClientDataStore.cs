namespace DebtDiary.Core
{
    public interface IClientDataStore
    {
        bool IsUserLoggedIn { get; }

        User CurrentUser { get; }

        void LoginUser(User user);

        void LogoutUser();
    }
}
