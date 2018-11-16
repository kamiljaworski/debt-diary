namespace DebtDiary.Core
{
    public interface IClientDataStore
    {
        User LoggedUser { get; }
        void LoginUser(User user);
        void LogoutUser();
    }
}
