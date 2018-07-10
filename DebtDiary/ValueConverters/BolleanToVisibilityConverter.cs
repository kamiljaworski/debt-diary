using System;
using System.Globalization;
using System.Security;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts Bollean of a PasswordBox attached property to Visibility
    /// </summary>
    public class BolleanToVisibilityConverter : BaseValueConverter<BolleanToVisibilityConverter>
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
