using DebtDiary.Core;
using System.Data.Entity;

namespace DebtDiary.DataProvider
{
    /// <summary>
    /// Application database context
    /// </summary>
    public class DebtDiaryDbContext : DbContext
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DebtDiaryDbContext() : base("DebtDiaryConnectionString")
        { }

        /// <summary>
        /// Users table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Debtors
        /// </summary>
        public DbSet<Debtor> Debtors { get; set; }

        /// <summary>
        /// Transactions
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }
}
}
