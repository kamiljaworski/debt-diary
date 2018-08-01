using DebtDiary.Core;

namespace DebtDiary.DataProvider
{
    /// <summary>
    /// Interface for data access
    /// </summary>
    public interface IDebtDiaryDataAccess
    {
        /// <summary>
        /// Register user method
        /// </summary>
        /// <param name="user">User to register in the database</param>
        void RegisterUser(User user);
    }
}
