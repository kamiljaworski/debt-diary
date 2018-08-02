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

        /// <summary>
        /// Method that check if account with this username can be added to the database
        /// </summary>
        /// <param name="username">New user's username</param>
        /// <returns><see cref="bool"/> false if this username exist in the database and true if not</returns>
        bool IsUsernameAvailable(string username);

        /// <summary>
        /// Method that check if account with this e-mail can be added to the database
        /// </summary>
        /// <param name="email">New user's e-mail</param>
        /// <returns><see cref="bool"/> false if this e-mail exist in the database and true if not</returns>
        bool IsEmailAvailable(string email);
    }
}
