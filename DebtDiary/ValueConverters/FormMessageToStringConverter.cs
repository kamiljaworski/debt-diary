using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="FormMessage"/> to <see cref="String"/> from localized Strings.resx resources
    /// </summary>
    public class FormMessageToStringConverter : BaseValueConverter<FormMessageToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Create a resource manager to retrieve resources.
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

                FormMessage message = (FormMessage)value;

                if (message == FormMessage.None)
                    return string.Empty;

                // Return appropriate message
                return strings.GetString(message.ToString());
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
