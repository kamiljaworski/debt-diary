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

                // Find value and return proper text
                switch ((FormMessage)value)
                {
                    case FormMessage.EmptyFirstName:
                        return strings.GetString("EmptyFirstName");

                    case FormMessage.EmptyLastName:
                        return strings.GetString("EmptyLastName");

                    case FormMessage.EmptyUsername:
                        return strings.GetString("EmptyUsername");

                    case FormMessage.EmptyEmail:
                        return strings.GetString("EmptyEmail");

                    case FormMessage.EmptyPassword:
                        return strings.GetString("EmptyPassword");

                    case FormMessage.EmptyRepeatedPassword:
                        return strings.GetString("EmptyRepeatedPassword");

                    case FormMessage.TakenUsername:
                        return strings.GetString("TakenUsername");

                    case FormMessage.TakenEmail:
                        return strings.GetString("TakenEmail");

                    case FormMessage.DifferentPasswords:
                        return strings.GetString("DifferentPasswords");

                    case FormMessage.PasswordTooShort:
                        return strings.GetString("PasswordTooShort");

                    case FormMessage.IncorrectEmail:
                        return strings.GetString("IncorrectEmail");

                    case FormMessage.IncorrectFirstName:
                        return strings.GetString("IncorrectFirstName");

                    case FormMessage.IncorrectLastName:
                        return strings.GetString("IncorrectLastName");

                    case FormMessage.IncorrectUsername:
                        return strings.GetString("IncorrectUsername");

                    case FormMessage.UnselectedGender:
                        return strings.GetString("UnselectedGender");

                    case FormMessage.EmptyMessage:
                        return string.Empty;
                }

                // If value isn't found empty string
                return String.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
