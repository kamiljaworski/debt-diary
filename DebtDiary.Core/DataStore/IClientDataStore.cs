namespace DebtDiary.Core
{
    public interface IClientDataStore
    {
        bool IsUserLoggedIn { get; set; }

        User CurrentUser { get; set; }
    }
}
