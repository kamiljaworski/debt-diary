using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class LoginPageViewModel : BaseViewModel, ILoadable
    {
        #region Private members
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IDiaryPageViewModel _diaryPageViewModel;
        private readonly IClientDataStore _clientDataStore;
        private readonly IDataAccess _dataAccess;
        private readonly IDialogFacade _dialogFacade;

        private SecureString _password = null;
        #endregion

        #region Public Properties

        public string Username { get; set; }
        public bool IsLoginRunning { get; set; }
        public bool IsLoaded { get; private set; }
        #endregion

        #region Public Commands

        public ICommand CreateAccountCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Form Messages

        public FormMessage UsernameMessage { get; set; } = FormMessage.None;
        public FormMessage PasswordMessage { get; set; } = FormMessage.None;

        #endregion

        #region Constructor

        public LoginPageViewModel(IApplicationViewModel applicationViewModel, IDiaryPageViewModel diaryPageViewModel, IDialogFacade dialogFacade, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _diaryPageViewModel = diaryPageViewModel;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;
            _dialogFacade = dialogFacade;

            CreateAccountCommand = new RelayCommand(async () => await _applicationViewModel.ChangeCurrentPageAsync(ApplicationPage.RegisterPage));
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            IsLoaded = true;
        }
        #endregion

        #region Private Methods

        private async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => IsLoginRunning, async () =>
            {
                // Get password from the view
                _password = (parameter as IHavePassword)?.Password;

                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Get the user and add to the client data store
                if (_dataAccess.TryGetUser(Username, _password.GetEncryptedPassword(), out User loggedUser) == false)
                    return;

                _clientDataStore.LoginUser(loggedUser);

                // Update debtors list in side menu
                await Task.Run(() => _diaryPageViewModel.UpdateDebtorsList());

                // Reset entered data
                _diaryPageViewModel.UpdateUsersData();

                // Reset application subpage to SummarySubpage
                _applicationViewModel.ResetCurrentSubpage();

                // Go to DiaryPage
                await _applicationViewModel.ChangeCurrentPageAsync(ApplicationPage.DiaryPage);
            });
        }
        #endregion

        #region Helpers private methods

        private async Task<bool> ValidateDataAsync()
        {
            await Task.Run(() =>
            {
                ResetFormMessages();

                // Check if username is empty
                if (string.IsNullOrEmpty(Username))
                    UsernameMessage = FormMessage.EmptyUsername;

                // Check if password is empty
                if (_password.IsNullOrEmpty())
                    PasswordMessage = FormMessage.EmptyPassword;

                // If data is correct
                if (IsEnteredDataCorrect() && !_dataAccess.UserExist(Username, _password.GetEncryptedPassword()))
                {
                    UsernameMessage = FormMessage.IncorrectUsername;
                    PasswordMessage = FormMessage.IncorrectPassword;
                }
            });

            return IsEnteredDataCorrect();
        }

        private void ResetFormMessages()
        {
            UsernameMessage = FormMessage.None;
            PasswordMessage = FormMessage.None;
        }

        private bool IsEnteredDataCorrect()
        {
            // Check if all form messages are equal to None
            if (UsernameMessage != FormMessage.None || PasswordMessage != FormMessage.None)
                return false;

            return true;
        }
        #endregion

    }
}
