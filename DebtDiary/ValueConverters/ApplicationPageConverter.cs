using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="ApplicationPage"/> to an actual Page
    /// </summary>
    public class ApplicationPageConverter : BaseValueConverter<ApplicationPageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.LoginPage:
                    return new LoginPage();

                case ApplicationPage.RegisterPage:
                    return new RegisterPage();

                case ApplicationPage.DiaryPage:
                    return new DiaryPage();

                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
