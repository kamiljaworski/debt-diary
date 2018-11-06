using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel, IDebtorsListViewModel
    {
        private SortType _sortType = SortType.Descending;

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
            IEnumerable<DebtorsListItemViewModel> debtorsList = loggedUser.Debtors.Select(x => new DebtorsListItemViewModel(x, IocContainer.Get<IApplicationViewModel>(), IocContainer.Get<IDebtorInfoSubpageViewModel>(), IocContainer.Get<IDiaryPageViewModel>()));

            // Make ObservableCollection from this list
            Debtors = Sort(debtorsList, _sortType);

            // Get selected debtor information
            Debtor selectedDebtor = IocContainer.Get<IApplicationViewModel>().SelectedDebtor;
        }

        /// <summary>
        /// Sort Debtors collection order by SortType
        /// </summary>
        private ObservableCollection<DebtorsListItemViewModel> Sort(IEnumerable<DebtorsListItemViewModel> debtorsLists, SortType sortType)
        {
            _sortType = sortType;

            // Sort Debtors list with appropriate order
            IOrderedEnumerable<DebtorsListItemViewModel> list = _sortType == SortType.Ascending ? DebtorsSortedAscending(debtorsLists) : DebtorsSortedDescending(debtorsLists);

            // Make ObservableCollection from this list
            return new ObservableCollection<DebtorsListItemViewModel>(list);
        }

        public void Sort(SortType sortType) => Debtors = Sort(Debtors, sortType);

        #endregion

        private IOrderedEnumerable<DebtorsListItemViewModel> DebtorsSortedAscending(IEnumerable<DebtorsListItemViewModel> debtorsLists) => debtorsLists.OrderBy(x => x.Debt).ThenBy(x => x.FullName);
        private IOrderedEnumerable<DebtorsListItemViewModel> DebtorsSortedDescending(IEnumerable<DebtorsListItemViewModel> debtorsLists) => debtorsLists.OrderByDescending(x => x.Debt).ThenBy(x => x.FullName);
    }
}
