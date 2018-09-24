using System;
using System.Globalization;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Core.Color"/> to <see cref="Brush"/>
    /// </summary>
    public class ColorToBrushConverter : BaseValueConverter<ColorToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Core.Color))
                return Brushes.Green;

            Core.Color color = (Core.Color)value;
            SolidColorBrush brush = (SolidColorBrush)new BrushConverter().ConvertFromString(color.ToString());

            return brush;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
