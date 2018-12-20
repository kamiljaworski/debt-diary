using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="string"/> from a TextBox to <see cref="bool"/>
    /// </summary>
    public class TextToBooleanConverter : BaseValueConverter<TextToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If string is null or empty return false
            if (string.IsNullOrEmpty(value as string))
                return false;

            // If not return true
            return true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
