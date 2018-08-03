using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="String"/> from a TextBox to <see cref="Visibility"/>
    /// </summary>
    public class TextToVisibilityConverter : BaseValueConverter<TextToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If string is null or empty return visible
            if (value is String && String.IsNullOrEmpty((value as String)))
                return Visibility.Visible;

            // If not return Hidden
            return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
