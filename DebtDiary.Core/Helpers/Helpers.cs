using System.Linq;
using System.Text;

namespace DebtDiary.Core
{
    public static class Helpers
    {
        /// <summary>
        /// Get initials from first and last name
        /// </summary>
        /// <param name="firstName">First name of a person</param>
        /// <param name="lastName">Last name of a person</param>
        /// <returns>Person initials</returns>
        public static string GetInitials(string firstName, string lastName)
        {
            // Initials variables
            char firstNameInitial;
            char lastNameInitial;

            // Check if first name has at leas 1 character and if not set blank char
            if (firstName == null || firstName.Count() == 0)
                firstNameInitial = ' ';
            else
                firstNameInitial = firstName[0];

            // Check if last name has at leas 1 character and if not set blank char
            if (lastName == null || lastName.Count() == 0)
                lastNameInitial = ' ';
            else
                lastNameInitial = lastName[0];

            // Build Initials
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(firstNameInitial, 1);
            stringBuilder.Append(lastNameInitial, 1);
            return stringBuilder.ToString().ToUpper();
        }
    }
}
