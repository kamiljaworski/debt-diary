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
            // Check if first and last name has at least 1 character
            if (firstName == null || firstName.Count() == 0)
                return string.Empty;

            if (lastName == null || lastName.Count() == 0)
                return string.Empty;

            // Build Initials
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(firstName[0], 1);
            stringBuilder.Append(lastName[0], 1);
            return stringBuilder.ToString().ToUpper();
        }
    }
}
