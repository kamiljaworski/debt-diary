using DebtDiary.Core;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;

namespace DebtDiary
{
    public class SummarySubpageViewModel : BaseViewModel, ILoadable
    {
        private readonly User _loggedUser;

        #region Public properties

        public IStatisticsPanel SumOfDebts { get; private set; }
        public IStatisticsPanel NumberOfDebtors { get; private set; }
        public IStatisticsPanel NumberOfOperations { get; private set; }
        public IStatisticsPanel LastOperation { get; private set; }

        public SeriesCollection SeriesCollection { get; set; }
        public OperationsListViewModel OperationsList { get; private set; } = null;
        public Func<double, string> CurrencyFormatter { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Constructor

        public SummarySubpageViewModel(IClientDataStore clientDataStore)
        {
            IsLoaded = false;

            CurrencyFormatter = value => FormattingHelpers.GetFormattedCurrency(value);
            _loggedUser = clientDataStore.LoggedUser;

            UpdateStatisticPanels();

            SeriesCollection = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values = new ChartValues<decimal>(_loggedUser.GetChartPoints()),
                        PointGeometry = DefaultGeometries.None,
                        ToolTip = null
                    }
            };

            // 30 last operations ordered by AdditionDate
            OperationsList = new OperationsListViewModel(_loggedUser.Operations.OrderByDescending(x => x.AdditionDate).Take(30));
            IsLoaded = true;
        }
        #endregion

        private void UpdateStatisticPanels()
        {
            // Sum of debts
            decimal sum = _loggedUser.Debtors.Aggregate(0m, (x, y) => x + y.Debt);
            SumOfDebts = new StatisticPanelViewModel(StatisticPanelMessage.SumOfDebts, FormattingHelpers.GetFormattedCurrency(sum));

            // Number of debtors
            NumberOfDebtors = new StatisticPanelViewModel(StatisticPanelMessage.NumberOfDebtors, _loggedUser.Debtors.Count.ToString());

            // Numbers of Operations
            int numberOfOperations = _loggedUser.Operations.Count;
            NumberOfOperations = new StatisticPanelViewModel(StatisticPanelMessage.NumberOfOperations, numberOfOperations.ToString());

            // Last Operation
            string lastOperation = numberOfOperations == 0 ? null : FormattingHelpers.GetFormattedCurrency(_loggedUser.Operations.OrderByDescending(x => x.AdditionDate.Date)
                                                                                                           .ThenByDescending(x => x.Id).First().Value);
            LastOperation = new StatisticPanelViewModel(StatisticPanelMessage.LastOperation, lastOperation);
        }
    }
}
