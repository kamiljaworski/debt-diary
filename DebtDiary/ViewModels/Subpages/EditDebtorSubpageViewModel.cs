using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class EditDebtorSubpageViewModel : BaseViewModel
    {
        #region Private Fields

        private Debtor _selectedDebtor = null;
        private User _loggedUser = null;
        #endregion

        #region Public Properties

        public Color AvatarColor { get; set; } = Color.Green;
        public string Initials => Helpers.GetInitials(FirstName, LastName);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool IsEditDebtorRunning { get; set; } = false;
        #endregion

        #region Form Messages

        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
        public FormMessage GenderMessage { get; set; } = FormMessage.None;
        #endregion

        #region Public Commands

        public ICommand EditDebtorCommand { get; set; }
        public ICommand PreviousColorCommand { get; set; }
        public ICommand NextColorCommand { get; set; }
        #endregion

        #region Constructor

        public EditDebtorSubpageViewModel()
        {
            _selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;
            _loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

            AvatarColor = _selectedDebtor.AvatarColor;
            FirstName = _selectedDebtor.FirstName;
            LastName = _selectedDebtor.LastName;
            Gender = (Gender)_selectedDebtor.Gender;

            EditDebtorCommand = new RelayCommand(async () => await EditDebtorAsync());
            PreviousColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = ColorSelector.Next(AvatarColor));
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Edit debtor
        /// </summary>
        private async Task EditDebtorAsync()
        {
            await RunCommandAsync(() => IsEditDebtorRunning, async () =>
            {
                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Edit debtors properties
                _selectedDebtor.FirstName = FirstName;
                _selectedDebtor.LastName = LastName;
                _selectedDebtor.AvatarColor = AvatarColor;
                _selectedDebtor.Gender = Gender;

                // Save changes in the database
                await Task.Run(() => IocContainer.Get<IDataAccess>().SaveChanges());

                // Update list in the ViewModel
                IDebtorsListViewModel debtorsList = IocContainer.Get<IDebtorsListViewModel>();
                debtorsList.Update();

                // Turn off spinning text
                IsEditDebtorRunning = false;

                // Show successful dialog window 
                IocContainer.Get<IDialogFacade>().OpenDialog(DialogMessage.NewDebtorAdded);

                // Go back to debtor info subpage
                IocContainer.Get<IApplicationViewModel>().ChangeCurrentSubpage(ApplicationSubpage.DebtorInfoSubpage);

                // Clear fields in the view
                ClearAllFields();
            });
        }

        /// <summary>
        /// Validate debtors data
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

                // Check if there is debtor with this first and last name in db
                if (FirstNameMessage == FormMessage.None && LastNameMessage == FormMessage.None)
                    if (_loggedUser.Debtors.Where(d => d.FirstName == FirstName && d.LastName == LastName && d.Id != _selectedDebtor.Id).Count() > 0)
                    {
                        FirstNameMessage = FormMessage.EmptyMessage;
                        LastNameMessage = FormMessage.DebtorExist;
                    }

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
        /// Clear fields in the view
        /// </summary>
        private void ClearAllFields()
        {
            AvatarColor = Color.Green;
            FirstName = string.Empty;
            LastName = string.Empty;
            Gender = Gender.None;
        }
        #endregion
    }
}
