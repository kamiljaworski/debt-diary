using System;
using System.Globalization;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="StatisticPanelMessage"/> to icon like: "&#xE922;"
    /// </summary>
    public class StatisticPanelMessageToIconConverter : BaseValueConverter<StatisticPanelMessageToIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (!(value is StatisticPanelMessage))
                    return string.Empty;

                switch ((StatisticPanelMessage)value)
                {
                    case StatisticPanelMessage.DebtFemale:
                    case StatisticPanelMessage.DebtMale:
                    case StatisticPanelMessage.SumOfDebts:
                        return Application.Current.FindResource("TrackersIcon") as string;

                    case StatisticPanelMessage.NumberOfOperations:
                        return Application.Current.FindResource("DictionaryIcon") as string;

                    case StatisticPanelMessage.LastOperation:
                        return Application.Current.FindResource("DictionaryAddIcon") as string;

                    case StatisticPanelMessage.AdditionDate:
                        return Application.Current.FindResource("AddFriendIcon") as string;

                    case StatisticPanelMessage.NumberOfDebtors:
                        return Application.Current.FindResource("GroupIcon") as string;

                    default:
                        return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
