using DebtDiary.Core;
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
            return;
        }
    }
}
