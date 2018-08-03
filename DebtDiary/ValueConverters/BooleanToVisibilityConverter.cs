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
            // If value is false return visible
            if (value is Boolean && (Boolean)value == false)
                return Visibility.Visible;

            // If not - return hidden
            return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
