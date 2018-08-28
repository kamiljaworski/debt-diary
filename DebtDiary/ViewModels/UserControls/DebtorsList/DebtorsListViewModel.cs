using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
    }
}
