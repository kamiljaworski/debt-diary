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

            try
            {
                // Create a resource manager to retrieve resources.
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());
                return strings.GetString(type.ToString());
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
