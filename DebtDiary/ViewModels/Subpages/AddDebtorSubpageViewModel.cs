using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class AddDebtorSubpageViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public AvatarColor AvatarColor { get; set; } = AvatarColor.Green;

        public ICommand AddDebtorCommand { get; set; }
        public ICommand PreviousColorCommand { get; set; }
        public ICommand NextColorCommand { get; set; }

        public AddDebtorSubpageViewModel()
        {
            AddDebtorCommand = new RelayCommand(async () => await AddDebtorAsync());
            PreviousColorCommand = new RelayCommand(() => AvatarColor = AvatarColorChanger.Previous(AvatarColor));
            NextColorCommand = new RelayCommand(() => AvatarColor = AvatarColorChanger.Next(AvatarColor));
        }

        private async Task AddDebtorAsync()
        {
            // Make new debtor object
            Debtor debtor = new Debtor
            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender
            };

            // Add new debtor to ClientDataStore
            User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
            loggedUser.Debtors.Add(debtor);

            // Save changes in the database
            IocContainer.Get<IDebtDiaryDataAccess>().SaveChanges();

            // Update list in the ViewModel
            DebtorsListViewModel debtorsList = IocContainer.Get<DebtorsListViewModel>();
            debtorsList.UpdateChanges();

            // Clear fields in the view
            ResetData();

            // TODO: Refactor this code and add data validation
        }

        private void ResetData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Gender = Gender.None;
        }
    }
}
