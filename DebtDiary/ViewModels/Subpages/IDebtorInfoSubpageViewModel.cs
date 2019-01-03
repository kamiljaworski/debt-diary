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
        Func<double, string> CurrencyFormatter { get; set; }

        // Add loan form
        string LoanValue { get; set; }
        string LoanDescription { get; set; }
        DateTime LoanDate { get; set; }
        OperationType LoanOperationType { get; set; }
        ICommand AddLoanCommand { get; set; }
        FormMessage LoanValueMessage { get; set; }
        FormMessage LoanDescriptionMessage { get; set; }
        FormMessage LoanDateMessage { get; set; }
        bool IsAddLoanFormRunning { get; set; }

        // Add repayment form
        string RepaymentValue { get; set; }
        string RepaymentDescription { get; set; }
        DateTime RepaymentDate { get; set; }
        OperationType RepaymentOperationType { get; set; }
        ICommand AddRepaymentCommand { get; set; }
        FormMessage RepaymentValueMessage { get; set; }
        FormMessage RepaymentDescriptionMessage { get; set; }
        FormMessage RepaymentDateMessage { get; set; }
        bool IsAddRepaymentFormRunning { get; set; }

        // Edit and Delete debtor commands
        ICommand EditDebtorCommand { get; set; }
        ICommand DeleteDebtorCommand { get; set; }

        bool IsLoaded { get; }

        // Public methods
        void UpdateChanges();
    }
}
