namespace DebtDiary
{
    public interface IStatisticsPanel
    {
        string Value { get; set; }
        StatisticPanelMessage Message { get; set; }
    }
}
