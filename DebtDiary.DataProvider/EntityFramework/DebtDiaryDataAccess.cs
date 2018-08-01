using DebtDiary.Core;
using System;

namespace DebtDiary.DataProvider
{
    /// <summary>
    /// Application data access class which use Entity Framework database
    /// </summary>
    public class DebtDiaryDataAccess : IDebtDiaryDataAccess, IDisposable
    {
        /// <summary>
        /// Entity Framework database context
        /// </summary>
        private DebtDiaryDbContext dbContext = new DebtDiaryDbContext();

        /// <summary>
        /// Register user method
        /// </summary>
        /// <param name="user">User you want to register in the database</param>
        public void RegisterUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// IDisposable interface implementation method
        /// </summary>
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
