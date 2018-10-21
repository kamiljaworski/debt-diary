using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class MyAccountSubpageViewModel : BaseViewModel
    {
        #region Private Fields

        private User _loggedUser = null;
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
        #endregion

        #region Constructor

        public MyAccountSubpageViewModel()
        {
            _loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
            if(_loggedUser != null)
            {
                AvatarColor = _loggedUser.AvatarColor;
                FirstName = _loggedUser.FirstName;
                LastName = _loggedUser.LastName;
                Username = _loggedUser.Username;
                Email = _loggedUser.Email;
                Gender = (Gender)_loggedUser.Gender;
            }

            EditProfileCommand = new RelayCommand(async () => await EditProfileAsync());
            PreviousColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Next(AvatarColor));
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Add new debtor to the database and UI
        /// </summary>
        private async Task EditProfileAsync()
        {
            await RunCommandAsync(() => IsEditProfileRunning, async () =>
            {
                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Update user with new data
                UpdateUserData();

                // Save changes in the database
                await Task.Run(() => IocContainer.Get<IDataAccess>().SaveChanges());

                // Update users data in the side menu
                IocContainer.Get<IDiaryPageViewModel>().UpdateUsersData(); ;

                // Turn off spinning text
                IsEditProfileRunning = false;

                // Show successful dialog window 
                // TODO: new dialog message - profile updated
                IocContainer.Get<IDialogFacade>().OpenDialog(DialogMessage.ProfileUpdated);
            });
        }

        /// <summary>
        /// Validate new debtors data
        /// </summary>
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
            return IsEnteredDataCorrect();
        }

        /// <summary>
        /// Reset all the <see cref="FormMessage"/> properties to <see cref="FormMessage.None"/>
        /// </summary>
        private void ResetFormMessages()
        {
            FirstNameMessage = FormMessage.None;
            LastNameMessage = FormMessage.None;
            GenderMessage = FormMessage.None;
        }

        /// <summary>
        /// Check if all the <see cref="FormMessage"/> properties are set to <see cref="FormMessage.None"/>
        /// </summary>
        private bool IsEnteredDataCorrect()
        {
            // If any of the messages changed it's value return false
            if (FirstNameMessage != FormMessage.None || LastNameMessage != FormMessage.None ||
                GenderMessage != FormMessage.None)
                return false;

            // If not return true
            return true;
        }

        /// <summary>
        /// Update logged users data
        /// </summary>
        private void UpdateUserData()
        {
            _loggedUser.AvatarColor = AvatarColor;
            _loggedUser.FirstName = FirstName;
            _loggedUser.LastName = LastName;
            _loggedUser.Gender = Gender;
        }

        #endregion
    }
}
