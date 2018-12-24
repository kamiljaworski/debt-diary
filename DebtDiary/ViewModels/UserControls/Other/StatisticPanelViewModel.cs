using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public class StatisticPanelViewModel : IStatisticsPanel
    {
        public string Value { get; set; }
        public StatisticPanelMessage Message { get; set; }
        public Gender UsersGender { get; set; }

        public StatisticPanelViewModel(StatisticPanelMessage message, string value, Gender usersGender = Gender.None)
        {
            if (value == null)
                throw new ArgumentNullException();

            Message = message;
            Value = value;
            UsersGender = usersGender;
        }
    }
}
