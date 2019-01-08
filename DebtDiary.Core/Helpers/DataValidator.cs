using System;
using System.Text.RegularExpressions;

namespace DebtDiary.Core
{
    public static class DataValidator
    {
        public static bool IsEmailCorrect(string email) => IsMatch(email,
                        @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                      + "@"
                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        public static bool IsNameCorrect(string name) => IsMatch(name, @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ.' -]{2,}$");
        public static bool IsUsernameCorrect(string username) => IsMatch(username, @"^[A-Za-z0-9_-]{2,}$");
        public static bool IsDecimalNumberCorrect(string number) => IsMatch(number, @"^[+-]?\d{1,18}(\.\d{1,2})?$");
        public static bool IsOperationDescriptionCorrect(string description) => IsMatch(description, @"^[0-9A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ,.%$ -]{2,}$");

        private static bool IsMatch(string input, string pattern)
        {
            if (String.IsNullOrEmpty(input))
                return false;

            try
            {
                return Regex.IsMatch(input, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
