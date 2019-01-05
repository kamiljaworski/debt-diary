using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class DeleteDebtorSubpageViewModel : BaseViewModel, ILoadable
    {
        #region Private Fields
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IDiaryPageViewModel _diaryPageViewModel;
        private readonly IDialogFacade _dialogFacade;
        private readonly IClientDataStore _clientDataStore;
        private readonly IDataAccess _dataAccess;

        private readonly User _loggedUser = null;
        private readonly Debtor _selectedDebtor = null;
        private IHavePassword _password = null;
        #endregion

        #region Public Properties

        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => FormattingHelpers.GetInitials(FirstName, LastName);
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleteDebtorRunning { get; set; } = false;

        public FormMessage PasswordMessage { get; set; } = FormMessage.None;

        public ICommand DeleteDebtorCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Constructor

        public DeleteDebtorSubpageViewModel(IApplicationViewModel applicationViewModel, IDiaryPageViewModel diaryPageViewModel, IDialogFacade dialogFacade, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _diaryPageViewModel = diaryPageViewModel;
            _dialogFacade = dialogFacade;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;

            _loggedUser = _clientDataStore.LoggedUser;
            _selectedDebtor = _applicationViewModel.SelectedDebtor;

            FirstName = _selectedDebtor.FirstName;
            LastName = _selectedDebtor.LastName;
            AvatarColor = _selectedDebtor.AvatarColor;

            DeleteDebtorCommand = new RelayParameterizedCommand(async (parameter) => await DeleteDebtorAsync(parameter));
            GoBackCommand = new RelayCommand(async () => await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage));
            IsLoaded = true;
        }
        #endregion

        #region Private Methods

        private async Task DeleteDebtorAsync(object parameter)
        {
            await RunCommandAsync(() => IsDeleteDebtorRunning, async () =>
            {
                if (!(parameter is IHavePassword))
                    return;

                // Get reference to the password
                _password = (IHavePassword)parameter;

                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Add new debtor to ClientDataStore
                _loggedUser.Debtors.Remove(_selectedDebtor);

                // Save changes in the database
                bool isDataSaved = false;
                await Task.Run(() => isDataSaved = _dataAccess.TrySaveChanges());
                if (isDataSaved == false)
                {
                    _loggedUser.Debtors.Add(_selectedDebtor);
                    _dialogFacade.OpenDialog(DialogMessage.NoInternetConnection);
                    return;
                }

                // Update debtos list
                _diaryPageViewModel.UpdateDebtorsList();

                // Turn off spinning text
                IsDeleteDebtorRunning = false;

                // Show successful dialog window 
                _dialogFacade.OpenDialog(DialogMessage.DebtorDeleted);

                // Go to summary subpage
                await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.SummarySubpage);
            });
        }

        private async Task<bool> ValidateDataAsync()
        {
            await Task.Run(() =>
            {
                // Reset all the form messages properties
                ResetFormMessages();

                // Check if password is empty
                if (_password.Password.IsNullOrEmpty())
                    PasswordMessage = FormMessage.EmptyPassword;

                if (PasswordMessage == FormMessage.None && _password.Password.GetEncryptedPassword() != _loggedUser.Password)
                    PasswordMessage = FormMessage.IncorrectPassword;
            });

            // Check if any problem was found and return right value
            return IsEnteredDataCorrect();
        }

        private void ResetFormMessages()
        {
            PasswordMessage = FormMessage.None;
        }

        private bool IsEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (PasswordMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }
        #endregion
    }
}
