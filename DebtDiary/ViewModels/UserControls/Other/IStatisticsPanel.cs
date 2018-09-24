namespace DebtDiary
{
    public interface IStatisticsPanel
    {
        string Value { get; set; }
        Core.Color Background { get; set; }
        StatisticsPanelMessage Type { get; set; }
        Core.Gender Gender { get; set; }
    }
}
