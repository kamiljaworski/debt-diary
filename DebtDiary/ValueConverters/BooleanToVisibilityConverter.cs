using System;
using System.Globalization;
using System.Security;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts Boolean to Visibility
    /// </summary>
    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Boolean && (Boolean)value == false)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
