using DebtDiary.Core;
using System.Security;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Register Page View Model
    /// </summary>
    class RegisterPageViewModel : BaseViewModel
    {
        #region Public Properties

        public string Username { get; set; }

        public SecureString Password { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command that change current page to Login Page
        /// </summary>
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Default Constructor
        public RegisterPageViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(GoToLoginPage);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method that change current page to Login Page
        /// </summary>
        private void GoToLoginPage()
        {
            IocContainer.Get<ApplicationViewModel>().GoToPageAsync(ApplicationPage.LoginPage);
        }
        #endregion
    }
}
