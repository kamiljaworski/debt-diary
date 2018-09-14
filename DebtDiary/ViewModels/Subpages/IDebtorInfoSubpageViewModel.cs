using DebtDiary.Core;

namespace DebtDiary
{
    public interface IDebtorInfoSubpageViewModel
    {
        string FullName  { get; }
        Debtor SelectedDebtor { get; set; }
    }
}
