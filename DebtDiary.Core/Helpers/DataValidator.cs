using System;
using System.Text.RegularExpressions;

namespace DebtDiary.Core
{
    /// <summary>
    /// Static class used for data validation
    /// </summary>
    public static class DataValidator
    {
        /// <summary>
        /// Method that validates email correctness - it must look like this: 'xxx@xxx.xxx'
        /// </summary>
        /// <param name="email">E-mail address you want to validate</param>
        /// <returns><see cref="Boolean"> true if e-mail is correct or false if not</returns>
        public static bool IsEmailCorrect(string email)
        {
            // Check if email is null or empty
            if (String.IsNullOrEmpty(email))
                return false;

            // Return true if email is in valid email format
            try
            {
                return Regex.IsMatch(email,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                // If there was any exception thrown return false
                return false;
            }
        }

        /// <summary>
        /// Method that validates first and last name correctness
        /// </summary>
        /// <param name="name">First or last name you want to check</param>
        /// <returns><see cref="Boolean"> true if name is correct or false if not</returns>
        public static bool IsNameCorrect(string name)
        {
            // Check if name is null or empty
            if (String.IsNullOrEmpty(name))
                return false;

            // Return true if name is valid
            try
            {
                return Regex.IsMatch(name, @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]{2,}$", RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                // If there was any exception thrown return false
                return false;
            }
        }


        /// <summary>
        /// Method that validates username correctness
        /// </summary>
        /// <param name="username">Username you want to check</param>
        /// <returns><see cref="Boolean"> true if username is correct or false if not</returns>
        public static bool IsUsernameNameCorrect(string username)
        {
            // Check if name is null or empty
            if (String.IsNullOrEmpty(username))
                return false;

            // Return true if name is valid
            try
            {
                return Regex.IsMatch(username, @"^[A-Za-z0-9]{2,}$", RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                // If there was any exception thrown return false
                return false;
            }
        }
    }
}
