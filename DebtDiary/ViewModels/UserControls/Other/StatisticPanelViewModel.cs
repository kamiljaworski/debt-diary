using DebtDiary.Core;

namespace DebtDiary
{
    public class StatisticsPanelViewModel : IStatisticsPanel
    {
        public string Value { get; set; }
        public Color Background { get; set; }
        public StatisticsPanelMessage Message { get; set; }

        public StatisticsPanelViewModel(StatisticsPanelMessage message, Color background, string value)
        {
            Message = message;
            Background = background;
            Value = value;
        }
    }
}
