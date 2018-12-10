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
            if (value == null || !(value is decimal))
                return Brushes.LightGreen;

            decimal debt = (decimal)value;
            string parameterText = (string)parameter;

            // If this converter is used in OperationListItem return Green or Red
            if (parameterText != null && parameterText == "Operation")
            {
                if (debt > 0)
                    return Brushes.Green;
                else if (debt < 0)
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }

            // If this converter is used anywhere else return lighter colors
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
