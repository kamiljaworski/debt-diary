using LiveCharts;
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

        void Update();
    }
}
