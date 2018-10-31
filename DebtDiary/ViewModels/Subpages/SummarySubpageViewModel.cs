using DebtDiary.Core;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;

namespace DebtDiary
{
    public class SummarySubpageViewModel : BaseViewModel
    {
        private User _loggedUser = null;
        #region Public properties

        public IStatisticsPanel SumOfDebts { get; private set; }
        public IStatisticsPanel NumberOfDebtors { get; private set; }
        public IStatisticsPanel NumberOfOperations { get; private set; }
        public IStatisticsPanel LastOperation { get; private set; }

        public SeriesCollection SeriesCollection { get; set; }
        public OperationsListViewModel OperationsList { get; private set; } = null;
        public Func<double, string> CurrencyFormatter { get; set; }

        public bool IsLoaded { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SummarySubpageViewModel()
        {
            IsLoaded = false;
            CurrencyFormatter = value => Helpers.GetFormattedCurrency(value);
            _loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

            UpdateStatisticPanels();

            SeriesCollection = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values = new ChartValues<decimal>(_loggedUser.GetChartPoints),
                        PointGeometry = DefaultGeometries.None,
                        ToolTip = null
                    }
            };

            // 30 last operations ordered by AdditionDate
            OperationsList = new OperationsListViewModel(_loggedUser.Operations.OrderByDescending(x => x.AdditionDate).Take(30));
            IsLoaded = true;
        }
        #endregion


        /// <summary>
        /// Update all the statistic panels
        /// </summary>
        private void UpdateStatisticPanels()
        {
            // Sum of debts
            decimal sum = _loggedUser.Debtors.Aggregate(0m, (x, y) => x + y.Debt);
            SumOfDebts = new StatisticPanelViewModel(StatisticPanelMessage.SumOfDebts, Helpers.GetFormattedCurrency(sum));

            // Number of debtors
            NumberOfDebtors = new StatisticPanelViewModel(StatisticPanelMessage.NumberOfDebtors, _loggedUser.Debtors.Count.ToString());

            // Numbers of Operations
            int numberOfOperations = _loggedUser.Operations.Count;
            NumberOfOperations = new StatisticPanelViewModel(StatisticPanelMessage.NumberOfOperations, numberOfOperations.ToString());

            // Last Operation
            string lastOperation = numberOfOperations == 0 ? null : Helpers.GetFormattedCurrency(_loggedUser.Operations.OrderByDescending(x => x.AdditionDate).First().Value);
            LastOperation = new StatisticPanelViewModel(StatisticPanelMessage.LastOperation, lastOperation);
        }
    }
}
