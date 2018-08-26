namespace DebtDiary.Core
{
    /// <summary>
    /// Debtor
    /// </summary>
    public class Debtor : Person
    {
        public int Id { get; set; }

        public decimal Debt { get; set; } = 0;

        public User User { get; set; }


    }
}
