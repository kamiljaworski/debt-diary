using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel
    {
        public ObservableCollection<DebtorsListItemViewModel> Debtors { get; set; } = new ObservableCollection<DebtorsListItemViewModel>();

        public DebtorsListViewModel(bool designTime = false)
        {
            if (designTime == false)
            {
                // Try to get logged user info while application is working
                try
                {
                    User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
                    Debtors = new ObservableCollection<DebtorsListItemViewModel>(loggedUser.Debtors.Select(x => new DebtorsListItemViewModel(x)).OrderByDescending(x => x.Debt));
                }
                // If IoC isn't working (when it is in design mode) do nothing
                catch { }
            }
        }

        /// <summary>
        /// Updates debtors list in this View Model
        /// </summary>
        public void UpdateChanges()
        {
            User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

            // Find debtors which are in ClientDataStore but aren't in VM
            var newDebtors = loggedUser.Debtors.Where(d => Debtors.Count(x => x.Id == d.Id) == 0);

            // Add these debtors to VM
            if (newDebtors != null && newDebtors.Count() > 0)
                foreach (Debtor debtor in newDebtors)
                    Debtors.Add(new DebtorsListItemViewModel(debtor));
        }
    }
}
