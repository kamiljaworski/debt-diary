using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class AddDebtorSubpageViewModel : BaseViewModel, ILoadable
    {
        #region Private Fields
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IDiaryPageViewModel _diaryPageViewModel;
        private readonly IDialogFacade _dialogFacade;
        private readonly IClientDataStore _clientDataStore;
        private readonly IDataAccess _dataAccess;

        private readonly User _loggedUser;
        #endregion

        #region Public Properties

        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => FormattingHelpers.GetInitials(FirstName, LastName);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool IsAddDebtorRunning { get; set; } = false;
        #endregion

        #region Form Messages

        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
        public FormMessage GenderMessage { get; set; } = FormMessage.None;
        #endregion

        #region Public Commands

        public ICommand AddDebtorCommand { get; set; }
        public ICommand PreviousColorCommand { get; set; }
        public ICommand NextColorCommand { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Constructor

        public AddDebtorSubpageViewModel(IApplicationViewModel applicationViewModel, IDiaryPageViewModel diaryPageViewModel, IDialogFacade dialogFacade, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _diaryPageViewModel = diaryPageViewModel;
            _dialogFacade = dialogFacade;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;

            _loggedUser = _clientDataStore.LoggedUser;

            AddDebtorCommand = new RelayParameterizedCommand(async x => await AddDebtorAsync());
            PreviousColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Next(AvatarColor));
            IsLoaded = true;
        }
        #endregion

        #region Private Methods

        private async Task AddDebtorAsync()
        {
            await RunCommandAsync(() => IsAddDebtorRunning, async () =>
            {
                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Make new debtor object
                Debtor debtor = new Debtor
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Gender = Gender,
                    AvatarColor = AvatarColor,
                    AdditionDate = DateTime.Now
                };

                // Add new debtor to ClientDataStore
                _loggedUser.Debtors.Add(debtor);

                // Try to save changes in the database
                bool isDataSaved = false;
                await Task.Run(() => isDataSaved = _dataAccess.TrySaveChanges());
                if (isDataSaved == false)
                {
                    _loggedUser.Debtors.Remove(debtor);
                    _dialogFacade.OpenDialog(DialogMessage.NoInternetConnection);
                    return;
                }

                // Update debtors list
                _diaryPageViewModel.UpdateDebtorsList();

                // Turn off spinning text
                IsAddDebtorRunning = false;

                // Show successful dialog window 
                _dialogFacade.OpenDialog(DialogMessage.NewDebtorAdded);

                // Clear fields in the view
                ResetEnteredData();

                // Change current subpage to new debtor info subpage
                _applicationViewModel.SelectedDebtor = debtor;
                await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage);
            });
        }

        private async Task<bool> ValidateDataAsync()
        {
            await Task.Run(() =>
            {
                // Reset all the form messages properties
                ResetFormMessages();

                // Check if first name is empty
                if (string.IsNullOrEmpty(FirstName))
                    FirstNameMessage = FormMessage.EmptyFirstName;

                // Check if last name is empty
                if (string.IsNullOrEmpty(LastName))
                    LastNameMessage = FormMessage.EmptyLastName;

                // Check if gender was selected
                if (Gender == Gender.None)
                    GenderMessage = FormMessage.UnselectedGender;

                // Check if first name is correct
                if (FirstNameMessage == FormMessage.None && DataValidator.IsNameCorrect(FirstName) == false)
                    FirstNameMessage = FormMessage.IncorrectFirstName;

                // Check if first name is correct
                if (LastNameMessage == FormMessage.None && DataValidator.IsNameCorrect(LastName) == false)
                    LastNameMessage = FormMessage.IncorrectLastName;

                // Check if there is debtor with this first and last name in db
                if (FirstNameMessage == FormMessage.None && LastNameMessage == FormMessage.None)
                    if (_dataAccess.UserAddedThisDebtor(_loggedUser.Id, FirstName, LastName))
                    {
                        FirstNameMessage = FormMessage.EmptyMessage;
                        LastNameMessage = FormMessage.DebtorExist;
                    }

            });

            // Check if any problem was found and return right value
            return IsEnteredDataCorrect();
        }

        private void ResetFormMessages()
        {
            FirstNameMessage = FormMessage.None;
            LastNameMessage = FormMessage.None;
            GenderMessage = FormMessage.None;
        }

        private bool IsEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (FirstNameMessage != FormMessage.None || LastNameMessage != FormMessage.None ||
                GenderMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        private void ResetEnteredData()
        {
            AvatarColor = Color.Green;
            FirstName = string.Empty;
            LastName = string.Empty;
            Gender = Gender.None;
        }
        #endregion
    }
}
