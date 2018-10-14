using DebtDiary.Core;
using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="SortType"/> to icon like: "&#xE922;"
    /// </summary>
    public class SortTypeToIconConverter : BaseValueConverter<SortTypeToIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Get icons from application resouce dictionary
                string ascendingIcon = Application.Current.FindResource("AscendingIcon") as string;
                string descendingIcon = Application.Current.FindResource("DescendingIcon") as string;

                // Return right icon
                if ((SortType)value == SortType.Ascending)
                    return ascendingIcon;
                return descendingIcon;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
