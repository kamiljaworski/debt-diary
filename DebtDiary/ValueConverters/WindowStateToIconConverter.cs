using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="WindowState"/> to icon like: "&#xE922;"
    /// </summary>
    public class WindowStateToIconConverter : BaseValueConverter<WindowStateToIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((WindowState)value == WindowState.Maximized)
                return "\uE923";
            return "\uE922";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
