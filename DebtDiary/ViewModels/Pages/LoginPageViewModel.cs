using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Login Page View Model
    /// </summary>
    class LoginPageViewModel : BaseViewModel
    {
        #region Private members

        /// <summary>
        /// Reference to password from the view 
        /// </summary>
        private SecureString _password = null;

        /// <summary>
        /// Reference to application data access class
        /// </summary>
        private IDebtDiaryDataAccess _dataAccess = IocContainer.Get<IDebtDiaryDataAccess>();

        /// <summary>
        /// Logged user from login method
        /// </summary>
        private User _loggedUser = null;
        #endregion

        #region Public Properties

        /// <summary>
        /// Username to log in
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Property used for turning on spinning text in button
        /// </summary>
        public bool IsLoginRunning { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command that change current page to RegisterPage
        /// </summary>
        public ICommand CreateAccountCommand { get; set; }

        /// <summary>
        /// Command that log the user in
        /// </summary>
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Form Messages

        /// <summary>
        /// FormMessage of a Username field in the view
        /// </summary>
        public FormMessage UsernameMessage { get; set; } = FormMessage.None;

        /// <summary>
        /// FormMessage of a Password field in the view
        /// </summary>
        public FormMessage PasswordMessage { get; set; } = FormMessage.None;

        #endregion

        #region Default Constructor

        public LoginPageViewModel()
        {
            // Create commands
            CreateAccountCommand = new RelayCommand(() => ChangeApplicationPage(ApplicationPage.RegisterPage));
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Log the user in
        /// </summary>
        /// <param name="parameter">View as <see cref="IHavePassword"/></param>
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

                // And go to diary page
                ChangeApplicationPage(ApplicationPage.DiaryPage);
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

                // Try to get user from db
                _loggedUser = _dataAccess.GetUser(Username, _password.GetEncryptedPassword());

                // Check if user was succesfully logged in
                if (_loggedUser == null)
                {
                    UsernameMessage = FormMessage.IncorrectUsername;
                    PasswordMessage = FormMessage.IncorrectPassword;
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
