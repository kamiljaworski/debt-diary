using DebtDiary.Core;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Login Page View Model
    /// </summary>
    class LoginPageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Username to log in
        /// </summary>
        public string Username { get; set; }

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

        private async Task LoginAsync(object parameter)
        {
            string username = Username;
            string password = (parameter as IHavePassword).Password.GetEncryptedPassword();

            ChangeApplicationPage(ApplicationPage.DiaryPage);
        }
        #endregion
    }
}
