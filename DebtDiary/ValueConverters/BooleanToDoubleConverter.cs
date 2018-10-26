using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Boolean"/> to <see cref="double"/> for use in window blur effect
    /// true = 5.0
    /// false = 0.0
    /// </summary>
    public class BooleanToDoubleConverter : BaseValueConverter<BooleanToDoubleConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Boolean))
                return null;

            Boolean boolValue = (Boolean)value;

            if (boolValue == true)
                return 8.0;

            return 0.0;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
