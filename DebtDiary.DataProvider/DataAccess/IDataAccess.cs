using DebtDiary.Core;

namespace DebtDiary.DataProvider
{
    public interface IDataAccess
    {
        bool TryCreateUser(User user);
        bool IsUsernameTaken(string username);
        bool IsEmailTaken(string email);
        User GetUser(string username, string hashedPassword);
        void SaveChanges();
    }
}
