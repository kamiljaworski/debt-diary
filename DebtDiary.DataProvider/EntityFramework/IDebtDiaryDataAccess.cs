using DebtDiary.Core;
using System.Threading.Tasks;

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
        Task CreateAccountAsync(User user);

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

        /// <summary>
        /// Get user from the database
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="hashedPassword">Hashed password</param>
        /// <returns><see cref="User"/> if user exist or <see cref="null"/> if not</returns>
        User GetUser(string username, string hashedPassword);
    }
}
