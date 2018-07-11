using DebtDiary.Core;
using System.ComponentModel;
using System.Security;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Login Page View Model
    /// </summary>
    class LoginPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged public EventHandler

        // Doing nothing lambda expression to avoid compiler warning
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        #endregion

        #region Public Properties

        public string Username { get; set; }

        public SecureString Password { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command that change current page to RegisterPage
        /// </summary>
        public ICommand GoToRegisterPageCommand { get; set; }
        #endregion

        #region Default Constructor

        public LoginPageViewModel()
        {
            // Create commands
            GoToRegisterPageCommand = new RelayCommand(GoToRegisterPage);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method that change current page to RegisterPage
        /// </summary>
        private void GoToRegisterPage()
        {
            ApplicationState.MainWindowViewModel.CurrentPage = ApplicationPage.RegisterPage;
        }
        #endregion
    }
}
