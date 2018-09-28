using DebtDiary.Core;
using LiveCharts;
using System;
using System.Windows.Input;

namespace DebtDiary
{
    public interface IDebtorInfoSubpageViewModel
    {
        // Properties showed in the view
        string FullName  { get; }
        IStatisticsPanel Debt { get; }
        IStatisticsPanel AdditionDate { get; }
        IStatisticsPanel NumberOfOperations { get; }
        IStatisticsPanel LastOperation { get; }
        Gender DebtorsGender { get; }
        Gender UsersGender { get; }

        // Chart and operations list
        SeriesCollection SeriesCollection { get; set; }
        ShortOperationsListViewModel OperationsList { get; }
        Func<decimal, string> CurrencyFormatter { get; set; }

        // Add loan form
        string LoanValue { get; set; }
        string LoanDescription { get; set; }
        OperationType LoanOperationType { get; set; }
        ICommand AddLoanCommand { get; set; }

        // Add repayment form
        string RepaymentValue { get; set; }
        string RepaymentDescription { get; set; }
        OperationType RepaymentOperationType { get; set; }
        ICommand AddRepaymentCommand { get; set; }

        // Public methods
        void UpdateChanges();
    }
}
