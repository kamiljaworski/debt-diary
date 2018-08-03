using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="String"/> from a TextBox to <see cref="Boolean"/>
    /// </summary>
    public class TextToBooleanConverter : BaseValueConverter<TextToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If string is null or empty return false
            if (value is String && String.IsNullOrEmpty((value as String)))
                return false;

            // If not return true
            return true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
