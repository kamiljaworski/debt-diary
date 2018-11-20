namespace DebtDiary.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Changes first letter of a string to lower case
        /// </summary>
        public static string ToLowerFirstLetter(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string changedString = s[0].ToString();
            changedString = changedString.ToLower();
            changedString += s.Remove(0, 1);

            return changedString;
        }

        /// <summary>
        /// Changes first letter of a string to upper case
        /// </summary>
        public static string ToUpperFirstLetter(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string changedString = s[0].ToString();
            changedString = changedString.ToUpper();
            changedString += s.Remove(0, 1);

            return changedString;
        }
    }
}
