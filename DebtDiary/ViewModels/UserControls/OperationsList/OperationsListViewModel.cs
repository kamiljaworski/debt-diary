using DebtDiary.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class OperationsListViewModel
    {
        public ObservableCollection<OperationsListItemViewModel> Operations { get; set; } = new ObservableCollection<OperationsListItemViewModel>();
        public bool IsAnyOperationAdded => Operations.Count > 0 ? true : false;

        public OperationsListViewModel()
        {
        }

        public OperationsListViewModel(IEnumerable<Operation> operations)
        {
            foreach (Operation operation in operations)
                Operations.Add(new OperationsListItemViewModel(operation));

            Operations = new ObservableCollection<OperationsListItemViewModel>(Operations.OrderByDescending(x => x.OperationDate));
        }

    }
}
