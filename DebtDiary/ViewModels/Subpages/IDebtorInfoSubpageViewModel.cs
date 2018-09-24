using DebtDiary.Core;
using LiveCharts;
using System;
using System.Windows.Input;

namespace DebtDiary
{
    public interface IDebtorInfoSubpageViewModel
    {
        string FullName  { get; }

        IStatisticsPanel Debt { get; }
        IStatisticsPanel AdditionDate { get; }
        IStatisticsPanel NumberOfOperations { get; }
        IStatisticsPanel LastOperation { get; }

        SeriesCollection SeriesCollection { get; set; }
        Gender DebtorsGender { get; }
        Gender UsersGender { get; }

        string LoanValue { get; set; }  
        string LoanDescription { get; set; }
        ICommand AddLoanCommand { get; set; }

        ShortOperationsListViewModel OperationsList { get; }

        Func<decimal, string> CurrencyFormatter { get; set; }

        void UpdateChanges();
    }
}
