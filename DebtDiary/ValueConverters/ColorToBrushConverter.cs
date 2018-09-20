using DebtDiary.Core;
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
            // If value is a AvatarColor return appropriate color
            if (value is Core.Color)
                switch ((Core.Color)value)
                {
                    case Core.Color.Green:
                        return Brushes.Green;

                    case Core.Color.Orange:
                        return Brushes.Orange;

                    case Core.Color.LightSeaGreen:
                        return Brushes.LightSeaGreen;
                }

            // If not return green
            return Brushes.Green;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
