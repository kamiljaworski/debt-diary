using DebtDiary.Core;

namespace DebtDiary
{
    public class StatisticsPanelViewModel : IStatisticsPanel
    {
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public Color Background { get; set; }
    }
}
