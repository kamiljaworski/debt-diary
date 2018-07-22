using System;
using System.Globalization;
using System.Security;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts Visibility to Boolean value
    /// </summary>
    public class VisibilityToBooleanConverter : BaseValueConverter<VisibilityToBooleanConverter>
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
