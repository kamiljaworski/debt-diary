using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="TimeSpan"/> to <see cref="Duration"/> for xaml binding
    /// </summary>
    public class TimeSpanToDurationConverter : BaseValueConverter<TimeSpanToDurationConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is a TimeSpan class and return its equivalent as a Duration
            if (value is TimeSpan)
                return new Duration((TimeSpan)value);

            // If it is not a TimeSpan return null
            return new Duration(TimeSpan.Zero);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
