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
                await Task.Delay(100);
                _password = (parameter as IHavePassword)?.Password;


            });
        }


        #region Helpers private methods

        /// <summary>
        /// Validate if entered data is correct
        /// </summary>
        /// <returns>True if user can be loged in or false if not</returns>
        private async Task<bool> ValidateDataAsync()
        {
            await Task.Run(() =>
            {
                return true;
            });
            return true;
            
        }
        #endregion
        #endregion
    }
}
