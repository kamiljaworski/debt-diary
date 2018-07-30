using DebtDiary.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="ApplicationPage"/> to title bar background color
    /// </summary>
    public class ApplicationPageToTitleBarBackgroundConverter : BaseValueConverter<ApplicationPageToTitleBarBackgroundConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush darkGrey = Application.Current.FindResource("DarkGreyBrush") as SolidColorBrush;
            SolidColorBrush lightGrey = Application.Current.FindResource("VeryLightGreyBrush") as SolidColorBrush;

            switch ((ApplicationPage)value)
            {
                case ApplicationPage.DiaryPage:
                    return lightGrey;

                default:
                    return darkGrey;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
