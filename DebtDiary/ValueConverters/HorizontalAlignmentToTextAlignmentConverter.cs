using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="HorizontalAlignment"/> to <see cref="TextAlignment"/>
    /// </summary>
    public class HorizontalAlignmentToTextAlignmentConverter : BaseValueConverter<HorizontalAlignmentToTextAlignmentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is a HorizontalAlignment enum
            if(!(value is HorizontalAlignment))
                return TextAlignment.Center;

            // Return equivalent of HorizontalAlignment in TextAlignment
            switch ((HorizontalAlignment)value)
            {
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;

                case HorizontalAlignment.Center:
                    return TextAlignment.Center;

                case HorizontalAlignment.Right:
                    return TextAlignment.Right;

                case HorizontalAlignment.Stretch:
                    return TextAlignment.Justify;

                default:
                    return TextAlignment.Center;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
