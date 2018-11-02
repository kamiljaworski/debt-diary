using System;
using System.Globalization;
using System.Windows.Media;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="DialogMessage"/> to <see cref="Brush"/> for icon color in dialog messages
    /// </summary>
    public class DialogMessageToBrushConverter : BaseValueConverter<DialogMessageToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If value is a DialogMessage return appropriate color
            if (value is DialogMessage)
                switch ((DialogMessage)value)
                {
                    case DialogMessage.None:
                    case DialogMessage.NoInternetConnection:
                        return Brushes.Red;

                    default:
                        return Brushes.Green;
                }

            // If not return green
            return Brushes.Green;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
