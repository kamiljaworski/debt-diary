using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="HorizontalAlignment"/> to <see cref="TextAlignment"/>
    /// </summary>
    public class HorizontalAlignmentToTextAlignment : BaseValueConverter<HorizontalAlignmentToTextAlignment>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
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
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
