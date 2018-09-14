using DebtDiary.Core;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel
    {
        public string FullName => SelectedDebtor?.FullName;
        public Debtor SelectedDebtor { get; set; } = null;
    }
}
