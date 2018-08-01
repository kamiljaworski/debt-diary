using DebtDiary.Core;

namespace DebtDiary.DataProvider
{
    /// <summary>
    /// Tempoprary test class for managing database
    /// </summary>
    public class TestDatabase
    {

        /// <summary>
        /// Temporary register user method
        /// </summary>
        /// <param name="user"></param>
        public void RegisterUser(User user)
        {
            using (var dbContext = new DebtDiaryDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }
    }
}
