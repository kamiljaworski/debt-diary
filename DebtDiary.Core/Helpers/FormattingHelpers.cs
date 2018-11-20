using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DebtDiary.Core
{
    public static class FormattingHelpers
    {
        public static string GetInitials(string firstName, string lastName)
        {
            // Initials variables
            char firstNameInitial;
            char lastNameInitial;

            // Check if first name has at least 1 character and if not set blank char
            if (firstName == null || firstName.Count() == 0)
                firstNameInitial = ' ';
            else
                firstNameInitial = firstName.FirstOrDefault(c => c != ' ');

            // Check if last name has at leas 1 character and if not set blank char
            if (lastName == null || lastName.Count() == 0)
                lastNameInitial = ' ';
            else
                lastNameInitial = lastName.FirstOrDefault(c => c != ' ');

            // Build Initials
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(firstNameInitial, 1);
            stringBuilder.Append(lastNameInitial, 1);
            return stringBuilder.ToString().ToUpper();
        }

        public static string GetFormattedCurrency(decimal number)
        {
            var numberFormat = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            numberFormat.CurrencyNegativePattern = 8;
            numberFormat.CurrencyPositivePattern = 3;

            return number.ToString("C", numberFormat);
        }

        public static string GetFormattedCurrency(double number) => GetFormattedCurrency(Convert.ToDecimal(number));
    }
}
