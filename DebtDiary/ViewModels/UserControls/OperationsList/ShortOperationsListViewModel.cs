using DebtDiary.Core;
using System.Collections.ObjectModel;

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
        }

    }
}
