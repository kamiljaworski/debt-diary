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

        public ShortOperationsListViewModel()
        {
        }

        public ShortOperationsListViewModel(IList<Operation> operations)
        {
            foreach (Operation operation in operations)
                Operations.Add(new ShortOperationsListItemViewModel(operation));

            Operations = new ObservableCollection<ShortOperationsListItemViewModel>(Operations.OrderByDescending(x => x.OperationDate));
        }

    }
}
