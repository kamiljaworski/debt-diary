using DebtDiary.Core;

namespace DebtDiary
{
    public class StatisticsPanelViewModel : IStatisticsPanel
    {
        public string Value { get; set; }
        public Color Background { get; set; }
        public StatisticsPanelMessage Type { get; set; }
        public Gender Gender { get; set; }

        public StatisticsPanelViewModel(StatisticsPanelMessage type, Color background, string value, Gender gender = Gender.None)
        {
            Type = type;
            Background = background;
            Value = value;
            Gender = Gender;
        }
    }
}
