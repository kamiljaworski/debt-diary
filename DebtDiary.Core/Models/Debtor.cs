using System;
using System.Collections.Generic;
using System.Linq;

namespace DebtDiary.Core
{
    public class Debtor : Person
    {
        public int Id { get; set; }
        public decimal Debt => Operations.Aggregate(0m, (a, b) => a + b.Value);
        public DateTime AdditionDate { get; set; }
        public User User { get; set; }
        public virtual List<Operation> Operations { get; set; } = new List<Operation>();

        public IEnumerable<decimal> GetChartPoints() => Operations.GetChartPoints(User.RegisterDate.Value);
    }
}
