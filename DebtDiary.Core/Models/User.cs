using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DebtDiary.Core
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        public Gender? Gender { get; set; }
        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => Helpers.GetInitials(FirstName, LastName);
        public string FullName => $"{FirstName} {LastName}";
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

        public IEnumerable<decimal> GetChartPoints => Operations.GetChartPoints(RegisterDate.Value);
    }
}
