using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="FormMessage"/> to <see cref="Boolean"/> for starting xaml animations
    /// </summary>
    public class FormMessageToBooleanConverter : BaseValueConverter<FormMessageToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is a FormMessage enum
            if (!(value is FormMessage))
                return false;

            // If there is no form message return false
            if ((FormMessage)value == FormMessage.None)
                return false;

            // If there is return true
            return true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
