namespace DebtDiary
{
    public interface IStatisticsPanel
    {
        string Value { get; set; }
        Core.Color Background { get; set; }
        StatisticPanelMessage Message { get; set; }
    }
}
