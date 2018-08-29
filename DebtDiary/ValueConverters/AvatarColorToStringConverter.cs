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
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

                // Check if value is a AvatarColor enum
                if (!(value is AvatarColor))
                    return String.Empty;

                // Return appropriate color
                AvatarColor color = (AvatarColor)value;
                switch (color)
                {
                    case AvatarColor.Green:
                        return strings.GetString("Green");

                    case AvatarColor.Orange:
                        return strings.GetString("Orange");

                    case AvatarColor.LightSeaGreen:
                        return strings.GetString("LightSeaGreen");

                    default:
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
