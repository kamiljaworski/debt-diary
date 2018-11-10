using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class MyAccountSubpageViewModel : BaseViewModel, ILoadable
    {
        #region Private Fields
        private IDiaryPageViewModel _diaryPageViewModel;
        private IDialogFacade _dialogFacade;
        private IClientDataStore _clientDataStore;
        private IDataAccess _dataAccess;

        private User _loggedUser = null;
        private IHaveThreePasswords _passwords = null;
        #endregion

        #region Public Properties

        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => Helpers.GetInitials(FirstName, LastName);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool IsEditProfileRunning { get; set; }
        public bool IsChangePasswordRunning { get; set; }
        #endregion

        #region Form Messages

        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
        public FormMessage GenderMessage { get; set; } = FormMessage.None;

        public FormMessage CurrentPasswordMessage { get; set; } = FormMessage.None;
        public FormMessage NewPasswordMessage { get; set; } = FormMessage.None;
        public FormMessage RepeatNewPasswordMessage { get; set; } = FormMessage.None;

        #endregion

        #region Public Commands

        public ICommand EditProfileCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand PreviousColorCommand { get; set; }
        public ICommand NextColorCommand { get; set; }

        public bool IsLoaded { get; private set; }
        #endregion

        #region Constructor

        public MyAccountSubpageViewModel(IDiaryPageViewModel diaryPageViewModel, IDialogFacade dialogFacade, IClientDataStore clientDataStore, IDataAccess dataAccess)
        {
            IsLoaded = false;

            _diaryPageViewModel = diaryPageViewModel;
            _dialogFacade = dialogFacade;
            _clientDataStore = clientDataStore;
            _dataAccess = dataAccess;

            _loggedUser = _clientDataStore.LoggedUser;

            // TODO: NullUser ...
            if (_loggedUser != null)
            {
                AvatarColor = _loggedUser.AvatarColor;
                FirstName = _loggedUser.FirstName;
                LastName = _loggedUser.LastName;
                Username = _loggedUser.Username;
                Email = _loggedUser.Email;
                Gender = (Gender)_loggedUser.Gender;
            }

            EditProfileCommand = new RelayCommand(async () => await EditProfileAsync());
            ChangePasswordCommand = new RelayParameterizedCommand(async (parameter) => await ChangePasswordAsync(parameter));
            PreviousColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Next(AvatarColor));
            IsLoaded = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Edit profile and update it in the UI
        /// </summary>
        private async Task EditProfileAsync()
        {
            await RunCommandAsync(() => IsEditProfileRunning, async () =>
            {
                // Validate entered data
                if (await ValidateEditProfileDataAsync() == false)
                    return;

                // Update user with new data
                UpdateLoggedUser();

                // Save changes in the database
                await Task.Run(() =>_dataAccess.SaveChanges());

                // Update users data in the side menu
                _diaryPageViewModel.UpdateUsersData();

                // Turn off spinning text
                IsEditProfileRunning = false;

                // Show successful dialog window 
                _dialogFacade.OpenDialog(DialogMessage.ProfileUpdated);
            });
        }

        /// <summary>
        /// Change logged users password
        /// </summary>
        private async Task ChangePasswordAsync(object parameter)
        {
            await RunCommandAsync(() => IsEditProfileRunning, async () =>
            {
                // Get password from the view
                if (parameter is IHaveThreePasswords)
                    _passwords = (IHaveThreePasswords)parameter;

                // Validate entered data
                if (await ValidateChangePasswordDataAsync() == false)
                    return;

                UpdateUsersPassword();

                // Save changes in the database
                await Task.Run(() => _dataAccess.SaveChanges());

                // Clear password fields in the view
                _passwords.ClearPassword();

                // Turn off spinning text
                IsEditProfileRunning = false;

                // Show successful dialog window 
                _dialogFacade.OpenDialog(DialogMessage.PasswordChanged);
            });
        }

        /// <summary>
        /// Validate users data in the EditProfile form
        /// </summary>
        private async Task<bool> ValidateEditProfileDataAsync()
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

                // Check if gender was selected
                if (Gender == Gender.None)
                    GenderMessage = FormMessage.UnselectedGender;

                // Check if first name is correct
                if (FirstNameMessage == FormMessage.None && DataValidator.IsNameCorrect(FirstName) == false)
                    FirstNameMessage = FormMessage.IncorrectFirstName;

                // Check if first name is correct
                if (LastNameMessage == FormMessage.None && DataValidator.IsNameCorrect(LastName) == false)
                    LastNameMessage = FormMessage.IncorrectLastName;
            });

            // Check if any problem was found and return right value
            return IsEditProfileEnteredDataCorrect();
        }

        /// <summary>
        /// Validate users data in the ChangePassword form
        /// </summary>
        private async Task<bool> ValidateChangePasswordDataAsync()
        {
            await Task.Run(() =>
            {
                // Reset all the form messages properties
                ResetFormMessages();

                // Check if current password is null or empty
                if (_passwords.Password.IsNullOrEmpty())
                    CurrentPasswordMessage = FormMessage.EmptyCurrentPassword;

                // Check if new password is null or empty
                if (_passwords.SecondPassword.IsNullOrEmpty())
                    NewPasswordMessage = FormMessage.EmptyNewPassword;

                // Check if repeated new password is null or empty
                if (_passwords.ThirdPassword.IsNullOrEmpty())
                    RepeatNewPasswordMessage = FormMessage.EmptyRepeatedNewPassword;

                // Check if current password is correct
                if (CurrentPasswordMessage == FormMessage.None && _passwords.Password.GetEncryptedPassword() != _loggedUser.Password)
                    CurrentPasswordMessage = FormMessage.IncorrectCurrentPassword;

                // Check if new password is longer or equal to 8 characters
                if (NewPasswordMessage == FormMessage.None && RepeatNewPasswordMessage == FormMessage.None && _passwords.SecondPassword.Length < 8)
                {
                    NewPasswordMessage = FormMessage.PasswordTooShort;
                    RepeatNewPasswordMessage = FormMessage.EmptyMessage;
                }

                // Check if passwords are the same
                if (NewPasswordMessage == FormMessage.None && RepeatNewPasswordMessage == FormMessage.None &&
                    _passwords.SecondPassword.GetEncryptedPassword() != _passwords.ThirdPassword.GetEncryptedPassword())
                {
                    NewPasswordMessage = FormMessage.DifferentPasswords;
                    RepeatNewPasswordMessage = FormMessage.EmptyMessage;
                }
            });

            // Check if any problem was found and return right value
            return IsChangePasswordEnteredDataCorrect();
        }

        /// <summary>
        /// Reset all the <see cref="FormMessage"/> properties to <see cref="FormMessage.None"/>
        /// </summary>
        private void ResetFormMessages()
        {
            FirstNameMessage = FormMessage.None;
            LastNameMessage = FormMessage.None;
            GenderMessage = FormMessage.None;
            CurrentPasswordMessage = FormMessage.None;
            NewPasswordMessage = FormMessage.None;
            RepeatNewPasswordMessage = FormMessage.None;
        }

        /// <summary>
        /// Check if all the <see cref="FormMessage"/> properties are set to <see cref="FormMessage.None"/>
        /// </summary>
        private bool IsEditProfileEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (FirstNameMessage != FormMessage.None || LastNameMessage != FormMessage.None || GenderMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        /// <summary>
        /// Check if all the <see cref="FormMessage"/> properties are set to <see cref="FormMessage.None"/>
        /// </summary>
        private bool IsChangePasswordEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (CurrentPasswordMessage != FormMessage.None || NewPasswordMessage != FormMessage.None || RepeatNewPasswordMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        /// <summary>
        /// Update logged users data
        /// </summary>
        private void UpdateLoggedUser()
        {
            _loggedUser.AvatarColor = AvatarColor;
            _loggedUser.FirstName = FirstName;
            _loggedUser.LastName = LastName;
            _loggedUser.Gender = Gender;
        }

        /// <summary>
        /// Update logged users password
        /// </summary>
        private void UpdateUsersPassword() => _loggedUser.Password = _passwords.SecondPassword.GetEncryptedPassword();


        #endregion
    }
}
