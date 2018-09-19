using System.Globalization;

namespace DebtDiary.Core
{
    public static class DataConverter
    {
        /// <summary>
        /// Converts <see cref="string"/> to <see cref="decimal"/> with invariant culture
        /// </summary>
        public static bool ToDecimal(string s, out decimal result)
        {
            // Replace commas with dots
            string number = s.Replace(',', '.');

            // Check if decimal number is correct
            if (DataValidator.IsDecimalNumberCorrect(number) == false)
            {
                result = 0.0m;
                return false;
            }

            // Convert decimal number with invariant culture
            return decimal.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }
    }
}
