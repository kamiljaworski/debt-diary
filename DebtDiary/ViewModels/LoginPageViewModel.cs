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

        /// <summary>
        /// Command that log the user in
        /// </summary>
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Default Constructor

        public LoginPageViewModel()
        {
            // Create commands
            CreateAccountCommand = new RelayCommand(GoToRegisterPage);
            LoginCommand = new RelayCommand(GoToDiaryPage);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method that change current page to RegisterPage
        /// </summary>
        private void GoToRegisterPage()
        {
            IocContainer.Get<ApplicationViewModel>().GoToPageAsync(ApplicationPage.RegisterPage);
        }

        /// <summary>
        /// Temporary login method that only change page to the DiaryPage
        /// </summary>
        private void GoToDiaryPage()
        {
            IocContainer.Get<ApplicationViewModel>().GoToPageAsync(ApplicationPage.DiaryPage);
        }
        #endregion
    }
}
