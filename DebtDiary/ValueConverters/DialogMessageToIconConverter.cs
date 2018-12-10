using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="DialogMessage"/> to icon like: "&#xE922;"
    /// </summary>
    public class DialogMessageToIconConverter : BaseValueConverter<DialogMessageToIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Get icons from application resouce dictionary
                string successIcon = Application.Current.FindResource("SuccessIcon") as string;
                string failureIcon = Application.Current.FindResource("FailureIcon") as string;

                // If value is a DialogMessage return appropriate icon
                if (value is DialogMessage)
                    switch ((DialogMessage)value)
                    {
                        case DialogMessage.None:
                        case DialogMessage.NoInternetConnection:
                            return failureIcon;

                        default:
                            return successIcon;
                    }

                // If not return success icon
                return successIcon;
            }
            catch
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
