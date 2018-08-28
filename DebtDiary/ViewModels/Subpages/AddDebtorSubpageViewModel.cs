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

        public ICommand AddDebtorCommand { get; set; }

        public AddDebtorSubpageViewModel()
        {
            AddDebtorCommand = new RelayCommand(async () => await AddDebtorAsync());
        }

        private async Task AddDebtorAsync()
        {


            Debtor debtor = new Debtor
            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender
            };

            

            User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
            loggedUser.Debtors.Add(debtor);

            IocContainer.Get<IDebtDiaryDataAccess>().SaveChanges();

            DebtorsListViewModel debtorsList = IocContainer.Get<DebtorsListViewModel>();
            debtorsList.UpdateChanges();
        }
    }
}
