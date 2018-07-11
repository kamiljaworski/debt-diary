using DebtDiary.Core;
using System.ComponentModel;
using System.Security;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// Register Page View Model
    /// </summary>
    class RegisterPageViewModel : INotifyPropertyChanged
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
        /// Command that change current page to Login Page
        /// </summary>
        public ICommand GoToLoginPageCommand { get; set; }
        #endregion

        #region Default Constructor
        public RegisterPageViewModel()
        {
            // Create commands
            GoToLoginPageCommand = new RelayCommand(GoToLoginPage);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method that change current page to Login Page
        /// </summary>
        private void GoToLoginPage()
        {
            ApplicationState.MainWindowViewModel.CurrentPage = ApplicationPage.LoginPage;
        }
        #endregion
    }
}
