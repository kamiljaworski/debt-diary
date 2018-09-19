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
        public string Debt { get; private set; }
        public int OperationsNumber { get; private set; } = 0;
        public string LastOperation { get; private set; } = null;
        public SeriesCollection SeriesCollection { get; set; }
        public Gender DebtorsGender { get; private set; }
        public Gender UsersGender { get; private set; }

        public string LoanValue { get; set; }
        public string LoanDescription { get; set; }
        public ICommand AddLoanCommand { get; set; }

        public ShortOperationsListViewModel OperationsList { get; private set; } = null;

        public Func<decimal, string> CurrencyFormatter { get; set; }



        public DebtorInfoSubpageViewModel()
        {
            CurrencyFormatter = value => Helpers.GetFormattedCurrency(value);

            AddLoanCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(LoanValue))
                    return;

                if (DataConverter.ToDecimal(LoanValue, out decimal value) == false)
                    return;

                _selectedDebtor.Operations.Add(new Operation { Value = value, Description = LoanDescription, AdditionDate = DateTime.Now });
                IocContainer.Get<IDataAccess>().SaveChanges();
                LoanDescription = string.Empty;
                LoanValue = string.Empty;
                UpdateChanges();
            });
        }

        #region Public Methods

        public void UpdateChanges()
        {
            // TODO: Update changes in another threat
            _selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;
            if (_selectedDebtor == null)
                return;

            FullName = _selectedDebtor.FullName;
            Debt = Helpers.GetFormattedCurrency(_selectedDebtor.Debt);
            OperationsNumber = _selectedDebtor.Operations.Count;
            DebtorsGender = (Gender)_selectedDebtor.Gender;
            UsersGender = (Gender)IocContainer.Get<IClientDataStore>().LoggedUser.Gender;

            if (OperationsNumber > 0)
                LastOperation = Helpers.GetFormattedCurrency(_selectedDebtor.Operations.OrderByDescending(x => x.AdditionDate).First().Value);
            else
                LastOperation = null;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<decimal>(_selectedDebtor.GetChartPoints),
                    PointGeometry = DefaultGeometries.None,
                    ToolTip = null
                }
            };

            OperationsList = new ShortOperationsListViewModel(_selectedDebtor);

            IocContainer.Get<DebtorsListViewModel>().UpdateChanges();
        }
        #endregion
    }
}
