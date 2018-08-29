using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="DialogMessage"/> to <see cref="String"/> from localized Strings.resx resources
    /// If parameter is equal to "Subtitle" then converter is looking for Subtitle strings
    /// </summary>
    public class DialogMessageToStringConverter : BaseValueConverter<DialogMessageToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Create a resource manager to retrieve resources.
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

                // Check if value is a DialogMessage enum
                if (!(value is DialogMessage))
                    return String.Empty;

                DialogMessage message = (DialogMessage)value;

                // If parameter was set to 'Subtitle' then find subtitle string and if not find title texts
                if (parameter != null && (parameter as string) == "Subtitle")
                {
                    switch (message)
                    {
                        case DialogMessage.None:
                            return String.Empty;

                        case DialogMessage.AccountCreated:
                            return strings.GetString("AccountCreatedSubtitle");

                        case DialogMessage.NewDebtorAdded:
                            return strings.GetString("NewDebtorAddedSubtitle");
                    }

                    // If value wasn't found return empty string
                    return String.Empty;
                }
                else
                {
                    switch (message)
                    {
                        case DialogMessage.None:
                            return String.Empty;

                        case DialogMessage.AccountCreated:
                            return strings.GetString("AccountCreatedTitle");

                        case DialogMessage.NewDebtorAdded:
                            return strings.GetString("NewDebtorAddedTitle");
                    }

                    // If value wasn't found return empty string
                    return String.Empty;

                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
