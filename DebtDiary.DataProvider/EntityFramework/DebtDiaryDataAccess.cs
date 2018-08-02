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
        /// Method that create new user in the database
        /// </summary>
        /// <param name="user">New user to sign up</param>
        public void CreateAccount(User user)
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
