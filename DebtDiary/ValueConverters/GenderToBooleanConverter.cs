using DebtDiary.Core;
using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Gender"/> to <see cref="Boolean"/> and <see cref="Boolean"/> to <see cref="Gender"/> back
    /// using ConverterParameter for xaml two-way RadioButton binding
    /// </summary>
    public class GenderToBooleanConverter : BaseValueConverter<GenderToBooleanConverter>
    {
        /// <summary>
        /// Converts <see cref="Gender"/> to <see cref="Boolean"/>
        /// </summary>
        /// <returns><see cref="Boolean"/></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is a Gender enum
            if (value == null || !(value is Gender))
                return false;

            // Check if ConverterParameter is set
            if (!(parameter is string))
                return false;

            // Set local variables
            string converterParameter = parameter as string;
            Gender gender = (Gender)value;

            // Check if converter parameter is the same as value
            if (gender != Gender.None && gender.ToString() == converterParameter)
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
            // Check if value is a Boolean
            if (value == null || !(value is bool))
                return Gender.None;

            // Check if ConverterParameter is set
            if (!(parameter is string))
                return Gender.None;

            // Set local variables
            string converterParameter = parameter as string;
            bool isSelected = (bool)value;

            // Check if converter parameter is correct and if this gender is set
            if (converterParameter == "Male" && isSelected)
                return Gender.Male;
            else if (converterParameter == "Female" && isSelected)
                return Gender.Female;

            // If not return null
            return Gender.None;
        }
    }
}
