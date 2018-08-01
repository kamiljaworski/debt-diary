using System.Data.Entity;

namespace DebtDiary.Core
{
    /// <summary>
    /// Application database context
    /// </summary>
    public class DebtDiaryDbContext : DbContext
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DebtDiaryDbContext(): base("name = DebtDiaryConnectionString")
        {
        }

        /// <summary>
        /// Users table
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
