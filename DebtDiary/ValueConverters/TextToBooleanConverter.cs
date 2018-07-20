using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts text of a TextBox to Boolean value
    /// </summary>
    public class TextToBooleanConverter : BaseValueConverter<TextToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String && String.IsNullOrEmpty((value as String)))
                return false;
            return true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
