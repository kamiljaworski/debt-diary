using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class RegisterPageViewModel : BaseViewModel, ILoadable
    {
        #region Private members
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IDialogFacade _dialogFacade;
        private readonly IDataAccess _dataAccess;

        private SecureString _password;
        private SecureString _repeatedPassword;
        #endregion

        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool IsRegisterRunning { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Form Messages

        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
        public FormMessage UsernameMessage { get; set; } = FormMessage.None;
        public FormMessage EmailMessage { get; set; } = FormMessage.None;
        public FormMessage PasswordMessage { get; set; } = FormMessage.None;
        public FormMessage RepeatedPasswordMessage { get; set; } = FormMessage.None;
        public FormMessage GenderMessage { get; set; } = FormMessage.None;
        #endregion

        #region Public Commands

        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region Constructor

        public RegisterPageViewModel(IApplicationViewModel applicationViewModel, IDialogFacade dialogFacade, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _applicationViewModel = applicationViewModel;
            _dialogFacade = dialogFacade;
            _dataAccess = dataAccess;

            LoginCommand = new RelayCommand(async () => await _applicationViewModel.ChangeCurrentPageAsync(ApplicationPage.LoginPage));
            SignUpCommand = new RelayParameterizedCommand(async (parameter) => await SignUpAsync(parameter));
            IsLoaded = true;
        }
        #endregion

        #region Private Methods

        private async Task SignUpAsync(object parameter)
        {
            await RunCommandAsync(() => IsRegisterRunning, async () =>
            {
                // Get passwords from the view
                _password = (parameter as IHaveTwoPasswords)?.Password;
                _repeatedPassword = (parameter as IHaveTwoPasswords)?.SecondPassword;

                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Create new user
                User user = new User
                {
                    Username = Username,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = _password.GetEncryptedPassword(),
                    Gender = Gender,
                    RegisterDate = DateTime.Now,
                    AvatarColor = RandomColorGenerator.GetRandomColor()
                };

                // Try to save this user in the database
                bool isUserSuccesfullyCreated = false;
                await Task.Run(() => isUserSuccesfullyCreated = _dataAccess.TryCreateUser(user));
                if (isUserSuccesfullyCreated == false)
                {
                    _dialogFacade.OpenDialog(DialogMessage.NoInternetConnection);
                    return;
                }

                // Turn off spinning text in the view
                IsRegisterRunning = false;

                // Show successful dialog window 
                _dialogFacade.OpenDialog(DialogMessage.AccountCreated);

                // Reset data in the view model
                ResetData(parameter as IHaveTwoPasswords);

                // Go to LoginPage
                await _applicationViewModel.ChangeCurrentPageAsync(ApplicationPage.LoginPage);
            });
        }

        #endregion

        #region Helpers private methods

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
                if (UsernameMessage == FormMessage.None && DataValidator.IsUsernameCorrect(Username) == false)
                    UsernameMessage = FormMessage.IncorrectUsername;

                // Check if e-mail is in correct format
                if (EmailMessage == FormMessage.None && DataValidator.IsEmailCorrect(Email) == false)
                    EmailMessage = FormMessage.IncorrectEmail;

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

                // Check if username is avaliable in db
                if (UsernameMessage == FormMessage.None && _dataAccess.IsUsernameTaken(Username) == true)
                    UsernameMessage = FormMessage.TakenUsername;

                // Check if e-mail is avaliable in db
                if (EmailMessage == FormMessage.None && _dataAccess.IsEmailTaken(Email) == true)
                    EmailMessage = FormMessage.TakenEmail;

            });

            // Check if any problem was found and return right value
            return IsEnteredDataCorrect();
        }

        private void ResetFormMessages()
        {
            // Reset all the form messages to None
            FirstNameMessage = FormMessage.None;
            LastNameMessage = FormMessage.None;
            UsernameMessage = FormMessage.None;
            EmailMessage = FormMessage.None;
            PasswordMessage = FormMessage.None;
            RepeatedPasswordMessage = FormMessage.None;
            GenderMessage = FormMessage.None;
        }

        private bool IsEnteredDataCorrect()
        {
            // Check if all the form messages are equal to None
            if (FirstNameMessage != FormMessage.None || LastNameMessage != FormMessage.None ||
                UsernameMessage != FormMessage.None || EmailMessage != FormMessage.None ||
                PasswordMessage != FormMessage.None || RepeatedPasswordMessage != FormMessage.None ||
                GenderMessage != FormMessage.None)
                return false;

            return true;
        }

        private void ResetData(IHaveTwoPasswords twoPasswords)
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
