using DebtDiary.Core;
using System;
using System.Globalization;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="AvatarColor"/> to <see cref="Brush"/>
    /// </summary>
    public class AvatarColorToBrushConverter : BaseValueConverter<AvatarColorToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If value is a AvatarColor return appropriate color
            if (value is AvatarColor)
                switch ((AvatarColor)value)
                {
                    case AvatarColor.Green:
                        return Brushes.Green;

                    case AvatarColor.Orange:
                        return Brushes.Orange;

                    case AvatarColor.LightSeaGreen:
                        return Brushes.LightSeaGreen;
                }

            // If not return green
            return Brushes.Green;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
