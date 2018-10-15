using DebtDiary.Core;
using LiveCharts;
using LiveCharts.Wpf;
using System;

namespace DebtDiary
{
    public class SummarySubpageViewModel : BaseViewModel
    {
        #region Public properties

        public IStatisticsPanel Debt { get; private set; }
        public IStatisticsPanel AdditionDate { get; private set; }
        public IStatisticsPanel NumberOfOperations { get; private set; }
        public IStatisticsPanel LastOperation { get; private set; }

        public SeriesCollection SeriesCollection { get; set; }
        public ShortOperationsListViewModel OperationsList { get; private set; } = null;
        public Func<double, string> CurrencyFormatter { get; set; }

        #endregion

        #region Constructor

        public SummarySubpageViewModel()
        {
            CurrencyFormatter = value => Helpers.GetFormattedCurrency(value);
            User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

            SeriesCollection = new SeriesCollection
            {
                    new LineSeries
                    {
                        Values = new ChartValues<decimal>(loggedUser.GetChartPoints),
                        PointGeometry = DefaultGeometries.None,
                        ToolTip = null
                    }
            };
        }
        #endregion
    }
}
