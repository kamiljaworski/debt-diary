using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DebtDiary.Core
{
    public class Debtor
    {
        public int Id { get; set; }

        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        public Gender? Gender { get; set; }
        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => Helpers.GetInitials(FirstName, LastName);
        public string FullName => $"{FirstName} {LastName}";
        public decimal Debt => Operations.Aggregate(0m, (a, b) => a + b.Value);
        public DateTime AdditionDate { get; set; }
        public User User { get; set; }
        public virtual List<Operation> Operations { get; set; } = new List<Operation>();

        public IEnumerable<decimal> GetChartPoints => Operations.GetChartPoints(User.RegisterDate.Value);
    }
}
