using System.Collections.Generic;
using System.Linq;

namespace DebtDiary.Core
{
    /// <summary>
    /// Debtor
    /// </summary>
    public class Debtor : Person
    {
        public int Id { get; set; }

        public decimal Debt => Operations.Aggregate(0m, (a, b) => a + b.Value);

        public User User { get; set; }

        public virtual List<Operation> Operations { get; set; } = new List<Operation>();
    }
}
