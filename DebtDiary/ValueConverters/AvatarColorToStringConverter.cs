using DebtDiary.Core;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="AvatarColor"/> to <see cref="String"/> from localized Strings.resx resources
    /// </summary>
    public class AvatarColorToStringConverter : BaseValueConverter<AvatarColorToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // Create a resource manager to retrieve resources.
                ResourceManager colors = new ResourceManager("DebtDiary.Localization.Colors", Assembly.GetExecutingAssembly());

                // Check if value is a AvatarColor enum
                if (!(value is AvatarColor))
                    return String.Empty;

                // Return appropriate color
                AvatarColor color = (AvatarColor)value;
                return colors.GetString(color.ToString());
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
