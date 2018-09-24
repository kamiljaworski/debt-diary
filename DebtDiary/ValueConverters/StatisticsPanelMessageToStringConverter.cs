using DebtDiary.Core;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="StatisticsPanelMessage"/> to <see cref="String"/>
    /// </summary>
    public class StatisticsPanelMessageToStringConverter : BaseValueConverter<StatisticsPanelMessageToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get type and gender
            if (!(value is StatisticsPanelMessage))
                return string.Empty;

            StatisticsPanelMessage type = (StatisticsPanelMessage)value;
            Gender gender = Gender.None;

            if (parameter is Gender)
                return gender = (Gender)parameter;

            try
            {
                // Create a resource manager to retrieve resources.
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

                // Return appropriate message
                if (gender == Gender.None)
                    return strings.GetString(type.ToString());
                else if (gender == Gender.Female)
                    return strings.GetString(type.ToString() + "Female");
                else
                    return strings.GetString(type.ToString() + "Male");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
