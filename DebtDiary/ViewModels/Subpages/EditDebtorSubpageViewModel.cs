using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class EditDebtorSubpageViewModel : BaseViewModel, ILoadable
    {
        #region Private Fields
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IDiaryPageViewModel _diaryPageViewModel;
        private readonly IDialogFacade _dialogFacade;
        private readonly IClientDataStore _clientDataStore;
        private readonly IDataAccess _dataAccess;

        private readonly Debtor _selectedDebtor;
        private readonly User _loggedUser;

        private readonly string _oldFirstName;
        private readonly string _oldLastName;
        private readonly Gender _oldGender;
        private readonly Color _oldAvatarColor;
        #endregion

        #region Public Properties

        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => FormattingHelpers.GetInitials(FirstName, LastName);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool IsEditDebtorRunning { get; set; } = false;
        #endregion

        #region Form Messages

        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
        public FormMessage GenderMessage { get; set; } = FormMessage.None;
        #endregion

        #region Public Commands

        public ICommand EditDebtorCommand { get; set; }
        public ICommand PreviousColorCommand { get; set; }
        public ICommand NextColorCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Constructor

        public EditDebtorSubpageViewModel(IApplicationViewModel applicationViewModel, IDiaryPageViewModel diaryPageViewModel, IDialogFacade dialogFacade, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _diaryPageViewModel = diaryPageViewModel;
            _dialogFacade = dialogFacade;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;

            _selectedDebtor = _applicationViewModel.SelectedDebtor;
            _loggedUser = _clientDataStore.LoggedUser;

            AvatarColor = _selectedDebtor.AvatarColor;
            FirstName = _selectedDebtor.FirstName;
            LastName = _selectedDebtor.LastName;
            Gender = (Gender)_selectedDebtor.Gender;

            _oldFirstName = FirstName;
            _oldLastName = LastName;
            _oldGender = Gender;
            _oldAvatarColor = AvatarColor;

            EditDebtorCommand = new RelayParameterizedCommand(async x => await EditDebtorAsync());
            PreviousColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Next(AvatarColor));
            GoBackCommand = new RelayCommand(async () => await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage));
            IsLoaded = true;
        }
        #endregion

        #region Private Methods

        private async Task EditDebtorAsync()
        {
            await RunCommandAsync(() => IsEditDebtorRunning, async () =>
            {
                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Edit debtors properties
                _selectedDebtor.EditPerson(FirstName, LastName, Gender, AvatarColor);

                // Save changes in the database
                bool isDataSaved = false;
                await Task.Run(() => isDataSaved = _dataAccess.TrySaveChanges());
                if (isDataSaved == false)
                {
                    _selectedDebtor.EditPerson(_oldFirstName, _oldLastName, _oldGender, _oldAvatarColor);
                    _dialogFacade.OpenDialog(DialogMessage.NoInternetConnection);
                    return;
                }

                // Update debtors list
                _diaryPageViewModel.UpdateDebtorsList();

                // Turn off spinning text
                IsEditDebtorRunning = false;

                // Show successful dialog window 
                _dialogFacade.OpenDialog(DialogMessage.DebtorEdited);

                // Go back to debtor info subpage
                await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage);

                ResetEnteredData();
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
                    if (_loggedUser.Debtors.Where(d => d.FirstName == FirstName && d.LastName == LastName && d.Id != _selectedDebtor.Id).Count() > 0)
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
