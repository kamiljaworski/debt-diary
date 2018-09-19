using LiveCharts;
using System;
using System.Windows.Input;

namespace DebtDiary
{
    public interface IDebtorInfoSubpageViewModel
    {
        string FullName  { get; }
        decimal Debt { get; }
        int OperationsNumber { get; }
        decimal? LastOperation { get; }
        SeriesCollection SeriesCollection { get; set; }

        string LoanValue { get; set; }  
        string LoanDescription { get; set; }
        ICommand AddLoanCommand { get; set; }

        ShortOperationsListViewModel OperationsList { get; }

        Func<decimal, string> CurrencyFormatter { get; set; }

        void UpdateChanges();
    }
}
