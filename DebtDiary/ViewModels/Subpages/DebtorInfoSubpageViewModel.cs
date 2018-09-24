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
        private Debtor _selectedDebtor = null;

        public string FullName { get; private set; }

        public IStatisticsPanel Debt { get; private set; }
        public IStatisticsPanel NumerOfOperations { get; private set; }
        public IStatisticsPanel LastOperation { get; private set; }

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
            StatisticPanelMessage message = DebtorsGender == Gender.Male ? StatisticPanelMessage.DebtMale : StatisticPanelMessage.DebtFemale;
            Debt = new StatisticsPanelViewModel(message, Color.Green, Helpers.GetFormattedCurrency(_selectedDebtor.Debt));
        }
        #endregion
    }
}
