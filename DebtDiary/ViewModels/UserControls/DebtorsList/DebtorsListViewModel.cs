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
                    UpdateChanges();
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

            // Get actual list of debtors and sort them
            IOrderedEnumerable<DebtorsListItemViewModel> debtorsList = loggedUser.Debtors.Select(x => new DebtorsListItemViewModel(x)).OrderByDescending(x => x.Debt);

            // Make ObservableCollection from this list
            Debtors = new ObservableCollection<DebtorsListItemViewModel>(debtorsList);
        }
    }
}
