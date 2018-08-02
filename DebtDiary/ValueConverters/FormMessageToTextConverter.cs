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
            // Create a resource manager to retrieve resources.
            ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

            // Find value and return proper text
            switch ((FormMessage)value)
            {
                case FormMessage.EmptyFirstName:
                    return strings.GetString("EmptyFirstName");

                case FormMessage.EmptyLastName:
                    return strings.GetString("EmptyLastName");

                case FormMessage.EmptyUsername:
                    break;

                case FormMessage.EmptyEmail:
                    break;

                case FormMessage.EmptyPassword:
                    break;

                case FormMessage.EmptyRepeatedPassword:
                    break;
            }

            // If value isn't found empty string
            return String.Empty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
