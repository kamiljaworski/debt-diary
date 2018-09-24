namespace DebtDiary
{
    public interface IStatisticsPanel
    {
        string Value { get; set; }
        Core.Color Background { get; set; }
        StatisticsPanelMessage Message { get; set; }
    }
}
