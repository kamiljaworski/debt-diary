using DebtDiary.Core;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="Gender"/> to localized <see cref="string"/>s with string name as a parameter
    /// </summary>
    public class GenderStringsConverter : BaseValueConverter<GenderStringsConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is Gender))
                return string.Empty;

            if (parameter == null || !(parameter is string) || string.IsNullOrEmpty(parameter as string))
                return string.Empty;

            // Gender dependent string have to be named: "xxxFemale" and "xxxMale"
            try
            {
                Gender gender = (Gender)value;
                string stringName = (string)parameter;
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

                if (gender == Gender.Female)
                    return strings.GetString(stringName.ToString() + "Female") ?? string.Empty;
                else
                    return strings.GetString(stringName.ToString() + "Male") ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
