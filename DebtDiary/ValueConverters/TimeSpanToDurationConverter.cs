using DebtDiary.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="TimeSpan"/> to <see cref="Duration"/> for xaml binding
    /// </summary>
    public class TimeSpanToDurationConverter : BaseValueConverter<TimeSpanToDurationConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
                return new Duration((TimeSpan)value);

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
