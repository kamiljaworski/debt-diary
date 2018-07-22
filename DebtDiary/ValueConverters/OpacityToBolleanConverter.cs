using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts page grid's opacity to boolean to disable buttons
    /// </summary>
    public class OpacityToBolleanConverter : BaseValueConverter<OpacityToBolleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((double)value > 0.9)
                return true;
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
