using System;
using System.Globalization;
using System.Security;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts SecureString length to Boolean value
    /// </summary>
    public class VisibilityToBolleanConverter : BaseValueConverter<VisibilityToBolleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility && ((Visibility)value) == Visibility.Visible)
                return true;
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
