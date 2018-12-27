using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class DebtorsListViewModel : BaseViewModel, IDebtorsListViewModel
    {
        private readonly IApplicationViewModel _applicationViewModel;
        private readonly IClientDataStore _clientDataStore;
        private SortType _sortType = SortType.Descending;

        public IList<DebtorsListItemViewModel> Debtors { get; set; } = new ObservableCollection<DebtorsListItemViewModel>();
        public bool IsAnyDebtorAdded => Debtors.Count > 0 ? true : false;

        // Design mode constructor
        public DebtorsListViewModel() { }

        public DebtorsListViewModel(IApplicationViewModel applicationViewModel, IClientDataStore clientDataStore)
        {
            _clientDataStore = clientDataStore;
            _applicationViewModel = applicationViewModel;
            Update();
        }

        #region Public methods

        public void Update()
        {
            if (_clientDataStore == null || _clientDataStore.LoggedUser == null || _applicationViewModel == null)
                return;

            User loggedUser = _clientDataStore.LoggedUser;

            IEnumerable<DebtorsListItemViewModel> debtorsList = loggedUser.Debtors.Select(x => new DebtorsListItemViewModel(x, _applicationViewModel, this));
            Debtors = Sort(debtorsList, _sortType);
        }

        public void Sort(SortType sortType) => Debtors = Sort(Debtors, sortType);
        #endregion

        #region Private methods

        private ObservableCollection<DebtorsListItemViewModel> Sort(IEnumerable<DebtorsListItemViewModel> debtorsLists, SortType sortType)
        {
            _sortType = sortType;
            var list = _sortType == SortType.Ascending ? DebtorsSortedAscending(debtorsLists) : DebtorsSortedDescending(debtorsLists);
            return new ObservableCollection<DebtorsListItemViewModel>(list);
        }

        private IOrderedEnumerable<DebtorsListItemViewModel> DebtorsSortedAscending(IEnumerable<DebtorsListItemViewModel> debtorsLists)
        {
            return debtorsLists.OrderBy(x => x.Debt).ThenBy(x => x.FullName);
        }

        private IOrderedEnumerable<DebtorsListItemViewModel> DebtorsSortedDescending(IEnumerable<DebtorsListItemViewModel> debtorsLists)
        {
            return debtorsLists.OrderByDescending(x => x.Debt).ThenBy(x => x.FullName);
        }
        #endregion
    }
}
