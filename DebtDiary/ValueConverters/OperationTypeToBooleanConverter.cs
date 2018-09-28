using DebtDiary.Core;
using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="OperationType"/> to <see cref="Boolean"/> and <see cref="Boolean"/> to <see cref="OperationType"/> back
    /// using ConverterParameter for xaml two-way RadioButton binding
    /// </summary>
    public class OperationTypeToBooleanConverter : BaseValueConverter<OperationTypeToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if ConverterParameter is set
            if (!(parameter is string))
                return false;

            // Set local variables
            string converterParameter = parameter as string;
            OperationType operationType = (OperationType)value;

            // Check if converter parameter is the same as value
            if (converterParameter == "DebtorsLoan" && operationType == OperationType.DebtorsLoan)
                return true;
            else if (converterParameter == "UsersLoan" && operationType == OperationType.UsersLoan)
                return true;
            else if (converterParameter == "DebtorsRepayment" && operationType == OperationType.DebtorsRepayment)
                return true;
            else if (converterParameter == "UsersRepayment" && operationType == OperationType.UsersRepayment)
                return true;

            // If not return false
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if ConverterParameter is set
            if (!(parameter is string))
                return false;

            // Set local variables
            string converterParameter = parameter as string;
            bool isSet = (bool)value;

            // Check if converter parameter is correct and if this gender is set
            if (converterParameter == "DebtorsLoan" && isSet)
                return OperationType.DebtorsLoan;
            else if (converterParameter == "UsersLoan" && isSet)
                return OperationType.UsersLoan;
            else if (converterParameter == "DebtorsRepayment" && isSet)
                return OperationType.DebtorsRepayment;
            else if (converterParameter == "UsersRepayment" && isSet)
                return OperationType.UsersRepayment;

            // If not return null
            return null;
        }
    }
}
