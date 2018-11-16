using DebtDiary.Core;

namespace DebtDiary.DataProvider
{
    public interface IDataAccess
    {
        bool TryCreateUser(User user);
        bool IsUsernameTaken(string username);
        bool IsEmailTaken(string email);
        bool UserExist(string username, string hashedPassword);
        bool UserAddedThisDebtor(int usersId, string debtorsFirstName, string debtorsLastName);
        bool TryGetUser(string username, string hashedPassword, out User user);
        bool TrySaveChanges();
    }
}
