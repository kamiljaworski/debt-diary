using DebtDiary.Core;
using DebtDiary.DataProvider;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel
    {
        #region Private members
        private Debtor _selectedDebtor = null;
        #endregion

        #region Public properties

        #region Properties showed in the view
        public string FullName { get; private set; }
        public IStatisticsPanel Debt { get; private set; }
        public IStatisticsPanel AdditionDate { get; private set; }
        public IStatisticsPanel NumberOfOperations { get; private set; }
        public IStatisticsPanel LastOperation { get; private set; }
        public Gender DebtorsGender { get; private set; }
        public Gender UsersGender { get; private set; }
        #endregion

        #region Chart and operations list
        public SeriesCollection SeriesCollection { get; set; }
        public ShortOperationsListViewModel OperationsList { get; private set; } = null;
        public Func<decimal, string> CurrencyFormatter { get; set; }
        #endregion

        #region Add loan form
        public string LoanValue { get; set; }
        public string LoanDescription { get; set; }
        public OperationType LoanOperationType { get; set; } = OperationType.DebtorsLoan;
        public ICommand AddLoanCommand { get; set; }
        #endregion

        #region Add repayment form
        public string RepaymentValue { get; set; }
        public string RepaymentDescription { get; set; }
        public OperationType RepaymentOperationType { get; set; } = OperationType.DebtorsRepayment;
        public ICommand AddRepaymentCommand { get; set; }
        #endregion
        #endregion

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
            DebtorsGender = (Gender)_selectedDebtor.Gender;
            UsersGender = (Gender)IocContainer.Get<IClientDataStore>().LoggedUser.Gender;

            UpdateStatisticsPanels();


            //Debt = Helpers.GetFormattedCurrency(_selectedDebtor.Debt);
            //OperationsNumber = _selectedDebtor.Operations.Count;

            //if (OperationsNumber > 0)
            //    LastOperation = Helpers.GetFormattedCurrency(_selectedDebtor.Operations.OrderByDescending(x => x.AdditionDate).First().Value);
            //else
            //    LastOperation = null;

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

        #region Private Methods

        private void UpdateStatisticsPanels()
        {
            // Debt Panel
            StatisticPanelMessage debtMessage = DebtorsGender == Gender.Male ? StatisticPanelMessage.DebtMale : StatisticPanelMessage.DebtFemale;
            Debt = new StatisticPanelViewModel(debtMessage, Helpers.GetFormattedCurrency(_selectedDebtor.Debt), UsersGender);

            // Addition Date
            AdditionDate = new StatisticPanelViewModel(StatisticPanelMessage.AdditionDate, _selectedDebtor.AdditionDate.ToShortDateString());

            // Numbers of Operations
            int numberOfOperations = _selectedDebtor.Operations.Count;
            NumberOfOperations = new StatisticPanelViewModel(StatisticPanelMessage.NumberOfOperations, numberOfOperations.ToString());

            // Last Operation
            string lastOperation = numberOfOperations == 0 ? null : Helpers.GetFormattedCurrency(_selectedDebtor.Operations.OrderByDescending(x => x.AdditionDate).First().Value);
            LastOperation = new StatisticPanelViewModel(StatisticPanelMessage.LastOperation, lastOperation);
        }
        #endregion
    }
}
