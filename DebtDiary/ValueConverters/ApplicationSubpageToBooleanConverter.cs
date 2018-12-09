using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="ApplicationSubpage"/> to <see cref="Boolean"/> used for binding IsSelected button property
    /// </summary>
    public class ApplicationSubpageToBooleanConverter : BaseValueConverter<ApplicationSubpageToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ApplicationSubpage))
                return false;

            if (parameter == null || !(parameter is string))
                return false;

            ApplicationSubpage subpage = (ApplicationSubpage)value;
            string buttonSubpage = (string)parameter;

            if (subpage.ToString() == buttonSubpage)
                return true;

            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
