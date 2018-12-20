using System;
using System.Globalization;
using System.Linq;

namespace DebtDiary
{
    /// <summary>
    /// Removes '-' when there is negative value
    /// </summary>
    public class StatisticPanelMessageValueConverter : BaseMultiValueConverter<StatisticPanelMessageValueConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if values are correct
            if (values == null || values.Count() < 2)
                return string.Empty;

            if (!(values[0] is StatisticPanelMessage))
                return string.Empty;

            if (values[1] == null)
                return string.Empty;

            // Get values
            StatisticPanelMessage message = (StatisticPanelMessage)values[0];
            string panelValue = values[1] is string ? (string)values[1] : null;

            if (message == StatisticPanelMessage.DebtFemale || message == StatisticPanelMessage.DebtMale)
                if (panelValue != null && panelValue.Length > 0 && panelValue[0] == '-')
                    return panelValue.Remove(0, 1);

            return panelValue;
        }


        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
