using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel, IDebtorsListViewModel
    {
        private IClientDataStore _clientDataStore;
        private IApplicationViewModel _applicationViewModel;
        private SortType _sortType = SortType.Descending;

        public IList<DebtorsListItemViewModel> Debtors { get; set; } = new ObservableCollection<DebtorsListItemViewModel>();
        public bool IsAnyDebtorAdded => Debtors.Count > 0 ? true : false;

        // TODO: Do sth with this ctor
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

        public DebtorsListViewModel(IClientDataStore clientDataStore, IApplicationViewModel applicationViewModel)
        {
            _clientDataStore = clientDataStore;
            _applicationViewModel = applicationViewModel;
            Update();
        }

        #region Public methods

        public void Update()
        {
            User loggedUser = _clientDataStore.LoggedUser;

            // TODO: add NullUser class and remove this if statement
            if (loggedUser == null)
                return;
            
            IEnumerable<DebtorsListItemViewModel> debtorsList = loggedUser.Debtors.Select(x => new DebtorsListItemViewModel(x, _applicationViewModel, this));
            Debtors = Sort(debtorsList, _sortType);
        }

        public void Sort(SortType sortType) => Debtors = Sort(Debtors, sortType);
        #endregion

        #region Private methods

        private ObservableCollection<DebtorsListItemViewModel> Sort(IEnumerable<DebtorsListItemViewModel> debtorsLists, SortType sortType)
        {
            _sortType = sortType;
            IOrderedEnumerable<DebtorsListItemViewModel> list = _sortType == SortType.Ascending ? DebtorsSortedAscending(debtorsLists) : DebtorsSortedDescending(debtorsLists);
            return new ObservableCollection<DebtorsListItemViewModel>(list);
        }

        private IOrderedEnumerable<DebtorsListItemViewModel> DebtorsSortedAscending(IEnumerable<DebtorsListItemViewModel> debtorsLists) => debtorsLists.OrderBy(x => x.Debt).ThenBy(x => x.FullName);
        private IOrderedEnumerable<DebtorsListItemViewModel> DebtorsSortedDescending(IEnumerable<DebtorsListItemViewModel> debtorsLists) => debtorsLists.OrderByDescending(x => x.Debt).ThenBy(x => x.FullName);
        #endregion
    }
}
