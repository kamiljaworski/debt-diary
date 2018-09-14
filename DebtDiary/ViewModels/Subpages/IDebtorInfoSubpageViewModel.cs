using DebtDiary.Core;

namespace DebtDiary
{
    public interface IDebtorInfoSubpageViewModel
    {
        string FullName  { get; }
        decimal Debt { get; }
        int OperationsNumber { get; }
        decimal LastOperation { get; }
        Debtor SelectedDebtor { get; set; }
    }
}
