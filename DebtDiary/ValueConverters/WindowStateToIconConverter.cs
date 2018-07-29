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
            // Get icons from application resouce dictionary
            string maximizeIcon = Application.Current.FindResource("MaximizeIcon") as string;
            string restoreIcon = Application.Current.FindResource("RestoreIcon") as string;

            // Return right icon
            if ((WindowState)value == WindowState.Maximized)
                return restoreIcon;
            return maximizeIcon;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
