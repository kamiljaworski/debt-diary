using DebtDiary.Core;
using DebtDiary.DataProvider;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel
    {
        #region Private members
        private Debtor _selectedDebtor = null;

        private decimal _loanValue;
        private decimal _repaymentValue;
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
        public FormMessage LoanValueMessage { get; set; } = FormMessage.None;
        public FormMessage LoanDescriptionMessage { get; set; } = FormMessage.None;
        public bool IsAddLoanFormRunning { get; set; } = false;
        #endregion

        #region Add repayment form
        public string RepaymentValue { get; set; }
        public string RepaymentDescription { get; set; }
        public OperationType RepaymentOperationType { get; set; } = OperationType.DebtorsRepayment;
        public ICommand AddRepaymentCommand { get; set; }
        public FormMessage RepaymentValueMessage { get; set; } = FormMessage.None;
        public FormMessage RepaymentDescriptionMessage { get; set; } = FormMessage.None;
        public bool IsAddRepaymentFormRunning { get; set; } = false;
        #endregion
        #endregion

        #region Constructor

        public DebtorInfoSubpageViewModel()
        {
            CurrencyFormatter = value => Helpers.GetFormattedCurrency(value);
            AddLoanCommand = new RelayCommand(async () => await AddLoanAsync());
            AddRepaymentCommand = new RelayCommand(async () => await AddRepaymentAsync());
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Update displayed data in the view
        /// </summary>
        public void UpdateChanges()
        {
            _selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;
            if (_selectedDebtor == null)
                return;

            FullName = _selectedDebtor.FullName;
            DebtorsGender = (Gender)_selectedDebtor.Gender;
            UsersGender = (Gender)IocContainer.Get<IClientDataStore>().LoggedUser.Gender;

            UpdateStatisticPanels();

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

        /// <summary>
        /// Add new loan to the users operations list
        /// </summary>
        /// <returns></returns>
        private async Task AddLoanAsync()
        {
            await RunCommandAsync(() => IsAddLoanFormRunning, async () =>
            {
                // Validate entered data
                if (await ValidateAddLoanDataAsync() == false)
                    return;

                // Check if there is sign change needed
                decimal value = LoanOperationType == OperationType.DebtorsLoan ? _loanValue : -_loanValue;
                _selectedDebtor.Operations.Add(new Operation { Value = value, Description = LoanDescription, AdditionDate = DateTime.Now, OperationType = LoanOperationType });

                // Save changes in the database
                await Task.Run(() => IocContainer.Get<IDataAccess>().SaveChanges());

                // Clear fields and update changes
                ClearAddLoanFields();
                UpdateChanges();
            });

        }

        /// <summary>
        /// Add new repayment to the users operations list
        /// </summary>
        /// <returns></returns>
        private async Task AddRepaymentAsync()
        {
            await RunCommandAsync(() => IsAddRepaymentFormRunning, async () =>
            {
                // Validate entered data
                if (await ValidateAddRepaymentDataAsync() == false)
                    return;

                // Check if there is sign change needed
                decimal value = RepaymentOperationType == OperationType.UsersRepayment ? _repaymentValue : -_repaymentValue;
                _selectedDebtor.Operations.Add(new Operation { Value = value, Description = RepaymentDescription, AdditionDate = DateTime.Now, OperationType = RepaymentOperationType });

                // Save changes in the database
                await Task.Run(() => IocContainer.Get<IDataAccess>().SaveChanges());

                // Clear fields and update changes
                ClearAddRepaymentFields();
                UpdateChanges();
            });

        }
        #endregion

        #region Private helper methods

        /// <summary>
        /// Update all the statistic panels
        /// </summary>
        private void UpdateStatisticPanels()
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

        /// <summary>
        /// Reset all the form messages
        /// </summary>
        private void ResetFormMessages()
        {
            LoanDescriptionMessage = FormMessage.None;
            LoanValueMessage = FormMessage.None;
            RepaymentDescriptionMessage = FormMessage.None;
            RepaymentValueMessage = FormMessage.None;
        }
        #endregion

        #region Add loan form helper methods

        /// <summary>
        /// Validate entered data
        /// </summary>
        private async Task<bool> ValidateAddLoanDataAsync()
        {
            await Task.Run(() =>
            {
                ResetFormMessages();

                // Check if loan value is empty
                if (string.IsNullOrEmpty(LoanValue))
                    LoanValueMessage = FormMessage.EmptyValue;

                // Check if loan description is empty
                if (string.IsNullOrEmpty(LoanDescription))
                    LoanDescriptionMessage = FormMessage.EmptyDescription;

                // Check if loan value is correct and try to convert it to decimal
                if (LoanValueMessage == FormMessage.None && DataConverter.ToDecimal(LoanValue, out _loanValue) == false)
                    LoanValueMessage = FormMessage.IncorrectValue;

                // Check if loan description is correct
                if (LoanDescriptionMessage != FormMessage.None && DataValidator.IsNameCorrect(LoanDescription) == false)
                    LoanDescriptionMessage = FormMessage.IncorrectDescription;

                // Check if loan value is greater than zero
                if (LoanValueMessage == FormMessage.None && _loanValue < 0)
                    LoanValueMessage = FormMessage.NegativeNumber;
            });

            return IsAddLoanEnteredDataCorrect();
        }

        /// <summary>
        /// Check if add loan form entered data is correct
        /// </summary>
        private bool IsAddLoanEnteredDataCorrect()
        {
            // If any of the messages changed its value return false
            if (LoanDescriptionMessage != FormMessage.None || LoanValueMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        /// <summary>
        /// Clear add loan form properties
        /// </summary>
        private void ClearAddLoanFields()
        {
            LoanValue = string.Empty;
            LoanDescription = string.Empty;
            LoanOperationType = OperationType.DebtorsLoan;
        }
        #endregion

        #region Add repayment form helper methods

        /// <summary>
        /// Validate entered data
        /// </summary>
        private async Task<bool> ValidateAddRepaymentDataAsync()
        {
            await Task.Run(() =>
            {
                ResetFormMessages();

                // Check if repayment value is empty
                if (string.IsNullOrEmpty(RepaymentValue))
                    RepaymentValueMessage = FormMessage.EmptyValue;

                // Check if repayment description is empty
                if (string.IsNullOrEmpty(RepaymentDescription))
                    RepaymentDescriptionMessage = FormMessage.EmptyDescription;

                // Check if repayment value is correct and try to convert it to decimal
                if (RepaymentValueMessage == FormMessage.None && DataConverter.ToDecimal(RepaymentValue, out _repaymentValue) == false)
                    RepaymentValueMessage = FormMessage.IncorrectValue;

                // Check if repayment description is correct
                if (RepaymentDescriptionMessage != FormMessage.None && DataValidator.IsNameCorrect(RepaymentDescription) == false)
                    RepaymentDescriptionMessage = FormMessage.IncorrectDescription;

                // Check if repayment value is greater than zero
                if (RepaymentValueMessage == FormMessage.None && _repaymentValue < 0)
                    RepaymentValueMessage = FormMessage.NegativeNumber;
            });

            return IsAddRepaymentEnteredDataCorrect();
        }

        /// <summary>
        /// Check if add repayment form entered data is correct
        /// </summary>
        private bool IsAddRepaymentEnteredDataCorrect()
        {
            // If any of the messages changed its value return false
            if (RepaymentDescriptionMessage != FormMessage.None || RepaymentValueMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        /// <summary>
        /// Clear add repayment form properties
        /// </summary>
        private void ClearAddRepaymentFields()
        {
            RepaymentValue = string.Empty;
            RepaymentDescription = string.Empty;
            RepaymentOperationType = OperationType.DebtorsRepayment;
        }
        #endregion
    }
}
