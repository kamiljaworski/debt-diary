using System;
using System.Globalization;
using System.Linq;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="StatisticPanelMessage"/> to <see cref="Brush"/> for background color
    /// </summary>
    public class StatisticPanelMessageToBrushConverter : BaseMultiValueConverter<StatisticPanelMessageToBrushConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if values are correct
            if (values == null || values.Count() < 2)
                return Color.FromRgb(0, 128, 0);

            if (!(values[0] is StatisticPanelMessage))
                return Color.FromRgb(0, 128, 0);

            StatisticPanelMessage message = (StatisticPanelMessage)values[0];
            string panelValue = values[1] is string ? (string)values[1] : null;

            // Return appropriate color
            switch (message)
            {
                case StatisticPanelMessage.DebtMale:
                case StatisticPanelMessage.DebtFemale:
                case StatisticPanelMessage.SumOfDebts:
                    if (panelValue != null && panelValue.Length > 0 && panelValue[0] == '-')
                        return Color.FromRgb(226, 19, 29);
                    else
                        return Color.FromRgb(139, 195, 74);

                case StatisticPanelMessage.NumberOfOperations:
                    return Color.FromRgb(0, 188, 212);

                case StatisticPanelMessage.LastOperation:
                    return Color.FromRgb(233, 30, 99);

                case StatisticPanelMessage.AdditionDate:
                case StatisticPanelMessage.NumberOfDebtors:
                    return Color.FromRgb(255, 152, 0);
            }

            return Color.FromRgb(0, 128, 0);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
