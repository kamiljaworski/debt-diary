using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DebtDiary.Core
{
    /// <summary>
    /// Abstract Person class
    /// </summary>
    public abstract class Person
    {
        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string Initials
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(FirstName[0], 1);
                stringBuilder.Append(LastName[0], 1);
                return stringBuilder.ToString().ToUpper();
            }
        }
    }
}
