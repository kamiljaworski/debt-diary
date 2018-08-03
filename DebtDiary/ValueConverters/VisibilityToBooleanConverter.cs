using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Visibility"/> to <see cref="Boolean"/> value
    /// </summary>
    public class VisibilityToBooleanConverter : BaseValueConverter<VisibilityToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If value is Visible return true
            if (value is Visibility && ((Visibility)value) == Visibility.Visible)
                return true;

            // If not return false
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
