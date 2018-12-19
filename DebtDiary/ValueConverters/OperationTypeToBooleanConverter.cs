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
            // Check if value is a OperationType enum
            if (!(value is OperationType))
                return false;

            // Check if paramater is a string
            if (!(parameter is string))
                return false;

            string param = parameter as string;
            OperationType operationType = (OperationType)value;

            // Return true is operation type is the same as parameter
            if (operationType.ToString() == param)
                return true;

            // If not return false
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is a boolean
            if (!(value is bool))
                return null;

            // Check if paramater is a string
            if (!(parameter is string))
                return null;

            // Set local variables
            string converterParameter = parameter as string;
            bool isSet = (bool)value;
            
            // Check if converter parameter is correct and if this gender is set
            if(isSet)
            {
                switch (converterParameter)
                {
                    case "DebtorsLoan":
                        return OperationType.DebtorsLoan;
                    case "UsersLoan":
                        return OperationType.UsersLoan;
                    case "DebtorsRepayment":
                        return OperationType.DebtorsRepayment;
                    case "UsersRepayment":
                        return OperationType.UsersRepayment;
                    default:
                        return null;

                }
            }

            // If not return null
            return null;
        }
    }
}
