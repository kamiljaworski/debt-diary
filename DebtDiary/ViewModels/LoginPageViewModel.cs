using DebtDiary.Core;
using System.Security;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Login Page View Model
    /// </summary>
    class LoginPageViewModel : BaseViewModel
    {
        #region Public Properties

        public string Username { get; set; }

        public SecureString Password { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command that change current page to RegisterPage
        /// </summary>
        public ICommand CreateAccountCommand { get; set; }
        #endregion

        #region Default Constructor

        public LoginPageViewModel()
        {
            // Create commands
            CreateAccountCommand = new RelayCommand(GoToRegisterPage);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method that change current page to RegisterPage
        /// </summary>
        private void GoToRegisterPage()
        {
            IocContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPage.RegisterPage);
        }
        #endregion
    }
}
