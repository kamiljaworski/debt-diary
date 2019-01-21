using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class ShortOperationsListViewModel
    {
        public ObservableCollection<ShortOperationsListItemViewModel> Operations { get; set; } = new ObservableCollection<ShortOperationsListItemViewModel>();
        public bool IsAnyOperationAdded => Operations.Count > 0 ? true : false;

        public ShortOperationsListViewModel() { }

        public ShortOperationsListViewModel(IEnumerable<Operation> operations)
        {
            operations.ForEach(o => Operations.Add(new ShortOperationsListItemViewModel(o)));

            Operations = new ObservableCollection<ShortOperationsListItemViewModel>(Operations.OrderByDescending(x => x.OperationDate.Date).ThenByDescending(x => x.Id));
        }
    }
}
