using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="FormMessage"/> to <see cref="Visibility"/> for showing error messages in forms
    /// </summary>
    public class FormMessageToVisibilityConverter : BaseValueConverter<FormMessageToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is FormMessage enum
            if (!(value is FormMessage))
                return Visibility.Hidden;

            // If there is no form message hide element
            if ((FormMessage)value == FormMessage.None)
                return Visibility.Hidden;

            // If is show element
            return Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
