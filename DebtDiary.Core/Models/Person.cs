using System.ComponentModel.DataAnnotations;

namespace DebtDiary.Core
{
    public abstract class Person
    {
        [StringLength(80)]
        public string FirstName { get; set; }

        [StringLength(80)]
        public string LastName { get; set; }

        public Gender? Gender { get; set; }
        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => FormattingHelpers.GetInitials(FirstName, LastName);
        public string FullName => $"{FirstName} {LastName}";

        public void EditPerson(string firstName, string lastName, Gender gender, Color avatarColor)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            AvatarColor = avatarColor;
        }
    }
}
