using DebtDiary.Core;
using System.Collections.ObjectModel;
using System.Linq;

namespace DebtDiary
{
    public class ShortOperationsListViewModel
    {
        public ObservableCollection<ShortOperationsListItemViewModel> Operations { get; set; } = new ObservableCollection<ShortOperationsListItemViewModel>();

        public ShortOperationsListViewModel()
        {

        }

        public ShortOperationsListViewModel(Debtor debtor)
        {
            debtor.Operations.ForEach(x => Operations.Add(new ShortOperationsListItemViewModel(x)));
            Operations = new ObservableCollection<ShortOperationsListItemViewModel>(Operations.OrderByDescending(x => x.OperationDate));
        }

    }
}
