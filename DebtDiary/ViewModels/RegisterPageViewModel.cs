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

        #region Form Messages

        /// <summary>
        /// FormMessage of a FirstName field in the view
        /// </summary>
        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;

        /// <summary>
        /// FormMessage of a LastName field in the view
        /// </summary>
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
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

        /// <summary>
        /// Command that sets FirstNameMessage to FormMessage.None
        /// </summary>
        public ICommand ResetFirstNameMessageCommand { get; set; }

        /// <summary>
        /// Command that sets LastNameMessage to FormMessage.None
        /// </summary>
        public ICommand ResetLastNameMessageCommand { get; set; }
        #endregion

        #region Default Constructor
        public RegisterPageViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(GoToLoginPage);
            SignUpCommand = new RelayParameterizedCommand((parameter) => SignUp(parameter));
            ResetFirstNameMessageCommand = new RelayCommand(() => FirstNameMessage = FormMessage.None);
            ResetLastNameMessageCommand = new RelayCommand(() => LastNameMessage = FormMessage.None);
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
            string password = (parameter as IHaveTwoPasswords)?.Password.GetEncryptedPassword();
            string repeatedPassword = (parameter as IHaveTwoPasswords)?.SecondPassword.GetEncryptedPassword();

            // Get the DataAccess reference
            IDebtDiaryDataAccess dataAccess = IocContainer.Get<IDebtDiaryDataAccess>();

            // Validate data and if there is any problem return from method
            if (ValidateData() == false)
                return;

            //if (string.IsNullOrEmpty(FirstName))
            //{
            //    FirstNameMessage = FormMessage.EmptyFirstName;
            //    return;
            //}

            //if (password != repeatedPassword)
            //    return;

            //if (dataAccess.IsUsernameAvailable(Username) == false)
            //    return;

            //if (dataAccess.IsEmailAvailable(Email) == false)
            //    return;

            // Make new user object
            User user = new User
            {
                Username = Username,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = password,
                Gender = Gender,
                RegisterDate = DateTime.Now
            };

            // Sign up a new user
            dataAccess.CreateAccount(user);

            // Clear all the fields in the view
            ClearAllFields(parameter as IHaveTwoPasswords);
        }

        #endregion

        #region Helpers private methods

        /// <summary>
        /// Helper method that validate if new user's data is correct 
        /// </summary>
        /// <returns>True if user can be added to the database or false if not</returns>
        private bool ValidateData()
        {
            // Check if first name textbox is not empty
            if (string.IsNullOrEmpty(FirstName))
                FirstNameMessage = FormMessage.EmptyFirstName;

            // Check if last name textbox is not empty
            if (string.IsNullOrEmpty(LastName))
                LastNameMessage = FormMessage.EmptyLastName;

            // If any of the messages changed it's value return false
            if (FirstNameMessage != FormMessage.None || LastNameMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
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
