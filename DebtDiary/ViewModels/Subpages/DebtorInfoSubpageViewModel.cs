using DebtDiary.Core;
using DebtDiary.DataProvider;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel, ILoadable
    {
        #region Private members
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IDiaryPageViewModel _diaryPageViewModel;
        private readonly IClientDataStore _clientDataStore;
        private readonly IDataAccess _dataAccess;
        private readonly IDialogFacade _dialogFacade;

        private Debtor _selectedDebtor;

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
        public Func<double, string> CurrencyFormatter { get; set; }
        #endregion

        #region Add loan form
        public string LoanValue { get; set; }
        public string LoanDescription { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public OperationType LoanOperationType { get; set; } = OperationType.DebtorsLoan;
        public ICommand AddLoanCommand { get; set; }
        public FormMessage LoanValueMessage { get; set; } = FormMessage.None;
        public FormMessage LoanDescriptionMessage { get; set; } = FormMessage.None;
        public FormMessage LoanDateMessage { get; set; } = FormMessage.None;
        public bool IsAddLoanFormRunning { get; set; } = false;
        #endregion

        #region Add repayment form
        public string RepaymentValue { get; set; }
        public string RepaymentDescription { get; set; }
        public DateTime RepaymentDate { get; set; } = DateTime.Now;
        public OperationType RepaymentOperationType { get; set; } = OperationType.DebtorsRepayment;
        public ICommand AddRepaymentCommand { get; set; }
        public FormMessage RepaymentValueMessage { get; set; } = FormMessage.None;
        public FormMessage RepaymentDescriptionMessage { get; set; } = FormMessage.None;
        public FormMessage RepaymentDateMessage { get; set; } = FormMessage.None;
        public bool IsAddRepaymentFormRunning { get; set; } = false;
        #endregion

        public ICommand EditDebtorCommand { get; set; }
        public ICommand DeleteDebtorCommand { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Constructor

        public DebtorInfoSubpageViewModel(IApplicationViewModel applicationViewModel, IDiaryPageViewModel diaryPageViewModel, IDialogFacade dialogFacade, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _diaryPageViewModel = diaryPageViewModel;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;
            _dialogFacade = dialogFacade;

            CurrencyFormatter = value => FormattingHelpers.GetFormattedCurrency(value);
            AddLoanCommand = new RelayParameterizedCommand(async x => await AddLoanAsync());
            AddRepaymentCommand = new RelayParameterizedCommand(async x => await AddRepaymentAsync());
            EditDebtorCommand = new RelayCommand(async () => await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.EditDebtorSubpage));
            DeleteDebtorCommand = new RelayCommand(async () => await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DeleteDebtorSubpage));

            UpdateChanges();
            IsLoaded = true;
        }
        #endregion

        #region Public Methods

        public void UpdateChanges()
        {
            _selectedDebtor = _applicationViewModel.SelectedDebtor;
            if (_selectedDebtor == null)
                return;

            FullName = _selectedDebtor.FullName;
            DebtorsGender = (Gender)_selectedDebtor.Gender;
            UsersGender = (Gender)_clientDataStore.LoggedUser.Gender;

            UpdateStatisticPanels();

            // To avoid STA exception in unit tests
            try
            {
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Values = new ChartValues<decimal>(_selectedDebtor.GetChartPoints()),
                        PointGeometry = DefaultGeometries.None,
                        ToolTip = null
                    }
                };
            }
            catch (InvalidOperationException) { }

            OperationsList = new ShortOperationsListViewModel(_selectedDebtor.Operations);

            if (IsLoaded == true)
                _diaryPageViewModel.UpdateDebtorsList();
        }
        #endregion

        #region Private Methods

        private async Task AddLoanAsync()
        {
            await RunCommandAsync(() => IsAddLoanFormRunning, async () =>
            {
                // Validate entered data
                if (await ValidateAddLoanDataAsync() == false)
                    return;

                // Check if there is sign change needed
                decimal value = LoanOperationType == OperationType.DebtorsLoan ? _loanValue : -_loanValue;

                Operation newOperation = new Operation { Value = value, Description = LoanDescription, AdditionDate = LoanDate, OperationType = LoanOperationType };
                _selectedDebtor.Operations.Add(newOperation);

                // Save changes in the database
                bool isDataSaved = false;
                await Task.Run(() => isDataSaved = _dataAccess.TrySaveChanges());
                if (isDataSaved == false)
                {
                    _selectedDebtor.Operations.Remove(newOperation);
                    _dialogFacade.OpenDialog(DialogMessage.NoInternetConnection);
                    return;
                }

                // Clear fields and update changes
                ResetAddLoanData();
                UpdateChanges();
            });

        }

        private async Task AddRepaymentAsync()
        {
            await RunCommandAsync(() => IsAddRepaymentFormRunning, async () =>
            {
                // Validate entered data
                if (await ValidateAddRepaymentDataAsync() == false)
                    return;

                // Check if there is sign change needed
                decimal value = RepaymentOperationType == OperationType.UsersRepayment ? _repaymentValue : -_repaymentValue;
                Operation newOperation = new Operation { Value = value, Description = RepaymentDescription, AdditionDate = RepaymentDate, OperationType = RepaymentOperationType };
                _selectedDebtor.Operations.Add(newOperation);

                // Save changes in the database
                bool isDataSaved = false;
                await Task.Run(() => isDataSaved = _dataAccess.TrySaveChanges());
                if (isDataSaved == false)
                {
                    _selectedDebtor.Operations.Remove(newOperation);
                    _dialogFacade.OpenDialog(DialogMessage.NoInternetConnection);
                    return;
                }

                // Clear fields and update changes
                ResetAddRepaymentData();
                UpdateChanges();
            });

        }
        #endregion

        #region Private helper methods

        private void UpdateStatisticPanels()
        {
            // Debt Panel
            StatisticPanelMessage debtMessage = DebtorsGender == Gender.Male ? StatisticPanelMessage.DebtMale : StatisticPanelMessage.DebtFemale;
            Debt = new StatisticPanelViewModel(debtMessage, FormattingHelpers.GetFormattedCurrency(_selectedDebtor.Debt), UsersGender);

            // Addition Date
            AdditionDate = new StatisticPanelViewModel(StatisticPanelMessage.AdditionDate, _selectedDebtor.AdditionDate.ToShortDateString());

            // Numbers of Operations
            int numberOfOperations = _selectedDebtor.Operations.Count;
            NumberOfOperations = new StatisticPanelViewModel(StatisticPanelMessage.NumberOfOperations, numberOfOperations.ToString());

            // Last Operation
            string lastOperation = numberOfOperations == 0 ? null : FormattingHelpers.GetFormattedCurrency(_selectedDebtor.Operations.OrderByDescending(x => x.AdditionDate.Date)
                                                                                                           .ThenByDescending(x => x.Id).First().Value);
            LastOperation = new StatisticPanelViewModel(StatisticPanelMessage.LastOperation, lastOperation);
        }

        private void ResetFormMessages()
        {
            LoanDescriptionMessage = FormMessage.None;
            LoanValueMessage = FormMessage.None;
            LoanDateMessage = FormMessage.None;
            RepaymentDescriptionMessage = FormMessage.None;
            RepaymentValueMessage = FormMessage.None;
            RepaymentDateMessage = FormMessage.None;
        }
        #endregion

        #region Add loan form helper methods

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

                // Check if date is correct
                if (LoanDate.Date > DateTime.Now.Date || LoanDate.Date < (DateTime.Now - TimeSpan.FromDays(365)).Date)
                    LoanDateMessage = FormMessage.IncorrectDate;

                // Check if loan value is correct and try to convert it to decimal
                if (LoanValueMessage == FormMessage.None && DataConverter.ToDecimal(LoanValue, out _loanValue) == false)
                    LoanValueMessage = FormMessage.IncorrectValue;

                // Check if loan description is correct
                if (LoanDescriptionMessage == FormMessage.None && DataValidator.IsOperationDescriptionCorrect(LoanDescription) == false)
                    LoanDescriptionMessage = FormMessage.IncorrectDescription;

                // Check if loan value is greater than zero
                if (LoanValueMessage == FormMessage.None && _loanValue < 0)
                    LoanValueMessage = FormMessage.NegativeNumber;
            });

            return IsAddLoanEnteredDataCorrect();
        }

        private bool IsAddLoanEnteredDataCorrect()
        {
            // If any of the messages changed its value return false
            if (LoanDescriptionMessage != FormMessage.None || LoanValueMessage != FormMessage.None || LoanDateMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        private void ResetAddLoanData()
        {
            LoanValue = string.Empty;
            LoanDescription = string.Empty;
            LoanOperationType = OperationType.DebtorsLoan;
            LoanDate = DateTime.Now;
        }
        #endregion

        #region Add repayment form helper methods

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

                // Check if date is correct
                if (RepaymentDate.Date > DateTime.Now.Date || RepaymentDate.Date < (DateTime.Now - TimeSpan.FromDays(365)).Date)
                    RepaymentDateMessage = FormMessage.IncorrectDate;

                // Check if repayment value is correct and try to convert it to decimal
                if (RepaymentValueMessage == FormMessage.None && DataConverter.ToDecimal(RepaymentValue, out _repaymentValue) == false)
                    RepaymentValueMessage = FormMessage.IncorrectValue;

                // Check if repayment description is correct
                if (RepaymentDescriptionMessage == FormMessage.None && DataValidator.IsOperationDescriptionCorrect(RepaymentDescription) == false)
                    RepaymentDescriptionMessage = FormMessage.IncorrectDescription;

                // Check if repayment value is greater than zero
                if (RepaymentValueMessage == FormMessage.None && _repaymentValue < 0)
                    RepaymentValueMessage = FormMessage.NegativeNumber;
            });

            return IsAddRepaymentEnteredDataCorrect();
        }

        private bool IsAddRepaymentEnteredDataCorrect()
        {
            // If any of the messages changed its value return false
            if (RepaymentDescriptionMessage != FormMessage.None || RepaymentValueMessage != FormMessage.None || RepaymentDateMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        private void ResetAddRepaymentData()
        {
            RepaymentValue = string.Empty;
            RepaymentDescription = string.Empty;
            RepaymentOperationType = OperationType.DebtorsRepayment;
            RepaymentDate = DateTime.Now;
        }
        #endregion
    }
}
