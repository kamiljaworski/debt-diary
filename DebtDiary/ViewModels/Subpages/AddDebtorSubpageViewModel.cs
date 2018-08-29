﻿using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class AddDebtorSubpageViewModel : BaseViewModel
    {
        #region Private Fields

        private User _loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
        #endregion

        #region Public Properties

        public AvatarColor AvatarColor { get; set; } = AvatarColor.Green;
        public string Initials => Helpers.GetInitials(FirstName, LastName);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool IsAddDebtorRunning { get; set; } = false;
        #endregion

        #region Form Messages

        public FormMessage FirstNameMessage { get; set; } = FormMessage.None;
        public FormMessage LastNameMessage { get; set; } = FormMessage.None;
        public FormMessage GenderMessage { get; set; } = FormMessage.None;
        #endregion

        #region Public Commands

        public ICommand AddDebtorCommand { get; set; }
        public ICommand PreviousColorCommand { get; set; }
        public ICommand NextColorCommand { get; set; }
        #endregion

        #region Constructor

        public AddDebtorSubpageViewModel()
        {
            AddDebtorCommand = new RelayCommand(async () => await AddDebtorAsync());
            PreviousColorCommand = new RelayCommand(() => AvatarColor = AvatarColorChanger.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = AvatarColorChanger.Next(AvatarColor));
        }
        #endregion

        #region Private Methods

        private async Task AddDebtorAsync()
        {
            await RunCommandAsync(() => IsAddDebtorRunning, async () =>
            {
                // Validate entered data
                if (await ValidateDataAsync() == false)
                    return;

                // Make new debtor object
                Debtor debtor = new Debtor
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Gender = Gender,
                    AvatarColor = AvatarColor
                };

                // Add new debtor to ClientDataStore
                _loggedUser.Debtors.Add(debtor);

                // Save changes in the database
                IocContainer.Get<IDebtDiaryDataAccess>().SaveChanges();

                // Update list in the ViewModel
                DebtorsListViewModel debtorsList = IocContainer.Get<DebtorsListViewModel>();
                debtorsList.UpdateChanges();

                // Clear fields in the view
                ResetData();

                // TODO: Refactor this code and add data validation
            });
        }

        /// <summary>
        /// Validate new debtors data
        /// </summary>
        /// <returns>True if debtor can be added to the database or false if not</returns>
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
                    if (_loggedUser.Debtors.Where(d => d.FirstName == FirstName && d.LastName == LastName).Count() > 0)
                    {
                        FirstNameMessage = FormMessage.DebtorExist;
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
        /// <returns>False if there are some errors and true if not</returns>
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
        private void ResetData()
        {
            AvatarColor = AvatarColor.Green;
            FirstName = string.Empty;
            LastName = string.Empty;
            Gender = Gender.None;
        }
        #endregion
    }
}
