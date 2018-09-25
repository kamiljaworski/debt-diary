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
                return Brushes.Green;

            if (!(values[0] is StatisticPanelMessage))
                return Brushes.Green;

            StatisticPanelMessage message = (StatisticPanelMessage)values[0];
            string panelValue = values[1] is string ? (string)values[1] : null;

            // Return appropriate color
            switch (message)
            {
                case StatisticPanelMessage.DebtMale:
                case StatisticPanelMessage.DebtFemale:
                    if (panelValue != null && panelValue.Length > 0 && panelValue[0] == '-')
                        return Brushes.OrangeRed.Color;
                    else
                        return Brushes.YellowGreen.Color;

                case StatisticPanelMessage.NumberOfOperations:
                    return Brushes.MediumTurquoise.Color;

                case StatisticPanelMessage.LastOperation:
                    return Brushes.DeepPink.Color;

                case StatisticPanelMessage.AdditionDate:
                    return Brushes.Orange.Color;
            }

            // If there wasn't returned any color return green one
            return Brushes.Green;
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
