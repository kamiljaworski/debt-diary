using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
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

        /// <summary>
        /// First name of a new user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of a new user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Username of a new user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// E-mail of a new user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gender of a new user
        /// </summary>
        public Gender Gender { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command that change current page to Login Page
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// Command that signs the user up
        /// </summary>
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region Default Constructor
        public RegisterPageViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(GoToLoginPage);
            SignUpCommand = new RelayParameterizedCommand((parameter) => SignUp(parameter));
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

        /// <summary>
        /// Method that signs the user in
        /// </summary>
        /// <param name="parameter">Parameter of a RelayParameterizedCommand</param>
        private void SignUp(object parameter)
        {
            // Get passwords from the view
            string password1 = (parameter as IHaveTwoPasswords)?.Password.GetPassword();
            string password2 = (parameter as IHaveTwoPasswords)?.SecondPassword.GetEncryptedPassword();

            // TODO: Validate data before signing up

            // Make new user object
            User user = new User
            {
                Username = this.Username,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = password2,
                Gender = this.Gender,
                RegisterDate = DateTime.Now
            };

            // Sign up a new user
            IocContainer.Get<IDebtDiaryDataAccess>().SignUp(user);

            // Clear all the fields in the view
            ClearAllFields(parameter as IHaveTwoPasswords);
        }

        /// <summary>
        /// Helper method that clears all the fields in the view
        /// </summary>
        /// <param name="twoPasswords">View that implements <see cref="IHaveTwoPasswords"/> interface you want to clear</param>
        private void ClearAllFields(IHaveTwoPasswords twoPasswords)
        { 
            Username = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Gender = 0;
            twoPasswords.ClearPassword();
        }
        #endregion
    }
}
