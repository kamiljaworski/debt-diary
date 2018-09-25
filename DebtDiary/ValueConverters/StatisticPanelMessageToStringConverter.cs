using DebtDiary.Core;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="StatisticPanelMessage"/> to <see cref="String"/>
    /// </summary>
    public class StatisticPanelMessageToStringConverter : BaseMultiValueConverter<StatisticPanelMessageToStringConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if values are correct
            if (values == null || values.Count() < 3)
                return string.Empty;

            if (!(values[0] is StatisticPanelMessage))
                return string.Empty;

            // Get values
            StatisticPanelMessage message = (StatisticPanelMessage)values[0];
            string panelValue = values[1] is string ? (string)values[1] : null;
            Gender usersGender = values[2] is Gender ? (Gender)values[2] : Gender.None;

            // Return appropriate string
            try
            {
                ResourceManager strings = new ResourceManager("DebtDiary.Localization.Strings", Assembly.GetExecutingAssembly());

                // Check if it is a debt panel and value is negative
                if (usersGender != Gender.None && (message == StatisticPanelMessage.DebtFemale || message == StatisticPanelMessage.DebtMale))
                {
                    if (panelValue == null || panelValue.Length == 0)
                        return string.Empty;

                    if (panelValue[0] == '-')
                    {
                        string resource = "Minus" + message.ToString();
                        resource += usersGender == Gender.Female ? "ToFemale" : "ToMale";
                        return strings.GetString(resource);
                    }
                }

                return strings.GetString(message.ToString());
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }


        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
