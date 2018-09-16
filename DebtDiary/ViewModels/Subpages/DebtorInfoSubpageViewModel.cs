using DebtDiary.Core;
using DebtDiary.DataProvider;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Input;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel
    {
        private Debtor _selectedDebtor = null;

        public string FullName { get; private set; }
        public decimal Debt { get; private set; } = 0m;
        public int OperationsNumber { get; private set; } = 0;
        public decimal? LastOperation { get; private set; } = null;

        public SeriesCollection SeriesCollection { get; set; }

        public string LoanValue { get; set; }
        public string LoanDescription { get; set; }
        public ICommand AddLoanCommand { get; set; }
    

        public DebtorInfoSubpageViewModel()
        {
            AddLoanCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(LoanValue))
                    return;

                decimal.TryParse(LoanValue, out decimal loanValue);

                _selectedDebtor.Operations.Add(new Operation { Value = loanValue, Description=LoanDescription, Date = DateTime.Now });
                IocContainer.Get<IDataAccess>().SaveChanges();
                LoanDescription = string.Empty;
                LoanValue = string.Empty;
                Update();
            });
        }

        #region Public Methods

        public void Update()
        {
            _selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;
            if (_selectedDebtor == null)
                return;

            FullName = _selectedDebtor.FullName;
            Debt = _selectedDebtor.Debt;
            OperationsNumber = _selectedDebtor.Operations.Count;

            if (OperationsNumber > 0)
                LastOperation = _selectedDebtor.Operations.OrderByDescending(x => x.Date).First().Value;
            else
                LastOperation = null;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<decimal>(_selectedDebtor.GetChartPoints)
                }
            };
        }
        #endregion
    }
}
