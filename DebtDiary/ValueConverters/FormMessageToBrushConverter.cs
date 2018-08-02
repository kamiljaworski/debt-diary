using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="FormMessage"/> to <see cref="Brush"/> for coloring border in textboxes when <see cref="FormMessage"/> is set
    /// </summary>
    public class FormMessageToBrushConverter : BaseValueConverter<FormMessageToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If form message is set return red brush
            if ((FormMessage)value != FormMessage.None)
                return Brushes.Red;

            // If not return null
            return Application.Current.FindResource("DarkGreyBrush") as Brush;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
