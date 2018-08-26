using DebtDiary.Core;
using DebtDiary.DataProvider;
using System.Collections.Generic;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel
    {
        public List<DebtorsListItemViewModel> Debtors { get; set; } = new List<DebtorsListItemViewModel>();

        public DebtorsListViewModel(bool designTime = false)
        {
            if (designTime == false)
            {
                User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;
                IDebtDiaryDataAccess dataAccess = IocContainer.Get<IDebtDiaryDataAccess>();


                foreach (Debtor debtor in dataAccess.GetDebtorsList(loggedUser))
                    Debtors.Add(new DebtorsListItemViewModel(debtor));
            }
        }
    }
}
