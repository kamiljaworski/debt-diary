using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public AvatarColor AvatarColor { get; set; } = AvatarColor.Green;

        public string Initials
        {
            get
            {
                // Check if first and last name has at least 1 character
                if (FirstName == null || FirstName.Count() == 0)
                    return string.Empty;

                if (LastName == null || LastName.Count() == 0)
                    return string.Empty;

                // Build Initials
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(FirstName[0], 1);
                stringBuilder.Append(LastName[0], 1);
                return stringBuilder.ToString().ToUpper();
            }
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
