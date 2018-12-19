using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts page grid's opacity to boolean to disable buttons
    /// </summary>
    public class OpacityToBooleanConverter : BaseValueConverter<OpacityToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If value is right return true
            if (value is double && (double)value > 0.9)
                return true;

            // If not return false
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
