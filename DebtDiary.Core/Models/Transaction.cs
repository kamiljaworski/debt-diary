namespace DebtDiary.Core
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public Debtor Debtor { get; set; }
    }
}
