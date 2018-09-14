using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public interface IDebtorInfoSubpageViewModel
    {
        Debtor SelectedDebtor { get; set; }

        string FullName  { get; }
        decimal Debt { get; }
        int OperationsNumber { get; }
        decimal? LastOperation { get; }

        string LoanValue { get; set; }  
        string LoanDescription { get; set; }
        ICommand AddLoanCommand { get; set; }
    }
}
