using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Boolean"/> to <see cref="Visibility"/>
    /// </summary>
    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If value is not a Boolean
            if (!(value is Boolean))
                return Visibility.Hidden;

            Boolean boolValue = (Boolean)value;

            // If parameter is not null then probably it is bound to button
            if (parameter != null && parameter is String)
            {
                String stringParameter = parameter as String;
                if (stringParameter == "ButtonText" && boolValue == false)
                    return Visibility.Visible;
                else if (stringParameter == "SpinningText" && boolValue == true)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }

            // If value is false return visible
            if (boolValue == false)
                return Visibility.Visible;

            // If not - return hidden
            return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
