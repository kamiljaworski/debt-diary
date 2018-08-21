﻿using System;
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
                return Brushes.Green;

            decimal debt = (decimal)value;

            if (debt > 0)
                return Brushes.Green;
            else if (debt < 0)
                return Brushes.Coral;
            else
                return Brushes.White;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}