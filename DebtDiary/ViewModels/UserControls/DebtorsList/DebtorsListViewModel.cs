using DebtDiary.Core;
using System.Collections.Generic;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel
    {
        public List<DebtorsListItemViewModel> Debtors { get; set; }

        public DebtorsListViewModel(bool designTime = false)
        {
            if (designTime == false)
            {
                User loggedUser = IocContainer.Get<IClientDataStore>().CurrentUser;

                foreach (Debtor debtor in loggedUser.Debtors)
                    Debtors.Add(new DebtorsListItemViewModel(debtor));
            }
        }
    }
}
