using DebtDiary.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebtDiary.DataProvider
{
    public interface IDataAccess
    {
        /// <summary>
        /// Create new user
        /// </summary>
        void CreateAccount(User user);

        /// <summary>
        /// Check if username is already taken
        /// </summary>
        bool IsUsernameTaken(string username);

        /// <summary>
        /// Check if email is already taken
        /// </summary>
        bool IsEmailTaken(string email);

        /// <summary>
        /// Get the user from database
        /// </summary>
        User GetUser(string username, string hashedPassword);

        /// <summary>
        /// Save database changes done in the application runtime
        /// </summary>
        void SaveChanges();
    }
}
