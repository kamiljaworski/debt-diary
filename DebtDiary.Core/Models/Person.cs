using System.ComponentModel.DataAnnotations;

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

        public string Initials => Helpers.GetInitials(FirstName, LastName);

        public string FullName => $"{FirstName} {LastName}";
    }
}
