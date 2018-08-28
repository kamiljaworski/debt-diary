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

                    // Fill debtors list
                    foreach (Debtor debtor in loggedUser.Debtors)
                        Debtors.Add(new DebtorsListItemViewModel(debtor));
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

            // TODO: remove OLD code if new is ok
            //foreach (Debtor debtor in loggedUser.Debtors)
            //{
            //    // Find debtor in VM list with id like in the users one
            //    var foundDebtor = Debtors.Where(d => d.Id == debtor.Id);

            //    // If there is no debtor with this id add them to the list
            //    if (foundDebtor == null || foundDebtor.Count() == 0)
            //        Debtors.Add(new DebtorsListItemViewModel(debtor));
            //}

            // Find debtors whitch are in ClientDataStore but aren't in VM
            var newDebtors = loggedUser.Debtors.Where(d => Debtors.Count(x => x.Id == d.Id) == 0);

            // Add these debtors to VM
            if (newDebtors != null && newDebtors.Count() > 0)
                foreach (Debtor debtor in newDebtors)
                    Debtors.Add(new DebtorsListItemViewModel(debtor));
        }
    }
}
