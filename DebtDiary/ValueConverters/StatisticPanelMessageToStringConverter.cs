using DebtDiary.Core;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="StatisticPanelMessage"/> to <see cref="String"/>
    /// </summary>
    public class StatisticPanelMessageToStringConverter : BaseValueConverter<StatisticPanelMessageToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get type and gender
            if (!(value is StatisticPanelMessage))
                return string.Empty;

            StatisticPanelMessage type = (StatisticPanelMessage)value;

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
