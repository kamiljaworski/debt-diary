using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DebtDiary.Core
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? RegisterDate { get; set; }

        public string Initials
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(FirstName[0], 1);
                stringBuilder.Append(LastName[0], 1);
                return stringBuilder.ToString();
            }
        }
    }
}
