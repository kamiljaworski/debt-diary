using DebtDiary.Core;

namespace DebtDiary.DataProvider
{
    /// <summary>
    /// Interface for application data access
    /// </summary>
    public interface IDebtDiaryDataAccess
    {
        /// <summary>
        /// Method that create new user in the database
        /// </summary>
        /// <param name="user">New user to sign up</param>
        void CreateAccount(User user);
    }
}
