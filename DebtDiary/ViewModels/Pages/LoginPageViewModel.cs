using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Data.SqlClient;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Login Page View Model
    /// </summary>
    class LoginPageViewModel : BaseViewModel, ILoadable
    {
        #region Private members
        private IApplicationViewModel _applicationViewModel;
        private IDiaryPageViewModel _diaryPageViewModel;
        private IClientDataStore _clientDataStore;
        private IDataAccess _dataAccess;

        private User _loggedUser = null;

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

        #region Default Constructor

        public LoginPageViewModel(IApplicationViewModel applicationViewModel, IDiaryPageViewModel diaryPageViewModel, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _diaryPageViewModel = diaryPageViewModel;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;

            CreateAccountCommand = new RelayCommand(async () => await IocContainer.Get<IApplicationViewModel>().ChangeCurrentPageAsync(ApplicationPage.RegisterPage));
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

                // Validate data
                if (await ValidateDataAsync() == false)
                    return;

                // Save user in the application data
                IocContainer.Get<IClientDataStore>().LoginUser(_loggedUser);

                // Update debtors list
                await Task.Run(() => IocContainer.Get<IDiaryPageViewModel>().UpdateDebtorsList());

                // Get diary page reference from the IoC
                IDiaryPageViewModel diaryPage = IocContainer.Get<IDiaryPageViewModel>();

                // Reset users fullname, username and initials
                diaryPage.UpdateUsersData();

                // Reset application subpage to SummarySubpage
                IocContainer.Get<IApplicationViewModel>().ResetCurrentSubpage();

                // TODO: await for summary page data

                // And go to diary page
                await IocContainer.Get<IApplicationViewModel>().ChangeCurrentPageAsync(ApplicationPage.DiaryPage);

            });
        }
        #endregion

        #region Helpers private methods

        /// <summary>
        /// Validate if entered data is correct
        /// </summary>
        /// <returns>True if user can be loged in or false if not</returns>
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

                // If fields arent empty check if user exist
                if (IsEnteredDataCorrect())
                {
                    // Try to get user from db
                    _loggedUser = _dataAccess.GetUser(Username, _password.GetEncryptedPassword());

                    // Check if user was succesfully logged in
                    if (_loggedUser == null)
                    {
                        UsernameMessage = FormMessage.IncorrectUsername;
                        PasswordMessage = FormMessage.IncorrectPassword;
                    }
                }
            });

            return IsEnteredDataCorrect();
        }

        /// <summary>
        /// Reset all the <see cref="FormMessage"/> to None
        /// </summary>
        private void ResetFormMessages()
        {
            UsernameMessage = FormMessage.None;
            PasswordMessage = FormMessage.None;
        }

        /// <summary>
        /// Check if all the <see cref="FormMessage"/> properties are set to <see cref="FormMessage.None"/>
        /// </summary>
        /// <returns><see cref="bool"/> false if there are some errors and true if not</returns>
        private bool IsEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (UsernameMessage != FormMessage.None || PasswordMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }
        #endregion

    }
}
