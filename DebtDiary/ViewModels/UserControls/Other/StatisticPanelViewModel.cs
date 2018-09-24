using DebtDiary.Core;

namespace DebtDiary
{
    public class StatisticPanelViewModel : IStatisticsPanel
    {
        public string Value { get; set; }
        public Color Background { get; set; }
        public StatisticPanelMessage Message { get; set; }

        public StatisticPanelViewModel(StatisticPanelMessage message, Color background, string value)
        {
            Message = message;
            Background = background;
            Value = value;
        }
    }
}
