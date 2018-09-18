using System;
using System.Globalization;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Decimal"/> to <see cref="Brush"/>
    /// </summary>
    public class DecimalToBrushConverter : BaseValueConverter<DecimalToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is decimal))
                return Brushes.LightGreen;

            decimal debt = (decimal)value;
            string valueParameter = (string)parameter;

            // If parameter is equal to "Operation" find appropriate darker color
            if (valueParameter != null && valueParameter == "Operation")
            {
                if (debt > 0)
                    return Brushes.Green;
                else if (debt < 0)
                    return Brushes.Red;
            }

            // Find appropriate color
            if (debt > 0)
                return Brushes.LightGreen;
            else if (debt < 0)
                return Brushes.Coral;
            else
                return Brushes.LightGoldenrodYellow;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
