using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel, IDebtorsListViewModel
    {
        public IList<DebtorsListItemViewModel> Debtors { get; set; } = new ObservableCollection<DebtorsListItemViewModel>();
        public bool IsAnyDebtorAdded => Debtors.Count > 0 ? true : false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DebtorsListViewModel(bool designTime = false)
        {
            if (designTime == false)
            {
                // Try to get logged user info while application is working
                try
                {
                    Update();
                }
                // If IoC isn't working (when it is in design mode) do nothing
                catch { }
            }
        }

        #region Public properties

        /// <summary>
        /// Update debtors list in this View Model
        /// </summary>
        public void Update()
        {
            User loggedUser = IocContainer.Get<IClientDataStore>().LoggedUser;

            // Get actual list of debtors and sort them
            IOrderedEnumerable<DebtorsListItemViewModel> debtorsList = loggedUser.Debtors.Select(x => new DebtorsListItemViewModel(x)).OrderByDescending(x => x.Debt);

            // Make ObservableCollection from this list
            Debtors = new ObservableCollection<DebtorsListItemViewModel>(debtorsList);

            // Get selected debtor information
            Debtor selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;

            // If any debtor is selected, set its property to true
            if(selectedDebtor != null)
            {
                DebtorsListItemViewModel debtorListItem = Debtors.FirstOrDefault(x => x.Id == selectedDebtor.Id);

                if (debtorListItem != null)
                    debtorListItem.IsSelected = true;
            }
        }

        /// <summary>
        /// Reset all the debtors IsSelected properties to false
        /// </summary>
        public void ResetSelectedDebtor() => Debtors.ToList()?.ForEach(x => x.IsSelected = false);

        /// <summary>
        /// Sort Debtors collection order by SortType
        /// </summary>
        public void Sort(SortType sortType)
        {
            // Sort Debtors list with appropriate order
            IOrderedEnumerable<DebtorsListItemViewModel> list = sortType == SortType.Ascending ? Debtors.OrderBy(x => x.Debt) : Debtors.OrderByDescending(x => x.Debt);

            // Make ObservableCollection from this list
            Debtors = new ObservableCollection<DebtorsListItemViewModel>(list);
        }
        #endregion
    }
}
