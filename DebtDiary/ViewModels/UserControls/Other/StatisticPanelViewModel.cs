using DebtDiary.Core;

namespace DebtDiary
{
    public class StatisticPanelViewModel : IStatisticsPanel
    {
        public string Value { get; set; }
        public StatisticPanelMessage Message { get; set; }

        public StatisticPanelViewModel(StatisticPanelMessage message, string value)
        {
            Message = message;
            Value = value;
        }
    }
}
