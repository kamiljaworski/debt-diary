namespace DebtDiary
{
    public interface IStatisticsPanel
    {
        string Icon { get; set; }
        string Description { get; set; }
        string Value { get; set; }
        Core.Color Background { get; set; }
    }
}
