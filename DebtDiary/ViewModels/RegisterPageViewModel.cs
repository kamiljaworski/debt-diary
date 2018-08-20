using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Security;
using System.Threading.Tasks;
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
        public Gender Gender { get; set; } = Gender.None;

        /// <summary>
        /// Property used for turning on spinning text in button
        /// </summary>
        public bool IsRegisterRunning { get; set; }

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

        /// <summary>
        /// FormMessage of a Username field in the view
        /// </summary>
        public FormMessage UsernameMessage { get; set; } = FormMessage.None;

        /// <summary>
        /// FormMessage of a Email field in the view
        /// </summary>
        public FormMessage EmailMessage { get; set; } = FormMessage.None;

        /// <summary>
        /// FormMessage of a Password field in the view
        /// </summary>
        public FormMessage PasswordMessage { get; set; } = FormMessage.None;

        /// <summary>
        /// FormMessage of a repeated Password field in the view
        /// </summary>
        public FormMessage RepeatedPasswordMessage { get; set; } = FormMessage.None;

        /// <summary>
        /// FormMessage of a Gender RadioButton in the view
        /// </summary>
        public FormMessage GenderMessage { get; set; } = FormMessage.None;
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

        #region Private members

        /// <summary>
        /// Reference to password from the view 
        /// </summary>
        private SecureString _password = null;

        /// <summary>
        /// Reference to repeated password from the view 
        /// </summary>
        private SecureString _repeatedPassword = null;

        /// <summary>
        /// Reference to application data access class
        /// </summary>
        private IDebtDiaryDataAccess _dataAccess = null;
        #endregion

        #region Default Constructor
        public RegisterPageViewModel()
        {
            // Create commands
            LoginCommand = new RelayCommand(() => ChangeApplicationPage(ApplicationPage.LoginPage));
            SignUpCommand = new RelayParameterizedCommand(async (parameter) => await SignUpAsync(parameter));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method that signs the user in
        /// </summary>
        /// <param name="parameter">Parameter of a RelayParameterizedCommand</param>
        private async Task SignUpAsync(object parameter)
        {
            await RunCommandAsync(() => IsRegisterRunning, async () =>
            {
                await Task.Delay(1);

                // Get passwords references from the view
                _password = (parameter as IHaveTwoPasswords)?.Password;
                _repeatedPassword = (parameter as IHaveTwoPasswords)?.SecondPassword;

                // Get the DataAccess reference
                _dataAccess = IocContainer.Get<IDebtDiaryDataAccess>();

                // Validate data and if there is any problem return from method
                if (await ValidateDataAsync() == false)
                    return;

                // Make new user object
                User user = new User
                {
                    Username = Username,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = _password.GetEncryptedPassword(),
                    Gender = Gender,
                    RegisterDate = DateTime.Now
                };

                // Sign up a new user
                await _dataAccess.CreateAccountAsync(user);

                // Show successful dialog window 
                IocContainer.Get<IDialogFacade>().OpenDialog();

                // Clear all the fields in the view
                ClearAllFields(parameter as IHaveTwoPasswords);

                // And go to login page
                ChangeApplicationPage(ApplicationPage.LoginPage);
            });
        }

        #endregion

        #region Helpers private methods

        /// <summary>
        /// Helper method that validate if new user's data is correct 
        /// </summary>
        /// <returns>True if user can be added to the database or false if not</returns>
        private async Task<bool> ValidateDataAsync()
        {
            await Task.Run(() =>
            {
                // Reset all the form messages properties
                ResetFormMessages();

                // Check if first name is empty
                if (string.IsNullOrEmpty(FirstName))
                    FirstNameMessage = FormMessage.EmptyFirstName;

                // Check if last name is empty
                if (string.IsNullOrEmpty(LastName))
                    LastNameMessage = FormMessage.EmptyLastName;

                // Check if username is empty
                if (string.IsNullOrEmpty(Username))
                    UsernameMessage = FormMessage.EmptyUsername;

                // Check if email is empty
                if (string.IsNullOrEmpty(Email))
                    EmailMessage = FormMessage.EmptyEmail;

                // Check if password is empty
                if (_password.IsNullOrEmpty())
                    PasswordMessage = FormMessage.EmptyPassword;

                // Check if repeated password is empty
                if (_repeatedPassword.IsNullOrEmpty())
                    RepeatedPasswordMessage = FormMessage.EmptyRepeatedPassword;

                // Check if gender was selected
                if (Gender == Gender.None)
                    GenderMessage = FormMessage.UnselectedGender;

                // Check if first name is correct
                if (FirstNameMessage == FormMessage.None && DataValidator.IsNameCorrect(FirstName) == false)
                    FirstNameMessage = FormMessage.IncorrectFirstName;

                // Check if first name is correct
                if (LastNameMessage == FormMessage.None && DataValidator.IsNameCorrect(LastName) == false)
                    LastNameMessage = FormMessage.IncorrectLastName;

                // Check if username is correct
                if (UsernameMessage == FormMessage.None && DataValidator.IsUsernameNameCorrect(Username) == false)
                    UsernameMessage = FormMessage.IncorrectUsername;

                // Check if e-mail is in correct format
                if (EmailMessage == FormMessage.None && DataValidator.IsEmailCorrect(Email) == false)
                    EmailMessage = FormMessage.IncorrectEmail;

                // Check if username is avaliable in db
                if (UsernameMessage == FormMessage.None && _dataAccess.IsUsernameAvailable(Username) == false)
                    UsernameMessage = FormMessage.TakenUsername;

                // Check if e-mail is avaliable in db
                if (EmailMessage == FormMessage.None && _dataAccess.IsEmailAvailable(Email) == false)
                    EmailMessage = FormMessage.TakenEmail;

                // Check if password is longer or equal to 8 characters
                if (PasswordMessage == FormMessage.None && RepeatedPasswordMessage == FormMessage.None &&
                    _password.Length < 8)
                {
                    PasswordMessage = FormMessage.PasswordTooShort;
                    RepeatedPasswordMessage = FormMessage.EmptyMessage;
                }

                // Check if passwords are the same
                if (PasswordMessage == FormMessage.None && RepeatedPasswordMessage == FormMessage.None &&
                    _password.GetEncryptedPassword() != _repeatedPassword.GetEncryptedPassword())
                {
                    PasswordMessage = FormMessage.DifferentPasswords;
                    RepeatedPasswordMessage = FormMessage.EmptyMessage;
                }
            });

            // Check if any problem was found and return right value
            return IsEnteredDataCorrect();
        }

        /// <summary>
        /// Helper method that resets all the <see cref="FormMessage"/> properties to <see cref="FormMessage.None"/>
        /// </summary>
        private void ResetFormMessages()
        {
            FirstNameMessage = FormMessage.None;
            LastNameMessage = FormMessage.None;
            UsernameMessage = FormMessage.None;
            EmailMessage = FormMessage.None;
            PasswordMessage = FormMessage.None;
            RepeatedPasswordMessage = FormMessage.None;
            GenderMessage = FormMessage.None;
        }

        /// <summary>
        /// Helper method that checks if all the <see cref="FormMessage"/> properties are set to <see cref="FormMessage.None"/>
        /// </summary>
        /// <returns><see cref="Boolean"/> false if there are some errors and true if not</returns>
        private bool IsEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (FirstNameMessage != FormMessage.None || LastNameMessage != FormMessage.None ||
                UsernameMessage != FormMessage.None || EmailMessage != FormMessage.None ||
                PasswordMessage != FormMessage.None || RepeatedPasswordMessage != FormMessage.None ||
                GenderMessage != FormMessage.None)
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
