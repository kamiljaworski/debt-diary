using System;
using System.Text.RegularExpressions;

namespace DebtDiary.Core
{
    public static class DataValidator
    {
        /// <summary>
        /// Validate if email address is correct
        /// </summary>
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
        /// Validate if name is correct
        /// </summary>
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
        /// Validate if username is correct
        /// </summary>
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

        /// <summary>
        /// Validate if decimal number is correct
        /// </summary>
        public static bool IsDecimalNumberCorrect(string number)
        {
            // Check if number is null or empty
            if (String.IsNullOrEmpty(number))
                return false;

            // Return true if number is valid
            try
            {
                return Regex.IsMatch(number, @"\d+(\.\d{1,2})?", RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
