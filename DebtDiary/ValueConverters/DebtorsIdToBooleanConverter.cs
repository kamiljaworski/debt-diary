using DebtDiary.Core;
using System;
using System.Globalization;
using System.Linq;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="int"/>s of DebtorsId to <see cref="Boolean"/> used to DebtorsListItem isSelected property multibinding
    /// </summary>
    public class DebtorsIdToBooleanConverter : BaseMultiValueConverter<DebtorsIdToBooleanConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Count() < 2)
                return false;

            if (!(values[0] is int))
                return false;

            if (!(values[1] is int))
                return false;

            int selectedDebtorId = (int)values[0];
            int thisDebtorId = (int)values[1];

            if (selectedDebtorId == thisDebtorId)
                return true;

            return false;
        }


        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
