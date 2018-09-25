using DebtDiary.Core;

namespace DebtDiary
{
    public interface IStatisticsPanel
    {
        string Value { get; set; }
        StatisticPanelMessage Message { get; set; }
        Gender UsersGender { get; set; }
    }
}
