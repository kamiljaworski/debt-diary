namespace DebtDiary.Core
{
    public class ClientDataStore : IClientDataStore
    {
        public bool IsUserLoggedIn { get; set; }

        public User CurrentUser { get; set; } = null;
    }
}
