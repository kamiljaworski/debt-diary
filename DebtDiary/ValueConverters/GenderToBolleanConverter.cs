using DebtDiary.Core;
using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Gender"/> to <see cref="Boolean"/> and <see cref="Boolean"/> to <see cref="Gender"/> back
    /// using ConverterParameter for xaml two-way RadioButton binding
    /// </summary>
    public class GenderToBolleanConverter : BaseValueConverter<GenderToBolleanConverter>
    {
        /// <summary>
        /// Converts <see cref="Gender"/> to <see cref="Boolean"/>
        /// </summary>
        /// <returns><see cref="Boolean"/></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if ConverterParameter is set
            if (!(parameter is string))
                return false;

            // Set local variables
            string converterParameter = parameter as string;
            Gender gender = (Gender)value;

            // Check if converter parameter is the same as value
            if (converterParameter == "Male" && gender == Gender.Male)
                return true;
            else if (converterParameter == "Female" && gender == Gender.Female)
                return true;

            // If not return false
            return false;
        }

        /// <summary>
        /// Converts back <see cref="Boolean"/> to <see cref="Gender"/>
        /// </summary>
        /// <returns><see cref="Gender"/></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if ConverterParameter is set
            if (!(parameter is string))
                return false;

            // Set local variables
            string converterParameter = parameter as string;
            bool isSet = (bool)value;

            // Check if converter parameter is correct and if this gender is set
            if (converterParameter == "Male" && isSet)
                return Gender.Male;
            else if (converterParameter == "Female" && isSet)
                return Gender.Female;

            // If not return null
            return null;
        }
    }
}
