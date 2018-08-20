using DebtDiary.Core;
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
            LoginCommand = new RelayParameterizedCommand((parameter) => Login(parameter));
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method that log the user in
        /// </summary>
        private void Login(object parameter)
        {
            //string username = Username;
            //string password = (parameter as IHavePassword).Password.GetEncryptedPassword();

            //ChangeApplicationPage(ApplicationPage.DiaryPage);

            var result = IocContainer.Get<IApplicationViewModel>().OpenDialog();
        }
        #endregion
    }
}
