using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DebtDiary.Core
{
    public class User : Person
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        public DateTime? RegisterDate { get; set; }
        public virtual List<Debtor> Debtors { get; set; } = new List<Debtor>();
        public List<Operation> Operations
        {
            get
            {
                List<Operation> operations = new List<Operation>();
                Debtors.ForEach(x => operations.AddRange(x.Operations));
                return operations.OrderByDescending(x => x.AdditionDate).ToList();
            }
        }

        public IEnumerable<decimal> GetChartPoints() => Operations.GetChartPoints(RegisterDate.Value);

        public void ChangePassword(string newEncrypedPassword)
        {
            if (string.IsNullOrEmpty(newEncrypedPassword))
                return;

            Password = newEncrypedPassword;
        }
    }
}
